using Microsoft.Extensions.Options;
using WatchAllApi.Interfaces.Repositories;
using WatchAllApi.Models;

namespace WatchAllApi.Repositories
{

    public class SeasonRepository : MongoRepositoryBase<SeasonModel>, ISeasonRepository
    {
        public SeasonRepository(IOptions<MongoDbConfiguration> settings) : base(settings)
        {
        }

        public override string CollectionName => "seasons";
    }
}
