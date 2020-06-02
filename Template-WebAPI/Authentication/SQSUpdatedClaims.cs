using MongoDB.Bson.Serialization.Attributes;

namespace Template_WebAPI.Authentication
{
  public class SQSUpdatedClaims
  {
    [BsonElement("updatedSecurityGroupProjectClaims")]
    public SecurityClaims UpdatedSecurityGroupProjectClaims { get; set; }
  }
}
