using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Moq;
using RESTfulAPIService.Controllers;
using RESTfulAPIService.Interfaces;
using RESTfulAPIService.Models;
using Xunit;
using Xunit.Abstractions;

//using AutoFixture;
//using System.Diagnostics.CodeAnalysis;

namespace RESTfullAPIService.ModuleTests.CotrollersTests
{
    public class UserControllerTest
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public UserControllerTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        // TODO: Will be add loging for are tests.
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

        // GetAll
        [Fact]
        public async Task GetAll_ReturnOk_FullListUsers()
        {
            // Arrange - устанавливает начальные условия для выполнения теста, подготовка данных
            var testList = new List<User>
            {
                new User
                {
                    Id = Guid.Parse("960925df-9161-456d-96f0-8f21f3424ef9"),
                    Name = "user1"
                },
                new User
                {
                    Id = Guid.Parse("7c6cbbb7-b6db-4663-b12b-df0fd716de06"),
                    Name = "user2"
                }
            };

            var mock = new Mock<IUserRepository>();

            mock.Setup(repository => repository.GetAll()).ReturnsAsync(await Task.FromResult(testList));

            var controller = new UserController(mock.Object);

            // Act - выполняет тест
            var result = await controller.GetAll();

            // Assert - верифицирует результат выполнения теста

            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var value = okObjectResult.Value;

            value.Should().BeEquivalentTo(testList);
        }

        [Fact]
        public async Task GetAll_ReturnOk_EmptyListUsers()
        {
            // Arrange 
            var testList = new List<User>();
            var mock = new Mock<IUserRepository>();

            mock.Setup(repository => repository.GetAll()).ReturnsAsync(await Task.FromResult(testList));

            var controller = new UserController(mock.Object);

            // Act
            var result = await controller.GetAll();

            // Assert 
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var value = okObjectResult.Value;

            value.Should().BeEquivalentTo(testList);
        }

        // GetById
        [Fact]
        public async Task GeByGuid_ReturnOk_User()
        {
            // Arrange
            var testData = new User
            {
                Id = new Guid("960925df-9161-456d-96f0-8f21f3424ef9"),
                Name = "user"
            };

            var mock = new Mock<IUserRepository>();

            mock.Setup(repository => repository.GetById(new Guid("960925df-9161-456d-96f0-8f21f3424ef9")))
                .ReturnsAsync(await Task.FromResult(
                    new User
                    {
                        Id = new Guid("960925df-9161-456d-96f0-8f21f3424ef9"),
                        Name = "user"
                    }));

            var controller = new UserController(mock.Object);

            // Act
            var result = await controller.GetById(new Guid("960925df-9161-456d-96f0-8f21f3424ef9"));

            // Assert
            var okObjectResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            var resultData = okObjectResult.Value as User;

            testData.Should().BeEquivalentTo(resultData);
        }

