using System;
using System.Net;
using System.Linq;
using GenesisAutomation.Exceptions;
using GenesisAutomation.Interfaces;
using GenesisAutomation.Models;
using GenesisAutomation.Logging;
using System.Security.Cryptography;
using System.Text;
using GenesisAutomation.EF;

namespace GenesisAutomation.Repositories
{
    public class UserRepository : IUserRepository
    {
        DataContext _context;

        ICILogger logger;
        ITokenService tokenService;

        public UserRepository(ICILogger logger, ITokenService tokenService, DataContext context)
        {
            _context = context;
            this.logger = logger;
            this.tokenService = tokenService;
        }

        public bool DoesUserExist(string email)
        {
            if(!(_context.Users.Any(user => user.Email.Equals(email))))
            {
                return false;
            }

            logger.LogInfo("E-mail already exists");
            throw new BusinessException(HttpStatusCode.BadRequest, "E-mail already exists");
        }


        public AuthorizationResponse CreateNewUser(User user)
        {
            AuthorizationResponse signUpResponse = new AuthorizationResponse();
            signUpResponse.Id = Guid.NewGuid();
            signUpResponse.CreatedOn = DateTime.UtcNow;
            signUpResponse.LastLoginOn = DateTime.UtcNow;
            signUpResponse.LastUpdatedOn = DateTime.UtcNow;

            signUpResponse.Token = tokenService.CreateToken();

            SaveNewUser(user, signUpResponse);

            return signUpResponse;

        }

        public void SaveNewUser(User user, AuthorizationResponse signUpResponse)
        {
            user.Id = signUpResponse.Id;
            user.Password = GetHash(user.Password);

            _context.Users.Add(user);
            _context.SignUpResponses.Add(signUpResponse);
            _context.SaveChanges();
        }


        public User GetUser(Guid userId)
        {
            //var user = _context.StoredUser.FirstOrDefault(u => u.user.Id == userId);
            //var user = _context.StoredUser.Where(u => u.user.Id.Equals(userId)).FirstOrDefault();
            var user = _context.Users.Where(u => u.Id.Equals(userId)).FirstOrDefault();

            if (user == null)
            {
                logger.LogInfo("UserRepository - GetSpecificUser - User Not Found");

                throw new BusinessException(HttpStatusCode.Unauthorized,
                                            "Invalid user and / or password");
            }

            return user;
        }


        public User GetUser(string email)
        {
            var user = _context.Users.Where(u => u.Email.Equals(email)).FirstOrDefault();

            if (user == null)
            {
                logger.LogInfo("UserRepository - GetSpecificUser - User Not Found");

                throw new BusinessException(HttpStatusCode.Unauthorized,
                                            "Invalid user and / or password");
            }

            return user;
        }

        public AuthorizationResponse CheckUserCredentials(SignInRequest signInRequest)
        {
            var signInEmail = signInRequest.Email;
            var signInPassword = signInRequest.Password;

            var user = GetUser(signInEmail);

            if (!CheckPassword(user, GetHash(signInPassword)))
            {
                logger.LogInfo("UserRepository - CheckPassword - Password did not match");

                throw new BusinessException(HttpStatusCode.Unauthorized,
                                            "Invalid user and / or password");
            }

            var signUpResponse = GetAuthorizationResponse(user.Id);

            signUpResponse.LastLoginOn = DateTime.UtcNow;
            signUpResponse.Token = tokenService.CreateToken();
            _context.SignUpResponses.Update(signUpResponse);
            _context.SaveChanges();

            return signUpResponse;
        }

        public AuthorizationResponse GetAuthorizationResponse(Guid userId)
        {
            return _context.SignUpResponses.Where(s => s.Id.Equals(userId)).FirstOrDefault();
        }

        string GetHash(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }


        bool CheckPassword(User user, string password)
        {
            var passwordMatch = false;

            if (user.Password.Equals(password))
            {
                passwordMatch = true;
            }

            return passwordMatch;
        }

    }
}
