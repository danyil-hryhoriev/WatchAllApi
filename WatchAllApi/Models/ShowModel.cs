using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using WatchAllApi.Enums;

namespace WatchAllApi.Models
{
    /// <summary>
    /// Model of show
    /// </summary>
    [DataContract]
    public class ShowModel
    {
        /// <summary>
        /// Unique show id
        /// </summary>
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [DataMember]
        public string Id { get; set; }

        /// <summary>
        /// Name of show
        /// </summary>
        [BsonElement("name")]
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Ids of show genres
        /// </summary>
        [BsonElement("genresIds")]
        [DataMember]
        public List<string> GenresIds { get; set; }

        /// <summary>
        /// Current status of show
        /// </summary>
        [BsonElement("status")]
        [DataMember]
        public ShowStatusEnum Status { get; set; }

        /// <summary>
        /// Duration of show episode
        /// </summary>
        [BsonElement("duration")]
        [DataMember]
        public int Duration { get; set; }

        /// <summary>
        /// Date of release
        /// </summary>
        [BsonElement("premierDate")]
        [DataMember]
        public DateTime PremiereDate { get; set; }

        /// <summary>
        /// Url to original
        /// </summary>
        [BsonElement("showUrl")]
        [DataMember]
        public string ShowUrl { get; set; }

        /// <summary>
        /// Day of week when episode releases
        /// </summary>
        [BsonElement("airDay")]
        [DataMember]
        public DayOfWeek DayOfAir { get; set; }

        /// <summary>
        /// Time of day when episode releases
        /// </summary>
        [BsonElement("airTime")]
        [DataMember]
        public string TimeOfAir { get; set; }

        /// <summary>
        /// User rating
        /// </summary>
        [BsonElement("rating")]
        [DataMember]
        public float Rating { get; set; }

        /// <summary>
        /// Id of channel
        /// </summary>
        [BsonElement("chanelId")]
        [DataMember]
        public string ChanelId { get; set; }

        /// <summary>
        /// Id for show on IMDB
        /// </summary>
        [BsonElement("imdbId")]
        [DataMember]
        public string ImdbId { get; set; }

        /// <summary>
        /// Id for show on The TvDB
        /// </summary>
        [BsonElement("theTvDbId")]
        [DataMember]
        public string TheTvDbId { get; set; }

        /// <summary>
        /// Big image of show
        /// </summary>
        [BsonElement("imageOriginal")]
        [DataMember]
        public string ImageOriginal { get; set; }

        /// <summary>
        /// Medium image of show
        /// </summary>
        [BsonElement("imageMedium")]
        [DataMember]
        public string ImageMedium { get; set; }

        /// <summary>
        /// Description of show
        /// </summary>
        [BsonElement("description")]
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// Ids of show seasons
        /// </summary>
        [BsonElement("seasonsIds")]
        [DataMember]
        public List<string> SeasonsIds { get; set; }

        /// <summary>
        /// Aliases of show
        /// </summary>
        [BsonElement("aliases")]
        [DataMember]
        public List<AliasModel> Aliases { get; set; }

        /// <summary>
        /// Actors of show
        /// </summary>
        [BsonElement("actors")]
        [DataMember]
        public List<ActorModel> Actors { get; set; }
    }
}
