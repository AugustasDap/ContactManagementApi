using ContactManagementApi.BusinessLogic.DTOs;
using ContactManagementApi.BusinessLogic.Interfaces;
using ContactManagementApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Test.Controllers
{
    public class UserControllerTests
    {
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly Mock<ILogger<AuthController>> _loggerMock;
        private readonly AuthController _controller;

        public UserControllerTests()
        {
            _authServiceMock = new Mock<IAuthService>();
            _loggerMock = new Mock<ILogger<AuthController>>();
            _controller = new AuthController(_authServiceMock.Object, _loggerMock.Object);  
        }

        //[Fact]
        //public async Task Signup_ReturnsOkResult_WhenUserIsCreated()
        //{
        //    // Arrange
        //    var userSignupDto = new UserSignupDto { UserName = "testuser", Password = "Password123!" };
        //    var account = new UserDto { Id = Guid.NewGuid(), UserName = "testuser" };

        //    _authServiceMock.Setup(service => service.SignupNewUser(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(account);

        //    // Act
        //    var result = await _controller.Signup(userSignupDto);

        //    // Assert
        //    var okResult = Assert.IsType<OkObjectResult>(result);
        //    var returnValue = Assert.IsType<dynamic>(okResult.Value);
        //    Assert.Equal(account.Id, returnValue.Id);
        //    Assert.Equal(account.UserName, returnValue.Username);
        //}

        [Fact]
        public async Task Signup_ReturnsBadRequest_WhenExceptionIsThrown()
        {
            // Arrange
            var userSignupDto = new UserSignupDto { UserName = "testuser", Password = "Password123!" };

            _authServiceMock.Setup(service => service.SignupNewUser(It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception("Error creating user"));

            // Act
            var result = await _controller.Signup(userSignupDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Error creating user", badRequestResult.Value);
        }

        [Fact]
        public async Task Login_ReturnsOkResult_WhenLoginIsSuccessful()
        {
            // Arrange
            var userLoginDto = new UserLoginDto { UserName = "testuser", Password = "Password123!" };
            var token = "dummyToken";

            _authServiceMock.Setup(service => service.Login(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(token);

            // Act
            var result = await _controller.Login(userLoginDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<dynamic>(okResult.Value);
            Assert.Equal(token, returnValue.Token);
        }

        [Fact]
        public async Task Login_ReturnsUnauthorized_WhenLoginFails()
        {
            // Arrange
            var userLoginDto = new UserLoginDto { UserName = "testuser", Password = "wrongpassword" };

            _authServiceMock.Setup(service => service.Login(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(string.Empty);

            // Act
            var result = await _controller.Login(userLoginDto);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal("Invalid username or password.", unauthorizedResult.Value);
        }
    }
}
