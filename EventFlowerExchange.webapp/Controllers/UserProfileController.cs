using EventFlowerExchange.Repositories.Entities;
using EventFlowerExchange.services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventFlowerExchange.WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;

        public UserProfileController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        // Lấy thông tin hồ sơ người dùng
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserProfile(int userId)
        {
            var userProfile = await _userProfileService.GetUserProfileByUserIdAsync(userId);
            if (userProfile == null)
            {
                return NotFound(new { message = "User profile not found." });
            }
            return Ok(userProfile);
        }

        // Cập nhật hồ sơ người dùng
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUserProfile(int userId, [FromBody] UserProfile userProfile)
        {
            if (userProfile == null || userProfile.UserId != userId)
            {
                return BadRequest(new { message = "Invalid profile data or user ID mismatch." });
            }

            await _userProfileService.UpdateUserProfileAsync(userProfile);
            return Ok(new { message = "User profile updated successfully." });
        }

        // Xóa hồ sơ người dùng
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUserProfile(int userId)
        {
            await _userProfileService.DeleteUserProfileAsync(userId);
            return Ok(new { message = "User profile deleted successfully." });
        }
    }

}