        [Fact]
        public async Task GeByGuid_ReturnNotFound()
        {
            // Arrange
            var testData = new User
            {
                Id = new Guid("960925df-9161-456d-96f0-8f21f3424ef9"),
                Name = "user"
            };
            
            var id = new Guid("960925df-9161-456d-96f0-8f21f3424eff");


            var mock = new Mock<IUserRepository>();

            mock.Setup(repository => repository.GetById(id))
                .ReturnsAsync(await Task.FromResult(
                    new User
                    {
                        Id = id,
                        Name = "user"
                    }));

            var controller = new UserController(mock.Object);

            // Act
            var result = await controller.GetById(testData.Id);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        // GetUserByName
        [Fact]
        public async Task GetByName_ReturnOk_FullListUsers()
        {
            // Arrange
            var testName = "David";
            var pattern = $"^[{testName}]\\w+";

            var testList = new List<User>
            {
                new User
                {
                    Id = new Guid("960925df-9161-456d-96f0-8f21f3424ef9"),
                    Name = "David"
                },
                new User
                {
                    Id = new Guid("960925df-9161-456d-96f0-8f21f3424eff"),
                    Name = "david"
                },
                new User
                {
                    Id = new Guid("960925df-9161-456d-96f0-8f21f3424ef0"),
                    Name = "user"
                }
            };

            var equivalentList = new List<User>
            {
                new User
                {
                    Id = new Guid("960925df-9161-456d-96f0-8f21f3424ef9"),
                    Name = "David"
                },
                new User
                {
                    Id = new Guid("960925df-9161-456d-96f0-8f21f3424eff"),
                    Name = "david"
                }
            };

            var mock = new Mock<IUserRepository>();

            mock.Setup(repository => repository.GetByName("David"))
                .ReturnsAsync(await Task.FromResult(
                    testList.FindAll(x => Regex.IsMatch(x.Name, pattern))));

            var controller = new UserController(mock.Object);

            // Act
            var result = await controller.GetByName(testName);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var value = okObjectResult.Value;

            value.Should().BeEquivalentTo(equivalentList);
        }

        [Fact]
        public async Task GetByName_ReturnOk_EmptyListUsers()
        {
            // Arrange
            var testName = "David";
            var pattern = $"^[{testName}]\\w+";
            var testList = new List<User>();
            var mock = new Mock<IUserRepository>();

            mock.Setup(repository => repository.GetByName(testName))
                .ReturnsAsync(await Task.FromResult(testList.FindAll(x => Regex.IsMatch(x.Name, pattern))));

            var controller = new UserController(mock.Object);

            // Act
            var result = await controller.GetByName(testName);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var value = okObjectResult.Value;

            value.Should().BeEquivalentTo(testList);
        }

        [Fact]
        public async Task Create_ReturnOk_User()
        {
            // Arrange
            var testData = new User
            {
                Id = new Guid("960925df-9161-456d-96f0-8f21f3424ef9"),
                Name = "user"
            };

            var mock = new Mock<IUserRepository>();

            mock.Setup(repository => repository.Create(testData))
                .ReturnsAsync(true);

            var controller = new UserController(mock.Object);

            // Act
            var result = await controller.Create(testData);

            // Assert
            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public async Task Create_ReturnOk_Conflict()
        {
            // Arrange
            var testData = new User
            {
                Id = new Guid("960925df-9161-456d-96f0-8f21f3424ef9"),
                Name = "user"
            };

            var mock = new Mock<IUserRepository>();
            var controller = new UserController(mock.Object);

            // Act
            var result = await controller.Create(testData);

            // Assert
            Assert.IsType<ConflictObjectResult>(result);
        }

        [Fact]
        public async Task Update_ReturnOk()
        {
            // Arrange
            var testData = new User
            {
                Id = new Guid("960925df-9161-456d-96f0-8f21f3424ef9"),
                Name = "user"
            };

            var mock = new Mock<IUserRepository>();

            mock.Setup(repository => repository.Update(testData))
                .ReturnsAsync(true);

            var controller = new UserController(mock.Object);

            // Act
            var result = await controller.Update(testData);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Update_ReturnBadRequest()
        {
            // Arrange
            var testData = new User
            {
                Id = new Guid("960925df-9161-456d-96f0-8f21f3424ef9"),
                Name = "user"
            };

            var mock = new Mock<IUserRepository>();

            mock.Setup(repository => repository.Update(testData))
                .ReturnsAsync(true);

            var controller = new UserController(mock.Object);

            // Act
            var result = await controller.GetById(testData.Id);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        // TODO: BadRequest
        [Fact]
        public async Task Update_ReturnNotFound()
        {
            // Arrange
            var testData = new User
            {
                Id = new Guid("960925df-9161-456d-96f0-8f21f3424ef9"),
                Name = "user"
            };
            
            var fakeData = new User
            {
                Id = new Guid("960925df-9161-456d-96f0-8f21f3424eff"),
                Name = "pass"
            };

            var mock = new Mock<IUserRepository>();

            mock.Setup(repository => repository.Update(testData))
                .ReturnsAsync(true);

            var controller = new UserController(mock.Object);
            
            // Act
            var result = await controller.Update(fakeData);
            
            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnOk()
        {
            // Arrange
            var testData = new User
            {
                Id = new Guid("960925df-9161-456d-96f0-8f21f3424ef9"),
                Name = "user"
            };
            
            var mock = new Mock<IUserRepository>();

            mock.Setup(repository => repository.Delete(testData.Id))
                .ReturnsAsync(true);

            var controller = new UserController(mock.Object);

            // Act
            var result = await controller.Delete(testData.Id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnNotFound()
        {
            // Arrange
            var testData = new User
            {
                Id = new Guid("960925df-9161-456d-96f0-8f21f3424ef9"),
                Name = "user"
            };

            var mock = new Mock<IUserRepository>();

            mock.Setup(repository => repository.Delete(testData.Id))
                .ReturnsAsync(true);

            var controller = new UserController(mock.Object);

            // Act
            var result = await controller.Delete(new Guid("960925df-9161-456d-96f0-8f21f3424fff"));

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnBadRequest()
        {
            // Arrange
            var testData = new User
            {
                Id = new Guid("960925df-9161-456d-96f0-8f21f3424ef9"),
                Name = "user"
            };

            var mock = new Mock<IUserRepository>();

            mock.Setup(repository => repository.Delete(testData.Id))
                .ReturnsAsync(true);

            var controller = new UserController(mock.Object);

            // Act
            var result = await controller.GetById(testData.Id);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}