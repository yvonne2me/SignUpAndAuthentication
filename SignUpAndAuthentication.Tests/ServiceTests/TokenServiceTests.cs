using System;
using SignUpAndAuthentication.Exceptions;
using SignUpAndAuthentication.Interfaces;
using SignUpAndAuthentication.Logging;
using SignUpAndAuthentication.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace SignUpAndAuthentication.Tests.ServiceTests
{
    public class TokenServiceTests
    {

        static Mock<ICILogger> mockLogger;
        static Mock<IConfiguration> mockConfiguration;
        static TokenService tokenService;

        public static void Init()
        {
            mockLogger = new Mock<ICILogger>();
            mockConfiguration = new Mock<IConfiguration>();
            tokenService = new TokenService(mockLogger.Object, mockConfiguration.Object);
        }

        public TokenServiceTests()
        {
            Init();
        }

        [Fact]
        public void Test_Validate_Token_Success()
        {
            var savedToken = "password1";
            var currentToken = "password1";

            var response = tokenService.ValidateTokens(savedToken, currentToken);
            Assert.True(response);
        }

        [Fact]
        public void Test_Validate_Token_Fail()
        {
            var savedToken = "password1";
            var currentToken = "password2";

            Assert.Throws<BusinessException>(() => tokenService.ValidateTokens(savedToken, currentToken));
        }
    }
}
