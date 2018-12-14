using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace WatchAllApi.Models
{
    [DataContract]
    public class AliasModel
    {
        [BsonElement("name")]
        [DataMember]
        public string Name { get; set; }

        [BsonElement("region")]
        [DataMember]
        public string Region { get; set; }
    }
}