using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WatchAllApi.Interfaces.Managers;
using WatchAllApi.Interfaces.Repositories;
using WatchAllApi.Models;

namespace WatchAllApi.Managers
{
    /// <summary>
    /// Managing of users and business logic for them
    /// </summary>
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Constructor of UserManager
        /// </summary>
        /// <param name="userRepository"></param>
        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Returns model of user by id
        /// </summary>
        /// <param name="id">Id of existing user</param>
        /// <returns></returns>
        public Task<UserProfile> GetById(string id)
        {
            return _userRepository.FindAsync(id);
        }

        /// <summary>
        /// Returns model of user by user login
        /// </summary>
        /// <param name="login">Login of existing user</param>
        /// <returns></returns>
        public Task<UserProfile> GetByLogin(string login)
        {
            return _userRepository.FindByLogin(login);
        }

        /// <summary>
        /// Returns model of user by email
        /// </summary>
        /// <param name="email">Email of existing user</param>
        /// <returns></returns>
        public Task<UserProfile> GetByEmail(string email)
        {
            return _userRepository.FindByEmail(email);
        }

        /// <summary>
        /// Returns all existing shows in DB
        /// </summary>
        /// <returns></returns>
        public Task<List<UserProfile>> GetAllUsers()
        {
            return _userRepository.SelectAllAsync();
        }

        /// <summary>
        /// Inserts new user in Db
        /// </summary>
        /// <param name="userProfile">Model of user that will be inserted</param>
        /// <returns></returns>
        public Task InsertProfileAsync(UserProfile userProfile)
        {
            return _userRepository.InsertAsync(userProfile);
        }

        /// <summary>
        /// Updates existing user in Db
        /// </summary>
        /// <param name="userProfile">Model of user that will be updated</param>
        /// <returns></returns>
        public Task UpdateProfileAsync(UserProfile userProfile)
        {
            return _userRepository.ReplaceByIdAsync(userProfile.Id, userProfile);
        }

        /// <summary>
        /// Deletes existing user in Db
        /// </summary>
        /// <param name="id">Id of user that will be deleted</param>
        /// <returns></returns>
        public Task<bool> DeleteProfileAsync(string id)
        {
            return _userRepository.DeleteByIdAsync(id);
        }
    }
}
