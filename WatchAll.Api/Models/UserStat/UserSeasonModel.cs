using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WatchAll.Api.Models
{
    /// <summary>
    /// User season model.
    /// </summary>
    [DataContract]
    public class UserSeasonModel
    {
        /// <summary>
        /// Gets or sets the season identifier.
        /// </summary>
        /// <value>The season identifier.</value>
        [DataMember]
        public string SeasonId { get; set; }

        /// <summary>
        /// Gets or sets the episode identifiers.
        /// </summary>
        /// <value>The episode identifiers.</value>
        [DataMember]
        public List<string> EpisodeIds { get; set; }

    }
}
