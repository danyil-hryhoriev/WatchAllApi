using System.Collections.Generic;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace WatchAllApi.Models
{
    [DataContract]
    public class ShowModel
    {
        [DataMember]
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string SeriesId { get; set; }

        [DataMember]
        [BsonElement("name")]
        public string Name { get; set; }

        [DataMember]
        [BsonElement("mark")]
        public double Mark { get; set; }

        [DataMember]
        [BsonElement("description")]
        public string Description { get; set; }

        [DataMember]
        [BsonElement("state")]
        public SerialStateEnum State { get; set; }

        [DataMember]
        [BsonElement("seasons")]
        public List<SeasonModel> Seasons { get; set; }
    }
}
