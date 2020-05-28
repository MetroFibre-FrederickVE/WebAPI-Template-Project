using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Template_WebAPI.Authentication
{
  public class SecurityClaims
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public UpdatableRole[] Roles { get; set; }
    public string[] Projects { get; set; }
    public User[] Users { get; set; }
  }

  public class UpdatableRole
  {
    public string Id { get; set; }
    public string RoleName { get; set; }
    public string Description { get; set; }
    public Permissions Permissions { get; set; }
  }

  public class Permissions
  {
    public bool Read { get; set; }
    public bool Write { get; set; }
  }

  public class User
  {
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string[] AssociatedClientId { get; set; }
    public UsersLegacydata LegacyData { get; set; }
    public string[] AssociatedServiceProviderId { get; set; }
    public string EntityId { get; set; }
  }

  public class UsersLegacydata
  {
    public int UserID { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public object Password { get; set; }
    public object ClientID { get; set; }
    public object PasswordHashed { get; set; }
  }
}
