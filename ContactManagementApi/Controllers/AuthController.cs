using ContactManagementApi.BusinessLogic.DTOs;
using ContactManagementApi.BusinessLogic.Interfaces;
using ContactManagementApi.BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ContactManagementApi.Controllers
{
    [Route("User/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }
        [HttpPost("Signup")]
        [AllowAnonymous]
        public async Task<IActionResult> Signup([FromBody] UserSignupDto userSignupDto)
        {
            try
            {
                _logger.LogInformation("Signup attempt for username: {Username}", userSignupDto.UserName);

                var account = await _authService.SignupNewUser(userSignupDto.UserName, userSignupDto.Password);

                _logger.LogInformation("Signup successful for user ID: {UserId}, username: {Username}", account.Id, account.Username);

                return Ok(new { account.Id, account.Username });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Signup failed for username: {Username}", userSignupDto.UserName);
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            try
            {
                _logger.LogInformation("Login attempt for username: {Username}", userLoginDto.UserName);

                var token = await _authService.Login(userLoginDto.UserName, userLoginDto.Password);

                if (string.IsNullOrEmpty(token))
                {
                    _logger.LogWarning("Login failed for username: {Username}", userLoginDto.UserName);
                    return Unauthorized("Invalid username or password.");
                }

                _logger.LogInformation("Login successful for username: {Username}", userLoginDto.UserName);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login failed for username: {Username}", userLoginDto.UserName);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllUsers")]
        [Authorize(Roles = "Admin")] //<<roles!!
        public async Task<IActionResult> GetAllUsers()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                _logger.LogWarning("Unauthorized access attempt to GetAllUsers.");
                return Unauthorized();
            }

            try
            {
                _logger.LogInformation("Admin user {AdminId} attempting to get all users.", userId);
                var users = await _authService.GetAllUsersAsync();
                _logger.LogInformation("Admin user {AdminId} successfully retrieved all users.", userId);
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all users by admin user {AdminId}.", userId);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteUser")]
        [Authorize(Roles = "Admin")] //<<roles!!
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                _logger.LogWarning("Unauthorized access attempt to DeleteUser.");
                return Unauthorized();
            }

            try
            {
                _logger.LogInformation("Admin user {AdminId} attempting to delete user {UserId}.", userId, id);
                var isDeleted = await _authService.DeleteUserAsync(id);
                if (!isDeleted)
                {
                    _logger.LogWarning("User {UserId} not found for deletion by admin user {AdminId}.", id, userId);
                    return NotFound("User not found");
                }

                _logger.LogInformation("Admin user {AdminId} successfully deleted user {UserId}.", userId, id);
                return Ok("User deleted");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting user {UserId} by admin user {AdminId}.", id, userId);
                return BadRequest(ex.Message);
            }
        }

    }
}
