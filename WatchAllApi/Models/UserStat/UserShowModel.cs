using System.Collections.Generic;
using System.Runtime.Serialization;
using WatchAllApi.Enums;

namespace WatchAllApi.Models.UserStat
{
    /// <summary>
    /// User show model.
    /// </summary>
    [DataContract]
    public class UserShowModel
    {
        /// <summary>
        /// Gets or sets the show identifier.
        /// </summary>
        /// <value>The show identifier.</value>
        [DataMember]
        public string ShowId { get; set; }

        /// <summary>
        /// Gets or sets the seasons.
        /// </summary>
        /// <value>The seasons.</value>
        [DataMember]
        public List<UserSeasonModel> Seasons { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        [DataMember]
        public WatchingStatusEnum Status { get; set; }
    }
}
