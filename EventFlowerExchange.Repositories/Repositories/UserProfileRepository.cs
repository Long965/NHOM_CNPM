using EventFlowerExchange.Repositories.Entities;
using EventFlowerExchange.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlowerExchange.Repositories.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly EventFlowerExchangeContext _context;

        public UserProfileRepository(EventFlowerExchangeContext context)
        {
            _context = context;
        }

        public async Task<UserProfile> GetByUserIdAsync(int userId)
        {
            return await _context.UserProfiles.FirstOrDefaultAsync(up => up.UserId == userId);
        }

        public async Task UpdateAsync(UserProfile userProfile)
        {
            _context.UserProfiles.Update(userProfile);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByUserIdAsync(int userId)
        {
            var userProfile = await GetByUserIdAsync(userId);
            if (userProfile != null)
            {
                _context.UserProfiles.Remove(userProfile);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddUserProfileAsync(UserProfile userProfile)
        {
            if (userProfile == null)
            {
                throw new ArgumentNullException(nameof(userProfile), "User profile cannot be null.");
            }

            try
            {
                // Thêm hồ sơ người dùng vào context
                _context.UserProfiles.Add(userProfile);
                
                // Lưu thay đổi vào cơ sở dữ liệu
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Xử lý lỗi trong quá trình lưu dữ liệu
                throw new InvalidOperationException("An error occurred while adding the user profile.", ex);
            }
        }

    }

}
