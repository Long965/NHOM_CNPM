using EventFlowerExchange.Repositories.Entities;
using EventFlowerExchange.services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [HttpPut("update-profile")]
        [Authorize] // Đảm bảo người dùng đã đăng nhập
        public async Task<IActionResult> UpdateUserProfile([FromBody] UserProfile userProfile)
        {
            // Lấy userId từ JWT Token
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized(new { message = "User ID not found in token." });
            }

            // Kiểm tra nếu dữ liệu userProfile không hợp lệ
            if (userProfile == null)
            {
                return BadRequest(new { message = "Profile data is missing." });
            }

            // Gắn `userId` vào đối tượng `userProfile`
            userProfile.UserId = userId;

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

        [HttpPost("createUserProfile")]
        [Authorize] // Chỉ người dùng đã đăng nhập mới được phép truy cập
        public async Task<IActionResult> CreateUserProfile([FromBody] UserProfile userProfile)
        {
            try
            {
                // Lấy UserId từ JWT Token
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userIdClaim))
                {
                    return Unauthorized(new { message = "User ID not found in token." });
                }

                // Chuyển đổi UserId từ string sang int
                var userId = int.Parse(userIdClaim);

                // Gán UserId từ JWT vào đối tượng userProfile
                userProfile.UserId = userId;

                // Kiểm tra nếu hồ sơ đã tồn tại
                var existingProfile = await _userProfileService.GetUserProfileByUserIdAsync(userId);
                if (existingProfile != null)
                {
                    return BadRequest(new { message = "User profile already exists." });
                }

                // Gọi service để lưu hồ sơ
                await _userProfileService.CreateUserProfileAsync(userProfile);

                return Ok(new { message = "User profile created successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while creating the profile: {ex.Message}" });
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
