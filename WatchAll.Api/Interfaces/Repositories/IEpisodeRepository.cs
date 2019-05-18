using System.Collections.Generic;
using System.Threading.Tasks;
using WatchAll.Api.Models;

namespace WatchAll.Api.Interfaces.Repositories
{
    /// <summary>
    /// Manages episodes in the database
    /// </summary>
    public interface IEpisodeRepository : IRepositoryBase<EpisodeModel>
    {
        /// <summary>
        /// Get list of episodes according to correspond show
        /// </summary>
        /// <param name="seasonId"></param>
        /// <returns></returns>
        Task<List<EpisodeModel>> FindBySeasonId(string seasonId);
    }
}
