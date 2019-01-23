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

        /// <summary>
        /// Get list of episodes according to correspond show
        /// </summary>
        /// <param name="seasonId"></param>
        /// <returns></returns>
        public async Task<List<EpisodeModel>> FindBySeasonId(string seasonId)
        {
            var filter = new BsonDocument("season_Id", seasonId);
            var cursor =  MongoDatabase.GetCollection<EpisodeModel>(CollectionName);

                var res = await cursor.FindAsync(filter);

            return await res.ToListAsync();
        }
    }
}
