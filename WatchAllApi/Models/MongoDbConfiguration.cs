using Microsoft.Extensions.Configuration;
using WatchAllApi.Interfaces;

namespace WatchAllApi.Models
{
    /// <summary>
    /// Configuration of DB
    /// </summary>
    public class MongoDbConfiguration : IDbConfiguration
    {
        /// <summary>
        /// Constructor with parameters for configuration
        /// </summary>
        /// <param name="configuration"></param>
        public MongoDbConfiguration(IConfiguration configuration)
        {
            if (configuration != null)
            {
                ConnectionString = configuration["MongoConnection:ConnectionString"];
                Database = configuration["MongoConnection:Database"];
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public MongoDbConfiguration()
        {
            
        }

        /// <summary>
        /// Connection string
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Database name
        /// </summary>
        public string Database { get; set; }
    }
}
