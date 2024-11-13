using EventFlowerExchange.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlowerExchange.Repositories.Interfaces
{
    public interface IUserProfileRepository
    {
        Task<UserProfile> GetByUserIdAsync(int userId);
        Task UpdateAsync(UserProfile userProfile);
        Task DeleteByUserIdAsync(int userId);
    }
}
