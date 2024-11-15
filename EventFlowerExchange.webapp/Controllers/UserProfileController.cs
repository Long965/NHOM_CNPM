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
            // Kiểm tra nếu dữ liệu userProfile không hợp lệ
            if (userProfile == null)
            {
                return BadRequest(new { message = "Profile data is missing." });
            }

            // Kiểm tra sự khớp giữa userId trong URL và userId trong đối tượng userProfile
            if (userProfile.UserId != userId)
            {
                return BadRequest(new { message = "User ID mismatch." });
            }

            try
            {
                // Gọi service để cập nhật hồ sơ người dùng
                await _userProfileService.UpdateUserProfileAsync(userProfile);

                // Trả về thông báo thành công
                return Ok(new { message = "User profile updated successfully." });
            }
            catch (Exception ex)
            {
                // Trường hợp có lỗi xảy ra trong quá trình cập nhật
                return StatusCode(500, new { message = $"An error occurred while updating the profile: {ex.Message}" });
            }
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
