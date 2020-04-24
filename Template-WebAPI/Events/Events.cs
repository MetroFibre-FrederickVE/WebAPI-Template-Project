using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using Template_WebAPI.Enums;
using System;

namespace Template_WebAPI.Model
{
  public class Events
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string EventId { get; set; }

    [BsonRepresentation(BsonType.Double)]
    public double CreatedAt
    {
      get {return ToUnixTime(EventId); }
      set { }
    }

    [Required(ErrorMessage = "{0} is a mandatory field")]
    public Template Template { get; set; }

    [Required(ErrorMessage = "{0} is a mandatory field")]
    public EnumValue EventType { get; set; }

    public double ToUnixTime(string objId)
    {
      return new DateTimeOffset(new ObjectId(objId).CreationTime).ToUnixTimeSeconds();
    }
  }
}
