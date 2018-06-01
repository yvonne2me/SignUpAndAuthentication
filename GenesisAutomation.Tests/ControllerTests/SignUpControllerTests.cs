using System;
using Xunit;
using GenesisAutomation.Controllers;
using GenesisAutomation.Models;
using System.Collections.Generic;
using System.Net;
using Moq;
using GenesisAutomation.Logging;
using GenesisAutomation.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GenesisAutomation.Tests.ControllerTests
{
    public class SignUpControllerTests
    {
        static Mock<ICILogger> mockLogger;
        static Mock<ISignUpService> mockSignUpService;

        static SignUpController signUpController;

        public static void Init()
        {
            mockLogger = new Mock<ICILogger>();
            mockSignUpService = new Mock<ISignUpService>();
            signUpController = new SignUpController(mockLogger.Object, mockSignUpService.Object);
        }


        [Fact]
        public void Test_SignUpController_Valid_Response()
        {
            Init();

            var user = new User();
            user.Email = "testemail@email.com";
            user.Name = "Test User";
            user.Password = "Password";
            user.Telephones = new List<Telephone>();
            Telephone telephone = new Telephone();
            telephone.Number = "0211234567";
            user.Telephones.Add(telephone);

            var result = signUpController.Post(user);
            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void Test_SignUpController_Bad_Request_No_User()
        {
            Init();

            var result = signUpController.Post(null);
            var response = result as ObjectResult;

            Assert.Equal(400, response.StatusCode);
            Assert.Equal("No User Information provided", response.Value);
        }
    }
}
