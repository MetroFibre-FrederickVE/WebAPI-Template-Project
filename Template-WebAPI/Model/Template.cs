﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
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

        [Required(ErrorMessage = "{0} is a mandatory field")]
        public EnumValue ProcessLevel { get; set; }

        [Required(ErrorMessage = "{0} is a mandatory field")]
        public EnumValue Sensor { get; set; }

        [BsonElement("ProjectId")]
        public string[] ProjectId { get; set; }
    }
}
