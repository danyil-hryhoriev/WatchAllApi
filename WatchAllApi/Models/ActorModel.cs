using System;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace WatchAllApi.Models
{
    [DataContract]
    public class ActorModel
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [DataMember]
        public string Id { get; set; }

        [BsonElement("fullName")]
        [DataMember]
        public string FullName { get; set; }

        [BsonElement("country")]
        [DataMember]
        public string Country { get; set; }

        [BsonElement("birthday")]
        [DataMember]
        public DateTime BirthDay { get; set; }

        [BsonElement("deathDay")]
        [DataMember]
        public DateTime DeathDay { get; set; }

        [BsonElement("gender")]
        [DataMember]
        public string Gender { get; set; }

        [BsonElement("role")]
        [DataMember]
        public string Role { get; set; }

        [BsonElement("image")]
        [DataMember]
        public string Image { get; set; }

        [BsonElement("characterName")]
        [DataMember]
        public string CharacterName { get; set; }
    }
}