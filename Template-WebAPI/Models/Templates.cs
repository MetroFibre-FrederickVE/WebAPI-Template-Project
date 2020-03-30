using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using Template_WebAPI.Enums;

namespace Template_WebAPI.Models
{
    public class Templates
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        [Required(ErrorMessage = "{0} is a mandatory field")]
        [RegularExpression("^([a-zA-Z0-9])*$", ErrorMessage = "Please ensure that the {0} field is Alphanumeric.")]
        [StringLength(255, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
        public string Name { get; set; }

        [EnumDataType(typeof(ProcessLevel))]
        [RegularExpression("^([0-7])*$", ErrorMessage = "Please ensure that the {0} field is a numeral.")]
        [Range(0, 7, ErrorMessage = "Please select an option within : 0 - 7")]
        public ProcessLevel ProcessLevel { get; set; }

        [EnumDataType(typeof(SensorId))]
        [RegularExpression("^([0-9])*$", ErrorMessage = "Please ensure that the {0} field is a numeral.")]
        [Range(0, 9, ErrorMessage = "Please select an option within : 0 - 9")]
        public SensorId SensorId { get; set; }

        [BsonElement("ProjectId")]
        public string[] ProjectId { get; set; }
    }
}
