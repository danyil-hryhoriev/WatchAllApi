using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
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

        /// <summary>
        /// Get list of season according to correspond show
        /// </summary>
        /// <param name="showId"></param>
        /// <returns></returns>
        public async Task<List<SeasonModel>> FindByShowId(string showId)
        {
            var filter = new BsonDocument("showId", showId);
            var cursor = await MongoDatabase.GetCollection<SeasonModel>(CollectionName)
                .FindAsync(filter);

            return cursor.ToList();
        }
    }
}

