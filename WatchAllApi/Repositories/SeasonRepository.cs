using Microsoft.Extensions.Options;
using WatchAllApi.Interfaces.Repositories;
using WatchAllApi.Models;

namespace WatchAllApi.Repositories
{
    /// <summary>
    /// Manages seasons in the database
    /// </summary>
    public class SeasonRepository : MongoRepositoryBase<SeasonModel>, ISeasonRepository
    {
        /// <summary>
        /// Constructor of SeasonRepository
        /// </summary>
        /// <param name="settings"></param>
        public SeasonRepository(IOptions<MongoDbConfiguration> settings) : base(settings)
        {
        }

        /// <summary>
        /// Collection name where will be stored seasons
        /// </summary>
        public override string CollectionName => "seasons";
    }
}

