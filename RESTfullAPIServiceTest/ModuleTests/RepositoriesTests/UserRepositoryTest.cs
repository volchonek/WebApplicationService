using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RESTfulAPIService.Controllers;
using RESTfulAPIService.Interfaces;
using RESTfulAPIService.Models;
using Xunit;

namespace RESTfullAPIService.ModuleTests.RepositoriesTests
{
    // TODO:  Will complite user repository tests
    public class UserRepositoryTest
    {
        private List<User> GetTestSessions()
        {
            var sessions = new List<User>();
            sessions.Add(new User()
            {

                Id = new Guid(),
                Name = "Test One"
            });
            sessions.Add(new User()
            {
                Id = new Guid(),
                Name = "Test Two"
            });
            return sessions;
        }

        [Fact]
        public async Task Repository_GetAll_ReturnListUser()
        {
            // Arrange - устанавливает начльные условия для выполнения теста
            var mock = new Mock<IUserRepository>();

            mock.Setup(repository => repository.GetAll())
                .ReturnsAsync(GetTestSessions());

            var controller = new UserController(mock.Object);

            // Act - выполняет тест
            var result = await controller.GetAll();

            // Assert - верефицирует результат выполнения теста
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<User>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }
    }
}