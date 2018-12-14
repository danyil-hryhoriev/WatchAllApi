using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using WatchAllApi.Enums;

namespace WatchAllApi.Models
{
    [DataContract]
    public class ShowModel
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [DataMember]
        public string Id { get; set; }

        [BsonElement("name")]
        [DataMember]
        public string Name { get; set; }

        [BsonElement("genresIds")]
        [DataMember]
        public List<string> GenresIds { get; set; }

        [BsonElement("status")]
        [DataMember]
        public ShowStatusEnum Status { get; set; }

        [BsonElement("duration")]
        [DataMember]
        public int Duration { get; set; }

        [BsonElement("premierDate")]
        [DataMember]
        public DateTime PremiereDate { get; set; }

        [BsonElement("showUrl")]
        [DataMember]
        public string ShowUrl { get; set; }

        [BsonElement("airDay")]
        [DataMember]
        public DayOfWeek DayOfAir { get; set; }

        [BsonElement("airTime")]
        [DataMember]
        public string TimeOfAir { get; set; }

        [BsonElement("rating")]
        [DataMember]
        public double Rating { get; set; }

        [BsonElement("chanelId")]
        [DataMember]
        public string ChanelId { get; set; }

        [BsonElement("imdbId")]
        [DataMember]
        public string ImdbId { get; set; }

        [BsonElement("theTvDbId")]
        [DataMember]
        public string TheTvDbId { get; set; }

        [BsonElement("imageOriginal")]
        [DataMember]
        public string ImageOriginal { get; set; }

        [BsonElement("imageMedium")]
        [DataMember]
        public string ImageMedium { get; set; }

        [BsonElement("description")]
        [DataMember]
        public string Description { get; set; }

        [BsonElement("seasonsIds")]
        [DataMember]
        public List<string> SeasonsIds { get; set; }

        [BsonElement("aliases")]
        [DataMember]
        public List<AliasModel> Aliases { get; set; }

        [BsonElement("actors")]
        [DataMember]
        public List<ActorModel> Actors { get; set; }
    }
}
