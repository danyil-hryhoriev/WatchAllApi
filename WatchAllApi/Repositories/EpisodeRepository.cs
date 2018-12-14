using Microsoft.Extensions.Options;
using WatchAllApi.Interfaces.Repositories;
using WatchAllApi.Models;

namespace WatchAllApi.Repositories
{

    public class EpisodeRepository : MongoRepositoryBase<EpisodeModel>, IEpisodeRepository
    {
        public EpisodeRepository(IOptions<MongoDbConfiguration> settings) : base(settings)
        {
        }

        public override string CollectionName => "episodes";
    }
}
