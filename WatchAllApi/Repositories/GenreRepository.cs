using Microsoft.Extensions.Options;
using WatchAllApi.Interfaces.Repositories;
using WatchAllApi.Models;

namespace WatchAllApi.Repositories
{
    /// <summary>
    /// Manages genres in the database
    /// </summary>
    public class GenreRepository: MongoRepositoryBase<GenreModel>, IGenreRepository
    {
        /// <summary>
        /// Constructor of GenreRepository
        /// </summary>
        /// <param name="settings"></param>
        public GenreRepository(IOptions<MongoDbConfiguration> settings) : base(settings)
        {
        }

        /// <summary>
        /// Collection name where will be stored genres
        /// </summary>
        public override string CollectionName => "genres";
    }
}
