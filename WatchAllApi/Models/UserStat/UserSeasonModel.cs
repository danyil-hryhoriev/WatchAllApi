using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WatchAllApi.Models
{
    [DataContract]
    public class UserSeasonModel
    {
        [DataMember]
        public string SeasonId { get; set; }

        [DataMember]
        public List<string> EpisodeIds { get; set; }

    }
}
