using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using GenesisAutomation.Exceptions;
using GenesisAutomation.Interfaces;
using GenesisAutomation.Logging;
using GenesisAutomation.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GenesisAutomation.Services
{
    public class TokenService : ITokenService
    {
        ICILogger logger;
        IConfiguration _config;

        public TokenService(ICILogger logger, IConfiguration config)
        {
            this.logger = logger;
            _config = config;
        }

        public string CreateToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool ValidateTokens(string savedUserToken, string currentUserToken)
        {
            if (currentUserToken != savedUserToken)
            {
                logger.LogInfo("SearchController - GET - Token provided does not match");
                throw new BusinessException(System.Net.HttpStatusCode.Unauthorized, "Unauthorized");
            }

            //Attempt at customizing Expiry Response Message but this JWT authentication will catch before it reaches this.
            //if (expiryTime < DateTime.UtcNow.AddMinutes(-30))
            //{
            //    logger.LogInfo("SearchController - GET - Token Expired");
            //    throw new BusinessException(System.Net.HttpStatusCode.Unauthorized, "Invalid Session");
            //}

            return true;
        }
    }
}
