using EventFlowerExchange.Repositories.Interfaces;
using EventFlowerExchange.services.DTO;
using EventFlowerExchange.services.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventFlowerExchange.WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // Đăng ký người dùng
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            try
            {
                var user = await _userService.RegisterAsync(userDto);
                return Ok(new { message = "User registered successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Đăng nhập người dùng
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto) // Cập nhật tham số thành LoginDto
        {
            if (loginDto == null || string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Password))
            {
                return BadRequest(new { message = "Email and password are required." });
            }


            try
            {
                var token = await _userService.LoginAsync(loginDto);
                return Ok(new
                {
                    message = "Login successfully",
                    token
                });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { message = "Invalid email or password." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred during login.", error = ex.Message });
            }
        }
    }
}
