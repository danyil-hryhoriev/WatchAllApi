using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using WatchAllApi.Enums;
using WatchAllApi.Models.UserStat;

namespace WatchAllApi.Models
{
    /// <summary>
    /// Full user model
    /// </summary>
    [DataContract]
    public class UserProfile
    {
        /// <summary>
        /// Constructor for profile
        /// </summary>
        public UserProfile()
        {
            CreatedDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Unique id
        /// </summary>
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [DataMember, Required(AllowEmptyStrings = false)]
        public string Id { get; set; }

        /// <summary>
        /// Login
        /// </summary>
        [BsonElement("login")]
        [DataMember]
        public string Login { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [BsonElement("password")]
        [DataMember]
        public string Password { get; set; }

        /// <summary>
        /// Firstname
        /// </summary>
        [BsonElement("firstName")]
        [DataMember]
        public string FirstName { get; set; }

        /// <summary>
        /// LastName
        /// </summary>
        [BsonElement("lastName")]
        [DataMember]
        public string LastName { get; set; }

        /// <summary>
        /// Role of user
        /// </summary>
        [BsonElement("role")]
        [DataMember]
        public UserRole Role { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [BsonElement("email")]
        public string Email { get; set; }

        /// <summary>
        /// Phone
        /// </summary>
        [BsonElement("phone")]
        public string Phone { get; set; }

        /// <summary>
        /// City
        /// </summary>
        [BsonElement("city")]
        public string City { get; set; }

        /// <summary>
        /// Shows that user watching
        /// </summary>
        [BsonElement("shows")]
        public List<UserShowModel> Shows { get; set; }

        /// <summary>
        /// Date of user creation
        /// </summary>
        [BsonElement("createdDate")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        [DataMember]
        public DateTime CreatedDate { get; set; }   // date and time
    }
}
