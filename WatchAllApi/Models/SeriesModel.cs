using System;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace WatchAllApi.Models
{
    [DataContract]
    public class SeriesModel
    {
        [DataMember]
        [BsonElement("seriesId")]
        public int SeriesId { get; set; }

        [DataMember]
        [BsonElement("name")]
        public string Name { get; set; }

        [DataMember]
        [BsonElement("realeseDate")]
        public DateTime RealeseDate { get; set; }
    }
}
