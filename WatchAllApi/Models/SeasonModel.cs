using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace WatchAllApi.Models
{
    [DataContract]
    public class SeasonModel
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [DataMember]
        public string Id { get; set; }

        [BsonElement("orderId")]
        [DataMember]
        public int OrderId { get; set; }

        [BsonElement("episodeQty")]
        [DataMember]
        public int EpisodeQty { get; set; }

        [BsonElement("premierDate")]
        [DataMember]
        public DateTime PremiereDate { get; set; }

        [BsonElement("endDate")]
        [DataMember]
        public DateTime EndDate { get; set; }

        [BsonElement("imageUrl")]
        [DataMember]
        public string Image { get; set; }

        [BsonElement("description")]
        [DataMember]
        public string Description { get; set; }

        [BsonElement("episodesIds")]
        [DataMember]
        public List<string> EpisodesIds { get; set; }
    }
}