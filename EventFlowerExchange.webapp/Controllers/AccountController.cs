using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using EventFlowerExchange.Repositories.Interfaces;
using EventFlowerExchange.services.DTO;
using System.Security.Claims;

namespace EventFlowerExchange.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            // Kiểm tra đầu vào ChangePasswordDto
            if (changePasswordDto == null || string.IsNullOrEmpty(changePasswordDto.OldPassword) || string.IsNullOrEmpty(changePasswordDto.NewPassword))
                return BadRequest(new { message = "Invalid password data." });

            // Lấy UserId từ Claim
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized(new { message = "User not authorized." });

            if (!int.TryParse(userIdClaim, out int userId))
                return Unauthorized(new { message = "Invalid user ID in claims." });

            try
            {
                // Gọi UserService để thay đổi mật khẩu
                await _userService.ChangePasswordAsync(userId, changePasswordDto);
                return Ok(new { message = "Password changed successfully." });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "User not found." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while changing the password.", error = ex.Message });
            }
        }
    }
}
