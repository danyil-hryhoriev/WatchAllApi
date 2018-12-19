using System;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using WatchAllApi.Enums;

namespace WatchAllApi.Models
{
    /// <summary>
    /// Model of actor
    /// </summary>
    [DataContract]
    public class ActorModel
    {

        /// <summary>
        /// Unique id
        /// </summary>
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [DataMember]
        public string Id { get; set; }

        /// <summary>
        /// Full name of actor
        /// </summary>
        [BsonElement("fullName")]
        [DataMember]
        public string FullName { get; set; }

        /// <summary>
        /// Actor's country
        /// </summary>
        [BsonElement("country")]
        [DataMember]
        public string Country { get; set; }

        /// <summary>
        /// Actor's date of birth
        /// </summary>
        [BsonElement("birthday")]
        [DataMember]
        public DateTime BirthDay { get; set; }

        /// <summary>
        /// Actor's date of death
        /// </summary>
        [BsonElement("deathDay")]
        [DataMember]
        public DateTime DeathDay { get; set; }

        /// <summary>
        /// Actor's gender
        /// </summary>
        [BsonElement("gender")]
        [DataMember]
        public GenderEnum Gender { get; set; }

        /// <summary>
        /// Role of actor
        /// </summary>
        [BsonElement("role")]
        [DataMember]
        public string Role { get; set; }

        /// <summary>
        /// Actor's photo
        /// </summary>
        [BsonElement("image")]
        [DataMember]
        public string Image { get; set; }

        /// <summary>
        /// Name of character
        /// </summary>
        [BsonElement("characterName")]
        [DataMember]
        public string CharacterName { get; set; }
    }
}