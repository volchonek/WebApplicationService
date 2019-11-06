using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RESTfulAPIService.Controllers;
using RESTfulAPIService.Interfaces;
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

        // TODO: Добавить логирование для тестов
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
        public async Task GetAllTest()
        {
            // Arrange - устанавливает начальные условия для выполнения теста
            // создаем заглушку для контроллера эмулируя пработу с репозиторием  
            var iurMockCode = new Mock<IUserRepository>();
            iurMockCode.Setup(repo => repo.GetAll());
            // создаем экземпляр контроллера 
            var controller = new UserController(iurMockCode.Object);

            // Act - выполняет тест
            var actionResult = await controller.GetAll();

            // Assert - верифицирует результат выполнения теста
            // проверяем вовращаемый тип, должен быть OkObjectResult
            var result = Assert.IsType<OkObjectResult>(actionResult);
            // проверяем статус код, должен быть 200.
            Assert.Equal(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, result.StatusCode);
        }
    }
}