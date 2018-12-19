using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace WatchAllApi.Models
{
    /// <summary>
    /// Alias of show in other regions
    /// </summary>
    [DataContract]
    public class AliasModel
    {
        /// <summary>
        /// Alias
        /// </summary>
        [BsonElement("name")]
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Country
        /// </summary>
        [BsonElement("region")]
        [DataMember]
        public string Region { get; set; }
    }
}