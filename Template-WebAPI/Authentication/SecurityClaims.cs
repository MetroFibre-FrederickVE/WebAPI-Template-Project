using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Template_WebAPI.Authentication
{
  public class SecurityClaims
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public object[] Roles { get; set; }

    public object[] Projects { get; set; }

    public object[] Users { get; set; }
  }
}
