using System.Collections.Generic;
using System.Threading.Tasks;
using WatchAll.Api.Models;
using WatchAll.Api.Models.Dto;

namespace WatchAll.Api.Interfaces.Managers
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
        Task<List<ShowModel>> GetFilteredShows(string name, int count);

        /// <summary>
        /// Get model of show with all fields from ShowModel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ShowDtoModel> GetDtoShow(ShowModel model);

        /// <summary>
        /// Get model of show with all fields from ShowModel
        /// </summary>
        /// <param name="showId">Id of show</param>
        /// <returns></returns>
        Task<ShowDtoModel> GetDtoShow(string showId);
    }
}
