using Microsoft.Extensions.Options;
using WatchAll.Api.Interfaces.Repositories;
using WatchAll.Api.Models;

namespace WatchAll.Api.Repositories
{

    /// <summary>
    /// Manages channels in the database
    /// </summary>
    public class ChannelRepository: MongoRepositoryBase<ChannelModel>, IChannelRepository
    {
        /// <summary>
        /// Constructor of ChanelRepository
        /// </summary>
        /// <param name="settings"></param>
        public ChannelRepository(IOptions<MongoDbConfiguration> settings) : base(settings)
        {
        }

        /// <summary>
        /// Collection name where will be stored channels
        /// </summary>
        public override string CollectionName => "chanels";
    }
}
