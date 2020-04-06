using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Template_WebAPI.Enums;

namespace Template_WebAPI.Model
{
    public class Template
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        [Required(ErrorMessage = "{0} is a mandatory field")]
        [RegularExpression("^([a-zA-Z0-9])*$", ErrorMessage = "Please ensure that the {0} field is Alphanumeric.")]
        [StringLength(255, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
        public string Name { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(ProcessLevel))]
        [BsonRepresentation(BsonType.Int32)]
        [Range(0, 7, ErrorMessage = "Please select an option within : 0 - 7")]
        public ProcessLevel ProcessLevel { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(Sensor))]
        [BsonRepresentation(BsonType.Int32)]
        [Range(0, 9, ErrorMessage = "Please select an option within : 0 - 9")]
        public Sensor Sensor { get; set; }

        [BsonElement("ProjectId")]
        public string[] ProjectId { get; set; }
    }
}
