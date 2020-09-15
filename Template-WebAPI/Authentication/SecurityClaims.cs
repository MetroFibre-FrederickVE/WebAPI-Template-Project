using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Template_WebAPI.Authentication
{
  public class SecurityClaims
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonProperty("_id")]
    public string Id { get; set; }

    [BsonElement("roles")]
    public UpdatableRole[] Roles { get; set; }

    [BsonElement("projects")]
    public string[] Projects { get; set; }

    [BsonElement("users")]
    public User[] Users { get; set; }
  }

  public class UpdatableRole
  {
    [BsonElement("id")]
    [JsonProperty("_id")]
    public string Id { get; set; }

    [BsonElement("roleName")]
    public string RoleName { get; set; }

    [BsonElement("description")]
    public string Description { get; set; }

    [BsonElement("permissions")]
    public Permissions Permissions { get; set; }
  }

  public class Permissions
  {
    [BsonElement("read")]
    public bool Read { get; set; }

    [BsonElement("write")]
    public bool Write { get; set; }
  }

  public class User
  {
    [BsonElement("_id")]
    [JsonProperty("_id")]
    public string Id { get; set; }

    [BsonElement("firstName")]
    public string FirstName { get; set; }

    [BsonElement("lastName")]
    public string LastName { get; set; }

    [BsonElement("email")]
    public string Email { get; set; }

    [BsonElement("associatedClientId")]
    public string[] AssociatedClientId { get; set; }

    [BsonElement("legacyData")]
    public UsersLegacydata LegacyData { get; set; }

    [BsonElement("associatedServiceProviderId")]
    public string[] AssociatedServiceProviderId { get; set; }

    [BsonElement("entityId")]
    public string EntityId { get; set; }
  }

  public class UsersLegacydata
  {
    [BsonElement("UserID")]
    public int UserID { get; set; }

    [BsonElement("Name")]
    public string Name { get; set; }

    [BsonElement("Surname")]
    public string Surname { get; set; }

    [BsonElement("Email")]
    public string Email { get; set; }

    [BsonElement("password")]
    public object Password { get; set; }

    [BsonElement("ClientID")]
    public object ClientID { get; set; }

    [BsonElement("passwordHashed")]
    public object PasswordHashed { get; set; }
  }
}
