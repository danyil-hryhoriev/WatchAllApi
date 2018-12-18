using Microsoft.Extensions.Options;
using WatchAllApi.Interfaces.Repositories;
using WatchAllApi.Models;

namespace WatchAllApi.Repositories
{

    public class ChanelRepository: MongoRepositoryBase<ChanelModel>, IChanelRepository
    {
        public ChanelRepository(IOptions<MongoDbConfiguration> settings) : base(settings)
        {
        }

        public override string CollectionName => "chanels";
    }
}
