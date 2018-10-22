using Microsoft.Extensions.Configuration;
using WatchAllApi.Interfaces;

namespace WatchAllApi.Models
{
    public class MongoDbConfiguration : IDbConfiguration
    {
        public MongoDbConfiguration(IConfiguration configuration)
        {
            if (configuration != null)
            {
                ConnectionString = configuration["MongoConnection:ConnectionString"];
                Database = configuration["MongoConnection:Database"];
            }
        }

        public MongoDbConfiguration()
        {
            
        }

        public string ConnectionString { get; set; }
        public string Database { get; set; }
    }
}
