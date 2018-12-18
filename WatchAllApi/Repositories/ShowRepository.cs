using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using WatchAllApi.Interfaces.Repositories;
using WatchAllApi.Models;

namespace WatchAllApi.Repositories
{

    public class ShowRepository : MongoRepositoryBase<ShowModel>, IShowRepository
    {
        public ShowRepository(IOptions<MongoDbConfiguration> settings) : base(settings)
        {
        }

        public override string CollectionName => "shows";

        public Task<List<ShowModel>> GetFirstTop(int countOfEnt)
        {
            return Task.Run(() => QueryWithFilter(x => x.Rating > 7.9).Take(countOfEnt).ToList());
        }
    }
}
