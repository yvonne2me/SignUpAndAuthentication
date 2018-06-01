using System;
using SignUpAndAuthentication.Interfaces;
using SignUpAndAuthentication.Logging;
using SignUpAndAuthentication.Models;
using SignUpAndAuthentication.Services;
using Moq;
using Xunit;

namespace SignUpAndAuthentication.Tests.ServiceTests
{
    public class SearchServiceTests
    {
        static Mock<ICILogger> mockLogger;
        static Mock<ITokenService> tokenService;
        static Mock<IUserRepository> userRepository;
        static SearchService searchService;

        public SearchServiceTests()
        {
            mockLogger = new Mock<ICILogger>();
            tokenService = new Mock<ITokenService>();
            userRepository = new Mock<IUserRepository>();
            searchService = new SearchService(mockLogger.Object, userRepository.Object, tokenService.Object);
        }


        [Fact]
        public void Test_GetUser_By_Id_Success()
        {
            User user = new User()
            {
                Id = Guid.Parse("8ad173ef-874a-4d49-99dd-c1d239e27f81"),
                Email = "newuser1@email.com",
                Password = "password1"
            };

            AuthorizationResponse authorizationResponse = new AuthorizationResponse()
            {
                Token = "12345"
            };

            userRepository.Setup(u => u.GetUser(It.Is<Guid>(s => s.Equals(user.Id)))).Returns(user);
            userRepository.Setup(u => u.GetAuthorizationResponse(It.Is<Guid>(s => s.Equals(user.Id)))).Returns(authorizationResponse);
            tokenService.Setup(t => t.ValidateTokens(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            var getUser = searchService.GetUser(user.Id, "token");

            Assert.Equal(user.Email, getUser.Email);
            Assert.Equal(user.Id, getUser.Id);
        }
    }
}
