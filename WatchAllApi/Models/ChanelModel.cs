﻿using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace WatchAllApi.Models
{
    public class ChanelModel
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [DataMember]
        public string Id { get; set; }

        [BsonElement("name")]
        [DataMember]
        public string Name { get; set; }

        [BsonElement("country")]
        [DataMember]
        public string Country { get; set; }
    }
}