using SignUpAndAuthentication.Interfaces;
using SignUpAndAuthentication.Logging;
using SignUpAndAuthentication.Models;

namespace SignUpAndAuthentication.Services
{
    public class SignInService : ISignInService
    {
        ICILogger logger;
        IUserRepository userRepository;

        public SignInService(ICILogger logger, IUserRepository userRepository)
        {
            this.logger = logger;
            this.userRepository = userRepository;
        }

        public AuthorizationResponse LogIn(SignInRequest signInRequest)
        {
            return userRepository.CheckUserCredentials(signInRequest);
        }
    }
}
