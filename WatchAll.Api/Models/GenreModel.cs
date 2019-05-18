using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace WatchAll.Api.Models
{
    /// <summary>
    /// Model of show genre
    /// </summary>
    [DataContract]
    public class GenreModel
    {
        /// <summary>
        /// Unique id
        /// </summary>
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [DataMember]
        public string Id { get; set; }

        /// <summary>
        /// Genre name
        /// </summary>
        [BsonElement("name")]
        [DataMember]
        public string Name { get; set; }
    }
}
