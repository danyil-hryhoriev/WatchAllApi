using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WatchAllApi.Interfaces.Repositories;
using WatchAllApi.Models;

namespace WatchAllApi.Repositories
{
    /// <summary>
    /// Manages shows in the database
    /// </summary>
    public class ShowRepository : MongoRepositoryBase<ShowModel>, IShowRepository
    {
        /// <summary>
        /// Constructor of ShowRepository
        /// </summary>
        /// <param name="settings"></param>
        public ShowRepository(IOptions<MongoDbConfiguration> settings) : base(settings)
        {
        }

        /// <summary>
        /// Collection name where will be stored shows
        /// </summary>
        public override string CollectionName => "shows";

        /// <summary>
        /// Returns top shows by rating
        /// </summary>
        /// <param name="name"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<List<ShowModel>> GetFiltered(string name, int count)
        {
            FilterDefinition<ShowModel> filter = Builders<ShowModel>.Filter.Empty;
            filter &= Builders<ShowModel>.Filter.Where(x => x.Name.Contains(name));
            var res = MongoDatabase.GetCollection<ShowModel>(CollectionName).Find(filter).Limit(count);
            return await res.ToListAsync();
        }
    }
}
