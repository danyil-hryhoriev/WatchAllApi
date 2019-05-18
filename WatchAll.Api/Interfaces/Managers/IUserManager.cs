using System.Collections.Generic;
using System.Threading.Tasks;
using WatchAll.Api.Models;

namespace WatchAll.Api.Interfaces.Managers
{
    /// <summary>
    /// Managing of users and business logic for them
    /// </summary>
    public interface IUserManager
    {
        /// <summary>
        /// Returns model of user by id
        /// </summary>
        /// <param name="id">Id of existing user</param>
        /// <returns></returns>
        Task<UserProfile> GetById(string id);

        /// <summary>
        /// Returns model of user by user login
        /// </summary>
        /// <param name="login">Login of existing user</param>
        /// <returns></returns>
        Task<UserProfile> GetByLogin(string login);

        /// <summary>
        /// Returns model of user by email
        /// </summary>
        /// <param name="email">Email of existing user</param>
        /// <returns></returns>
        Task<UserProfile> GetByEmail(string email);

        /// <summary>
        /// Returns all existing shows in DB
        /// </summary>
        /// <returns></returns>
        Task<List<UserProfile>> GetAllUsers();

        /// <summary>
        /// Inserts new user in Db
        /// </summary>
        /// <param name="userProfile">Model of user that will be inserted</param>
        /// <returns></returns>
        Task InsertProfileAsync(UserProfile userProfile);

        /// <summary>
        /// Updates existing user in Db
        /// </summary>
        /// <param name="userProfile">Model of user that will be updated</param>
        /// <returns></returns>
        Task UpdateProfileAsync(UserProfile userProfile);

        /// <summary>
        /// Deletes existing user in Db
        /// </summary>
        /// <param name="id">Id of user that will be deleted</param>
        /// <returns></returns>
        Task<bool> DeleteProfileAsync(string id);
    }
}
