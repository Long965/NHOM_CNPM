using EventFlowerExchange.Repositories.Entities;
using EventFlowerExchange.Repositories.Interfaces;
using EventFlowerExchange.Repositories.Repositories;
using EventFlowerExchange.services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlowerExchange.services.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepository _userProfileRepository;

        public UserProfileService(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

        public async Task<UserProfile> GetUserProfileByUserIdAsync(int userId)
        {
            return await _userProfileRepository.GetByUserIdAsync(userId);
        }

        public async Task UpdateUserProfileAsync(UserProfile userProfile)
        {
            if (userProfile == null)
                throw new ArgumentNullException(nameof(userProfile));

            try
            {
                var existingUserProfile = await _userProfileRepository.GetByUserIdAsync(userProfile.UserId);

                if (existingUserProfile == null)
                {
                    throw new Exception("User profile not found");
                }

                // Cập nhật các trường thông tin hồ sơ người dùng
                existingUserProfile.PhoneNumber = userProfile.PhoneNumber;
                existingUserProfile.Gender = userProfile.Gender;
                existingUserProfile.DateOfBirth = userProfile.DateOfBirth;
                existingUserProfile.Address = userProfile.Address;

                // Cập nhật đối tượng người dùng trong DbContext
                await _userProfileRepository.UpdateAsync(existingUserProfile);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in UserFrofileService while updating Profile", ex);
            }
            await _userProfileRepository.UpdateAsync(userProfile);
        }

        public async Task DeleteUserProfileAsync(int userId)
        {
            await _userProfileRepository.DeleteByUserIdAsync(userId);
        }
    }

}
