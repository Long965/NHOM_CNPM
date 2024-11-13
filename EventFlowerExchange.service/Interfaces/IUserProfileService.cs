using EventFlowerExchange.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlowerExchange.services.Interfaces
{
    public interface IUserProfileService
    {
        Task<UserProfile> GetUserProfileByUserIdAsync(int userId);
        Task UpdateUserProfileAsync(UserProfile userProfile);
        Task DeleteUserProfileAsync(int userId);
    }
}
