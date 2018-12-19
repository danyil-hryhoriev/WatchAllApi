using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
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
        /// <param name="countOfEnt">Count of top</param>
        /// <returns></returns>
        public Task<List<ShowModel>> GetFirstTop(int countOfEnt)
        {
            return Task.Run(() => QueryWithFilter(x => x.Rating > 7.9).Take(countOfEnt).ToList());
        }
    }
}
