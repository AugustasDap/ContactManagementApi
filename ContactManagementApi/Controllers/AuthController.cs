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
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("Signup")]
        [AllowAnonymous]
        public async Task<IActionResult> Signup([FromBody] UserSignupDto userSignupDto)
        {
            try
            {
                var account = await _authService.SignupNewUser(userSignupDto.UserName, userSignupDto.Password);
                return Ok(new { account.Id, account.Username });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            var token = await _authService.Login(userLoginDto.UserName, userLoginDto.Password);

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Invalid username or password.");
            }

            return Ok(new { Token = token });
        }
        [HttpDelete("DeleteUser")]
        [Authorize(Roles = "Admin")] //<<roles!!
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var isDeleted = await _authService.DeleteUserAsync(id);
            if (!isDeleted)
            {
                return NotFound("User not found");
            }
            return Ok("User deleted");
        }

    }
}
