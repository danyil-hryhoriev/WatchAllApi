using Microsoft.Extensions.Options;
using WatchAllApi.Interfaces.Repositories;
using WatchAllApi.Models;

namespace WatchAllApi.Repositories
{
    /// <summary>
    /// Manages episodes in the database
    /// </summary>
    public class EpisodeRepository : MongoRepositoryBase<EpisodeModel>, IEpisodeRepository
    {
        /// <summary>
        /// Constructor of EpisodeRepository
        /// </summary>
        /// <param name="settings"></param>
        public EpisodeRepository(IOptions<MongoDbConfiguration> settings) : base(settings)
        {
        }

        /// <summary>
        /// Collection name where will be stored episodes
        /// </summary>
        public override string CollectionName => "episodes";
    }
}
