using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using WatchAllApi.Enums;

namespace WatchAllApi.Models
{
    [DataContract]
    public class UserProfile
    {
        public UserProfile()
        {
            CreatedDate = DateTime.UtcNow;
        }

        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [DataMember, Required(AllowEmptyStrings = false)]
        public string Id { get; set; }

        [BsonElement("login")]
        [DataMember]
        public string Login { get; set; }

        [BsonElement("password")]
        [DataMember]
        public string Password { get; set; }

        [BsonElement("firstName")]
        [DataMember]
        public string FirstName { get; set; }

        [BsonElement("lastName")]
        [DataMember]
        public string LastName { get; set; }

        [BsonElement("role")]
        [DataMember]
        public UserRole Role { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("createdDate")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        [DataMember]
        public DateTime CreatedDate { get; set; }   // date and time

    }
}
