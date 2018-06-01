using System;
using GenesisAutomation.Controllers;
using GenesisAutomation.Interfaces;
using GenesisAutomation.Logging;
using GenesisAutomation.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace GenesisAutomation.Tests.ControllerTests
{
    public class SignInControllerTests
    {
        static Mock<ICILogger> mockLogger;
        static Mock<ISignInService> mockSignInService;

        public static void Init()
        {
            mockLogger = new Mock<ICILogger>();
            mockSignInService = new Mock<ISignInService>();
        }

        public SignInControllerTests()
        {
            Init();
        }

        [Fact]
        public void Test_SignInController_Success()
        {
            var signInController = new SignInController(mockLogger.Object, mockSignInService.Object);

            SignInRequest signInRequest = new SignInRequest()
            {
                Email = "test@test.com",
                Password = "Password"
            };


            var result = signInController.Post(signInRequest);
            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void Test_SignInController_Fail()
        {
            var signInController = new SignInController(mockLogger.Object, mockSignInService.Object);

            var result = signInController.Post(null);
            var response = result as ObjectResult;

            Assert.Equal(400, response.StatusCode);
            Assert.Equal("No Sign In Information provided", response.Value);
        }

    }
}
