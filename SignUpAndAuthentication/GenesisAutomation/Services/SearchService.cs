using System;
using GenesisAutomation.Interfaces;
using GenesisAutomation.Logging;
using GenesisAutomation.Models;

namespace GenesisAutomation.Services
{
    public class SearchService : ISearchService
    {
        ICILogger logger;
        ITokenService tokenService;
        IUserRepository userRepository;

        public SearchService(ICILogger logger, IUserRepository userRepository, ITokenService tokenService)
        {
            this.logger = logger;
            this.userRepository = userRepository;
            this.tokenService = tokenService;
        }

        public User GetUser(Guid userId, string token)
        {
            var user = userRepository.GetUser(userId);
            var signUpResponse = userRepository.GetAuthorizationResponse(userId);

            var checkToken = tokenService.ValidateTokens(signUpResponse.Token, token);

            if(checkToken)
            {
                return user;
            }

            return null;
        }
    }
}
