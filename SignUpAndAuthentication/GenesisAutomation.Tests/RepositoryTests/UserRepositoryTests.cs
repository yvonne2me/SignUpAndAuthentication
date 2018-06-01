using System;
using GenesisAutomation.EF;
using GenesisAutomation.Types;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using GenesisAutomation.Models;
using GenesisAutomation.Repositories;
using Moq;
using GenesisAutomation.Logging;
using GenesisAutomation.Interfaces;
using Xunit;

namespace GenesisAutomation.Tests.RepositoryTests
{
    public class UserRepositoryTests
    {
        DataContext _context;
        static UserRepository userRepository;

        static Mock<ICILogger> mockLogger;
        static Mock<ITokenService> tokenService;

        public UserRepositoryTests()
        {
            InitContext();
            Init();
        }

        public void Init()
        {
            mockLogger = new Mock<ICILogger>();
            tokenService = new Mock<ITokenService>();


            userRepository = new UserRepository(mockLogger.Object, 
                                                tokenService.Object,
                                                _context);
        }

        public void InitContext()
        {
            var builder = new DbContextOptionsBuilder<DataContext>()
                    .UseInMemoryDatabase("DataContext");

            var context = new DataContext(builder.Options);

            for (var i = 0; i < 5; i++)
            {
                User user = new User
                {
                    Email = "testuser@test.com",
                    Id = Guid.NewGuid(),
                    Name = "TestUser",
                    Password = "password123",
                    Telephones = new List<Telephone>()
                };

                AuthorizationResponse signUpResponse = new AuthorizationResponse
                {
                    CreatedOn = DateTime.UtcNow,
                    Id = Guid.NewGuid(),
                    LastLoginOn = DateTime.UtcNow,
                    LastUpdatedOn = DateTime.UtcNow,
                    Token = "555"
                };

                context.Users.Add(user);
                context.SignUpResponses.Add(signUpResponse);
            }

            context.SaveChanges();
            _context = context;

        }

        [Fact]
        public void Test_AddUser_Success()
        {
            User user = new User()
            {
                Email = "newuser1@email.com",
                Password = "password1"
            };

            var response = userRepository.CreateNewUser(user);
            Assert.NotNull(response);
        }

    }
}
