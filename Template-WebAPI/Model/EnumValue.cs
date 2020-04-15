using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Template_WebAPI.Enums
{
  public class EnumValue
  {
    [Required(ErrorMessage = "{0} is a mandatory field")]
    [BsonRepresentation(BsonType.String)]
    public string Name { get; set; }

    [Required(ErrorMessage = "{0} is a mandatory field")]
    [BsonRepresentation(BsonType.Int32)]
    public int Value { get; set; }

    [Required(ErrorMessage = "{0} is a mandatory field")]
    [BsonRepresentation(BsonType.String)]
    public string Description { get; set; }
  }
}
