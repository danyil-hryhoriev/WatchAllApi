using System.Collections.Generic;
using System.Runtime.Serialization;
using WatchAllApi.Enums;

namespace WatchAllApi.Models.UserStat
{
    [DataContract]
    public class UserShowModel
    {
        [DataMember]
        public string ShowId { get; set; }

        [DataMember]
        public List<UserSeasonModel> Seasons { get; set; }

        [DataMember]
        public WatchingStatusEnum Status { get; set; }
    }
}
