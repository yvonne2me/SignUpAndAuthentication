using GenesisAutomation.Interfaces;
using GenesisAutomation.Logging;
using GenesisAutomation.Models;

namespace GenesisAutomation.Services
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
