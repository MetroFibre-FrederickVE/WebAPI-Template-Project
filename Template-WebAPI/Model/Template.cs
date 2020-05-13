using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Template_WebAPI.Enums;

namespace Template_WebAPI.Model
{
  [BsonIgnoreExtraElements]
  public class Template
  {    
    public Template()
    {
      // templateInputMapping = new List<TemplateInputMapping>();
    }
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("Name")]
    [Required(ErrorMessage = "{0} is a mandatory field")]
    [RegularExpression(@"^[)(-_ a-zA-Z0-9]+$", ErrorMessage = "Please ensure that the {0} field is Alphanumeric.")]
    [StringLength(255, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
    public string Name { get; set; }

    [Required(ErrorMessage = "{0} is a mandatory field")]
    public EnumValue ProcessLevel { get; set; }

    [BsonIgnoreIfNull]
    [BsonElement("ProjectId")]
    public string[] ProjectId { get; set; }

    public List<TemplateInputMapping> TemplateInputMapping { get;set; }
    public string Version { get; set; }
  }
}
