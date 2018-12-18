using System.Collections.Generic;
using System.Threading.Tasks;
using WatchAllApi.Models;

namespace WatchAllApi.Interfaces.Managers
{
    public interface IUserManager
    {
        Task<UserProfile> GetByIdAsync(string id);
        Task<UserProfile> GetByLoginAsync(string login);
        Task<UserProfile> GetByEmailAsync(string email);
        Task<List<UserProfile>> GetAllUsers();

        Task InsertProfileAsync(UserProfile userProfile);
        Task UpdateProfileAsync(UserProfile userProfile);
        Task<bool> DeleteProfileAsync(string id);
        string ValidatePassword(string password);
    }
}
