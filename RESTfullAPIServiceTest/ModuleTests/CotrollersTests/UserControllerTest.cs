using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RESTfulAPIService.Controllers;
using RESTfulAPIService.Interfaces;
using RESTfulAPIService.Models;
using Xunit;
using Xunit.Abstractions;
using FluentAssertions;

//using AutoFixture;
//using System.Diagnostics.CodeAnalysis;

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

        // GetAll
        [Fact]
        public async Task Controller_GetAll_ReturnFullListUsers()
        {
            // Arrange - устанавливает начальные условия для выполнения теста, подготовка данных
            var testList = new List<User>()
            {
                new User()
                {
                    Id = Guid.Parse("960925df-9161-456d-96f0-8f21f3424ef9"),
                    Name = "user1"
                },
                new User()
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
            var  value = okObjectResult.Value;
            
            value.Should().BeEquivalentTo(testList);
        }
        
        [Fact]
        public async Task Controller_GetAll_ReturnEmptyListUsers()
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
            var  value = okObjectResult.Value;

            value.Should().BeEquivalentTo(testList);
        }

        // GetByGuid
        [Fact]
        public async Task Controller_GeByGuid_ReturnUser()
        {
            // Arrange
            var testData = new User()
            {
                Id = new Guid("960925df-9161-456d-96f0-8f21f3424ef9"),
                Name = "user"
            };
            
            var mock = new Mock<IUserRepository>();
            
            mock.Setup(repository => repository.GetByGuid(new Guid("960925df-9161-456d-96f0-8f21f3424ef9")))
                .ReturnsAsync(await Task.FromResult(
                    new User()
                    {
                        Id = new Guid("960925df-9161-456d-96f0-8f21f3424ef9"),
                        Name = "user"
                    }));

            var controller = new UserController(mock.Object);

            // Act
            var result = await controller.GetByGuid(new Guid("960925df-9161-456d-96f0-8f21f3424ef9"));
            
            // Assert
            var  okObjectResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            var resultData = okObjectResult.Value as User;

            testData.Should().BeEquivalentTo(resultData);
        }

        [Fact]
        public async Task Controller_GeByGuid_ReturnBadRequest()
        {
            // Arrange
            var goodId = new Guid("960925df-9161-456d-96f0-8f21f3424ef9");
            var badId = new Guid("960925df-9161-456d-96f0-8f21f3424eff");
            
            var mock = new Mock<IUserRepository>();
            
            mock.Setup(repository => repository.GetByGuid(goodId))
                .ReturnsAsync(await Task.FromResult(
                    new User()
                    {
                        Id =  goodId,
                        Name = "user"
                    }));

            var controller = new UserController(mock.Object);

            // Act
            var result = await controller.GetByGuid(badId);
            
            // Assert
            Assert.IsAssignableFrom<BadRequestResult>(result);
        }
        
        // GetUserByName
        [Fact]
        public async Task Controller_GetByName_ReturnFullListUsers()
        {
            // Arrange
            var testName = "David";
            var pattern = $"^[{testName}]\\w+";
            
            var testList = new List<User>()
            {
                new User()
                {
                    Id = new Guid("960925df-9161-456d-96f0-8f21f3424ef9"),
                    Name = "David"
                },
                new User()
                {
                    Id = new Guid("960925df-9161-456d-96f0-8f21f3424eff"),
                    Name = "david"
                },
                new User()
                {
                    Id = new Guid("960925df-9161-456d-96f0-8f21f3424ef0"),
                    Name = "user"
                }
            };
            
            var equivalentList = new List<User>()
            {
                new User()
                {
                    Id = new Guid("960925df-9161-456d-96f0-8f21f3424ef9"),
                    Name = "David"
                },
                new User()
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
            var  value = okObjectResult.Value;

            value.Should().BeEquivalentTo(equivalentList);
        }
        
        [Fact]
        public async Task Controller_GetByName_ReturnEmptyListUsers()
        {
            // Arrange
            var testName = "David";
            var pattern = $"^[{testName}]\\w+";
            var testList = new List<User>();
            var mock = new Mock<IUserRepository>();
            
            mock.Setup(repository => repository.GetByName(testName))
                .ReturnsAsync(await Task.FromResult( testList.FindAll(x => Regex.IsMatch(x.Name, pattern))));

            var controller = new UserController(mock.Object);
            
            // Act
            var result = await controller.GetByName(testName);
            
            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var value = okObjectResult.Value;
            
            value.Should().BeEquivalentTo(testList);
        }

        [Fact]
        public async Task Controller_Create_ReturnCreated()
        {
            // Arrange
            var testData = new User()
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
        public async Task Controller_Create_ReturnConflict()
        {
            // Arrange
            var testData = new User()
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
        public async Task Controller_Update_ReturnOk()
        {
            // Arrange
            var testData = new User()
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
        public async Task Controller_Delete_ReturnOk()
        {
            // Arrange
            var testId = new Guid("960925df-9161-456d-96f0-8f21f3424ef9");
            var mock = new Mock<IUserRepository>();
            
            mock.Setup(repository => repository.Delete(testId))
                .ReturnsAsync(true);
            
            var controller = new UserController(mock.Object);

            // Act
            var result = await controller.Delete(testId);
            
            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
        
        
        [Fact]
        public async Task Controller_Delete_ReturnBadRequest()
        {
            // Arrange
            var goodId = new Guid("960925df-9161-456d-96f0-8f21f3424ef9");
            var badId = new Guid("960925df-9161-456d-96f0-8f21f3424eff");
            
            var mock = new Mock<IUserRepository>();
            
            mock.Setup(repository => repository.Delete(goodId))
                .ReturnsAsync(true);
            
            var controller = new UserController(mock.Object);

            // Act
            var result = await controller.Delete(badId);
            
            // Assert
            Assert.IsType<BadRequestResult>(result);

        }
        
        // TODO: BadRequest
//        [Fact]
//        public async Task Controller_Update_ReturnBadRequest()
//        {
//            // Arrange
//            var testData = new User()
//            {
//                Id = new Guid("960925df-9161-456d-96f0-8f21f3424ef9"),
//                Name = "user"
//            };
//
//            var mock = new Mock<IUserRepository>();
//            
//            mock.Setup(repository => repository.Update(testData))
//                .ReturnsAsync(true);
//            
//            var controller = new UserController(mock.Object);
//
//            // Act
//            var result = await controller.Create(testData);
//            
//            // Assert
//            Assert.IsType<BadRequestResult>(result);
//        }

        // TODO: как проверить 400-й код ??
//        [Fact]
//        public async Task Controller_Create_ReturnBadRequest()
//        {
//            // Arrange
//            var testData = new User()
//            {
//                Id = new Guid("960925df-9161-456d-96f0-8f21f3424ef9"),
//                Name = "user"
//            };
//            
//            var mock = new Mock<IUserRepository>();
//
//             mock.Setup(repository => repository.Create(testData))
//                            .ReturnsAsync(false);
//            
//            var controller = new UserController(mock.Object);
//
//            // Act
//            var result = await controller.Create(testData);
//            
//            // Assert
//            Assert.IsType<BadRequestResult>(result);
//        }
        
        // TODO: доделать тесты на BadRequest
//        [Theory]
//        [MemberData(nameof(BadRequestData))]
//        public async Task Controller_GeByGuid_ReturnBadRequest(string id, Exception exception)
//        {
//            // Arrange
//            var guid = new Guid(id);
//
//            var mock = new Mock<IUserRepository>();
//            mock.Setup(repositoy => repositoy.GetByGuid(guid))
//                .Returns((Task<User>) Task.FromException(exception));
//
//            var contoller = new UserController(mock.Object);
//
//            // Act
//            // var result = await contoller.GetByGuid(guid);
//
//            // Assert
//            await Assert.ThrowsAnyAsync<ArgumentException>(() => contoller.GetByGuid(guid));
//        }
//
//        public static IEnumerable<object[]> BadRequestData
//        {
//            get
//            {
//                return new[]
//                {
//                    new object[]
//                    {
//                        "960925df-9161-456d-96f0-!@#$%^&*()<>", new ArgumentException()
//                    },
//                    new object[]
//                    {
//                        "960925df-9161-456d-96f0-8f21f3424ef", new ArgumentException()
//                    },
//                    new object[]
//                    {
//                        "", new ArgumentException()
//                    }
//                };
//            }
//        }
    }
}