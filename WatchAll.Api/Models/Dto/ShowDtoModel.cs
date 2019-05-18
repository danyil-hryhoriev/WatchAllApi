using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using WatchAll.Api.Enums;

namespace WatchAll.Api.Models.Dto
{
    /// <summary>
    /// Model of show
    /// </summary>
    [DataContract]
    public class ShowDtoModel
    {
        /// <summary>
        /// Unique show id
        /// </summary>
        [DataMember]
        public string Id { get; set; }

        /// <summary>
        /// Name of show
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Ids of show genres
        /// </summary>
        [DataMember]
        public List<GenreModel> Genres { get; set; }

        /// <summary>
        /// Current status of show
        /// </summary>
        [DataMember]
        public ShowStatusEnum Status { get; set; }

        /// <summary>
        /// Duration of show episode
        /// </summary>
        [DataMember]
        public int Duration { get; set; }

        /// <summary>
        /// Date of release
        /// </summary>
        [DataMember]
        public DateTime PremiereDate { get; set; }

        /// <summary>
        /// Url to original
        /// </summary>
        [DataMember]
        public string ShowUrl { get; set; }

        /// <summary>
        /// Day of week when episode releases
        /// </summary>
        [DataMember]
        public DayOfWeek DayOfAir { get; set; }

        /// <summary>
        /// Time of day when episode releases
        /// </summary>
        [DataMember]
        public string TimeOfAir { get; set; }

        /// <summary>
        /// User rating
        /// </summary>
        [DataMember]
        public float Rating { get; set; }

        /// <summary>
        /// Id of channel
        /// </summary>
        [DataMember]
        public ChannelModel Chanel { get; set; }

        /// <summary>
        /// Id for show on IMDB
        /// </summary>
        [DataMember]
        public string ImdbId { get; set; }

        /// <summary>
        /// Id for show on The TvDB
        /// </summary>
        [DataMember]
        public string TheTvDbId { get; set; }

        /// <summary>
        /// Big image of show
        /// </summary>
        [DataMember]
        public string ImageOriginal { get; set; }

        /// <summary>
        /// Medium image of show
        /// </summary>
        [DataMember]
        public string ImageMedium { get; set; }

        /// <summary>
        /// Description of show
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// Ids of show seasons
        /// </summary>
        [DataMember]
        public List<SeasonDtoModel> Seasons { get; set; }

        /// <summary>
        /// Aliases of show
        /// </summary>
        [DataMember]
        public List<AliasModel> Aliases { get; set; }

        /// <summary>
        /// Actors of show
        /// </summary>
        [DataMember]
        public List<ActorModel> Actors { get; set; }
    }
}
