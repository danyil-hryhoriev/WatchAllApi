using System;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace WatchAllApi.Models
{
    public class EpisodeModel
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [DataMember]
        public string Id { get; set; }

        [BsonElement("name")]
        [DataMember]
        public string Name { get; set; }

        [BsonElement("seasonId")]
        [DataMember]
        public int Season { get; set; }

        [BsonElement("orderId")]
        [DataMember]
        public int OrderNumber { get; set; }

        [BsonElement("air_date")]
        [DataMember]
        public DateTime AirDate { get; set; }

        [BsonElement("duration")]
        [DataMember]
        public string Duration { get; set; }

        [BsonElement("description")]
        [DataMember]
        public string Description { get; set; }
    }
}