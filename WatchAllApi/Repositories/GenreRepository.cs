using Microsoft.Extensions.Options;
using WatchAllApi.Interfaces.Repositories;
using WatchAllApi.Models;

namespace WatchAllApi.Repositories
{

    public class GenreRepository: MongoRepositoryBase<GenreModel>, IGenreRepository
    {
        public GenreRepository(IOptions<MongoDbConfiguration> settings) : base(settings)
        {
        }

        public override string CollectionName => "genres";
    }
}
