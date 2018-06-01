using SignUpAndAuthentication.Exceptions;
using SignUpAndAuthentication.Interfaces;
using SignUpAndAuthentication.Logging;
using SignUpAndAuthentication.Models;

namespace SignUpAndAuthentication.Services
{
    public class SignUpService : ISignUpService
    {
        ICILogger logger;
        IUserRepository userRepository;

        public SignUpService(ICILogger logger, IUserRepository userRepository)
        {
            this.logger = logger;
            this.userRepository = userRepository;
        }

        public AuthorizationResponse CreateNewUser(User user)
        {
            if (!userRepository.DoesUserExist(user.Email))
            {              
                return userRepository.CreateNewUser(user);
            }

            return null;
        }
    }
}
