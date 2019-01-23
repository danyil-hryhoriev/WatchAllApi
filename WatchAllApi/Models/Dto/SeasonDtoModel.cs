﻿using System;
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
    public class SeasonDtoModel
    {
        /// <summary>
        /// Unique id of season
        /// </summary>
        [DataMember]
        public string Id { get; set; }

        /// <summary>
        /// Season order id of correspond show
        /// </summary>
        [DataMember]
        public int OrderId { get; set; }

        /// <summary>
        /// Number of series in the season
        /// </summary>
        [DataMember]
        public int EpisodeQty { get; set; }

        /// <summary>
        /// Release date
        /// </summary>
        [DataMember]
        public DateTime PremiereDate { get; set; }

        /// <summary>
        /// Season end-date
        /// </summary>
        [DataMember]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Image for season
        /// </summary>
        [DataMember]
        public string Image { get; set; }

        /// <summary>
        /// Description of season
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// Id of season episodes 
        /// </summary>
        [DataMember]
        public List<EpisodeModel> Episodes { get; set; }

        /// <summary>
        /// Id of show
        /// </summary>
        [DataMember]
        public string ShowId { get; set; }
    }
}