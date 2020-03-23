using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Template_WebAPI.Enums;

namespace Template_WebAPI.Models
{
    public class Templates
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }
        public ProcessLevel ProcessLevel { get; set; }
        public SensorId SensorId { get; set; }
    }
}
