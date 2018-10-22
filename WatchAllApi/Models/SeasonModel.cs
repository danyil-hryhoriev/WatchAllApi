using System.Collections.Generic;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace WatchAllApi.Models
{
    [DataContract]
    public class SeasonModel
    {
        [DataMember]
        [BsonElement("seasonId")]
        public int SeasonId { get; set; }

        [DataMember]
        [BsonElement("seriesModels")]
        public List<SeriesModel> SeriesModels { get; set; }
    }
}
