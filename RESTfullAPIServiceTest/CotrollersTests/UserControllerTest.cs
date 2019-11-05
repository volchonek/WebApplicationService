using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Extensions;
using Moq;
using RESTfulAPIService.Controllers;
using RESTfulAPIService.Interfaces;
using RESTfulAPIService.Models;
using Xunit;
using Xunit.Abstractions;

namespace RESTfullAPIService.CotrollersTests
{
    public class UserControllerTest
    {
        private UserController _userController;
        private readonly ITestOutputHelper _testOutputHelper;
        public UserControllerTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

//        private UserController GetUserControllerer(ITestOutputHelper testOutputHelper)
//        {
//            ILogger<UserController> loggerForController = new XunitLogger<UserController>(testOutputHelper);
//            var config = new Mock<IConfiguration>();
//            
//            ITest test = (ITest) _testOutputHelper.GetType()
//                .GetField("test", BindingFlags.NonPublic | BindingFlags.Instance)
//                .GetValue(_testOutputHelper);
//            
//            var context = GetContext(test.TestCase.DisplayName);
//            return new UserController(IUserRepository iur);
//        }

        [Fact]
        public void GetAllTest()
        {
            // Arrange - устанавливает начльные условия для выполнения теста
            // создаем заглушку и устанавливаем код 200 после завершения выполнения операции
            var iurMockCode = new Mock<IUserRepository>();
            iurMockCode.Setup(repo => repo.GetAll());

            var testCode = 200;
            
            // Act - выполняет тест
            var result = iurMockCode.Object.GetAll().Status;
            
            Console.WriteLine(result.ToString());
            Console.WriteLine(iurMockCode.ToString());

            // Assert - верифицирует результат выполнения теста
            Assert.Equal(testCode.ToString(), result.ToString());
        }
    }
}