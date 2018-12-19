using System.Collections.Generic;
using System.Threading.Tasks;
using WatchAllApi.Models;

namespace WatchAllApi.Interfaces.Managers
{
    /// <summary>
    /// Managing of shows and business logic for them
    /// </summary>
    public interface IShowManager
    {

        /// <summary>
        /// Returns model of show by id
        /// </summary>
        /// <param name="id">Id of existing show</param>
        /// <returns></returns>
        Task<ShowModel> GetShowById(string id);

        /// <summary>
        /// Returns all existing shows in DB
        /// </summary>
        /// <returns></returns>
        Task<List<ShowModel>> GetAllShows();

        /// <summary>
        /// Inserts new show in Db
        /// </summary>
        /// <param name="showModel">Model of show that will be inserted</param>
        /// <returns></returns>
        Task<ShowModel> InsertShow(ShowModel showModel);

        /// <summary>
        /// Updates existing show in Db
        /// </summary>
        /// <param name="showModel">Model of show that will be updated</param>
        /// <returns></returns>
        Task<ShowModel> UpdateShow(ShowModel showModel);

        /// <summary>
        /// Deletes existing show in Db
        /// </summary>
        /// <param name="id">Id of show that will be deleted</param>
        /// <returns></returns>
        Task RemoveShow(string id);

        /// <summary>
        /// Returns Top-100 shows by rating
        /// </summary>
        /// <returns></returns>
        Task<List<ShowModel>> GetTopShows();

        /// <summary>
        /// Seeds DB by Data from files
        /// </summary>
        /// <returns></returns>
        Task SeedDb();
    }
}
