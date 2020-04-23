using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using Template_WebAPI.Enums;

namespace Template_WebAPI.Model
{
  public class EventsModel
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [Required(ErrorMessage = "{0} is a mandatory field")]
    public Template Template { get; set; }

    [Required(ErrorMessage = "{0} is a mandatory field")]
    public EnumValue EventType { get; set; }
  }
}
