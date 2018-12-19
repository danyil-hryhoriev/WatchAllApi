using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace WatchAllApi.Models
{
    /// <summary>
    /// Model of show season
    /// </summary>
    [DataContract]
    public class SeasonModel
    {
        /// <summary>
        /// Unique id of season
        /// </summary>
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [DataMember]
        public string Id { get; set; }

        /// <summary>
        /// Season order id of correspond show
        /// </summary>
        [BsonElement("orderId")]
        [DataMember]
        public int OrderId { get; set; }

        /// <summary>
        /// Number of series in the season
        /// </summary>
        [BsonElement("episodeQty")]
        [DataMember]
        public int EpisodeQty { get; set; }

        /// <summary>
        /// Release date
        /// </summary>
        [BsonElement("premierDate")]
        [DataMember]
        public DateTime PremiereDate { get; set; }

        /// <summary>
        /// Season end-date
        /// </summary>
        [BsonElement("endDate")]
        [DataMember]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Image for season
        /// </summary>
        [BsonElement("imageUrl")]
        [DataMember]
        public string Image { get; set; }

        /// <summary>
        /// Description of season
        /// </summary>
        [BsonElement("description")]
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// Id of season episodes 
        /// </summary>
        [BsonElement("episodesIds")]
        [DataMember]
        public List<string> EpisodesIds { get; set; }
    }
}