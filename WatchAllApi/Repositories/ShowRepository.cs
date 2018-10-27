using Microsoft.Extensions.Options;
using WatchAllApi.Interfaces;
using WatchAllApi.Models;

namespace WatchAllApi.Repositories
{

    public class ShowRepository: MongoRepositoryBase<ShowModel>, IShowRepository
    {
        public ShowRepository(IOptions<MongoDbConfiguration> settings) : base(settings)
        {
        }

        public override string CollectionName => "shows";
    }
}
