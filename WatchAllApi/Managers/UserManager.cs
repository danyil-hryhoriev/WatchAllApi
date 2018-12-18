using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WatchAllApi.Interfaces.Managers;
using WatchAllApi.Interfaces.Repositories;
using WatchAllApi.Models;

namespace WatchAllApi.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<UserProfile> GetByIdAsync(string id)
        {
            return _userRepository.FindAsync(id);
        }

        public Task<UserProfile> GetByLoginAsync(string login)
        {
            return _userRepository.FindByLogin(login);
        }

        public Task<UserProfile> GetByEmailAsync(string email)
        {
            return _userRepository.FindByEmail(email);
        }

        public Task<List<UserProfile>> GetAllUsers()
        {
            return _userRepository.SelectAllAsync();
        }

        public Task InsertProfileAsync(UserProfile userProfile)
        {
            return _userRepository.InsertAsync(userProfile);
        }

        public Task UpdateProfileAsync(UserProfile userProfile)
        {
            return _userRepository.ReplaceByIdAsync(userProfile.Id, userProfile);
        }

        public Task<bool> DeleteProfileAsync(string id)
        {
            return _userRepository.DeleteByIdAsync(id);
        }

        public string ValidatePassword(string password)
        {
            throw new NotImplementedException();
        }
    }
}
