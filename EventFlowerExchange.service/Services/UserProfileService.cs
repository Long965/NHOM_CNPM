using EventFlowerExchange.Repositories.Entities;
using EventFlowerExchange.Repositories.Interfaces;
using EventFlowerExchange.services.Interfaces;
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
            await _userProfileRepository.UpdateAsync(userProfile);
        }

        public async Task DeleteUserProfileAsync(int userId)
        {
            await _userProfileRepository.DeleteByUserIdAsync(userId);
        }
    }

}
