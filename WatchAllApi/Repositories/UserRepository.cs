using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using WatchAllApi.Interfaces;
using WatchAllApi.Interfaces.Repositories;
using WatchAllApi.Models;

namespace WatchAllApi.Repositories
{

    public class UserRepository: MongoRepositoryBase<UserProfile>, IUserRepository
    {
        public UserRepository(IOptions<MongoDbConfiguration> settings) : base(settings)
        {
        }

        public override string CollectionName => "users";

        public async Task<UserProfile> FindByLogin(string login)
        {
            var filter = new BsonDocument("login", login);
            var cursor = await MongoDatabase.GetCollection<UserProfile>(CollectionName)
                .FindAsync(filter);

            return await cursor.FirstOrDefaultAsync();
        }

        public async Task<UserProfile> FindByEmail(string email)
        {
            var filter = new BsonDocument("login", email);
            var cursor = await MongoDatabase.GetCollection<UserProfile>(CollectionName)
                .FindAsync(filter);

            return await cursor.FirstOrDefaultAsync();
        }
    }
}
