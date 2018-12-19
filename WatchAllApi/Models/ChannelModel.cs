using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace WatchAllApi.Models
{
    /// <summary>
    /// Model of channel
    /// </summary>
    public class ChannelModel
    {
        /// <summary>
        /// Unique ID for storage in the database
        /// </summary>
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [DataMember]
        public string Id { get; set; }

        /// <summary>
        /// Unique name of channel
        /// </summary>
        [BsonElement("name")]
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Channel сountry
        /// </summary>
        [BsonElement("country")]
        [DataMember]
        public string Country { get; set; }
    }
}
