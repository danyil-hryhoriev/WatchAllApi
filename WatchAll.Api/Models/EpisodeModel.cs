using System;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace WatchAll.Api.Models
{
    /// <summary>
    /// Model of episode
    /// </summary>
    [BsonIgnoreExtraElements]
    public class EpisodeModel
    {
        /// <summary>
        /// Unique id
        /// </summary>
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [DataMember]
        public string Id { get; set; }

        /// <summary>
        /// Series name
        /// </summary>
        [BsonElement("name")]
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Id of correspond season
        /// </summary>
        [BsonElement("seasonId")]
        [DataMember]
        public int Season { get; set; }

        /// <summary>
        /// Order in correspond season
        /// </summary>
        [BsonElement("orderId")]
        [DataMember]
        public int OrderNumber { get; set; }

        /// <summary>
        /// Date of release
        /// </summary>
        [BsonElement("air_date")]
        [DataMember]
        public DateTime AirDate { get; set; }

        /// <summary>
        /// Duration of episode
        /// </summary>
        [BsonElement("duration")]
        [DataMember]
        public string Duration { get; set; }

        /// <summary>
        /// Description of episode
        /// </summary>
        [BsonElement("description")]
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// Id of season
        /// </summary>
        [BsonElement("season_Id")]
        [DataMember]
        public string SeasonId { get; set; }
    }
}