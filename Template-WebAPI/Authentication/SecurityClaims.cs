using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Template_WebAPI.Authentication
{
  public class SecurityClaims
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string id { get; set; }
    public UpdatableRole[] roles { get; set; }
    public string[] projects { get; set; }
    public User[] users { get; set; }
  }

  public class UpdatableRole
  {
    public string id { get; set; }
    public string roleName { get; set; }
    public string description { get; set; }
    public Permissions permissions { get; set; }
  }

  public class Permissions
  {
    public bool read { get; set; }
    public bool write { get; set; }
  }

  public class User
  {
    public string id { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string email { get; set; }
    public string[] associatedClientId { get; set; }
    public UsersLegacydata legacyData { get; set; }
    public string[] associatedServiceProviderId { get; set; }
    public string entityId { get; set; }
  }

  public class UsersLegacydata
  {
    public int UserID { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public object password { get; set; }
    public object ClientID { get; set; }
    public object passwordHashed { get; set; }
  }
}
