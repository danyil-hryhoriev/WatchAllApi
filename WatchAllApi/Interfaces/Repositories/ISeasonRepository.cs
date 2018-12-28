using System.Collections.Generic;
using System.Threading.Tasks;
using WatchAllApi.Models;

namespace WatchAllApi.Interfaces.Repositories
{
    /// <summary>
    /// Manages seasons in the database
    /// </summary>
    public interface ISeasonRepository: IRepositoryBase<SeasonModel>
    {
        /// <summary>
        /// Get list of season according to correspond show
        /// </summary>
        /// <param name="showId"></param>
        /// <returns></returns>
        Task<List<SeasonModel>> FindByShowId(string showId);
    }
}
