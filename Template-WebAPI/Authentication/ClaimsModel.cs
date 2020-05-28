using Newtonsoft.Json;

namespace Template_WebAPI.Authentication
{
  public class ClaimsFromToken
  {
    [JsonProperty("_id")]
    public string Id { get; set; }

    [JsonProperty("username")]
    public string Username { get; set; }

    [JsonProperty("firstName")]
    public string FirstName { get; set; }

    [JsonProperty("lastName")]
    public string LastName { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("password")]
    public object Password { get; set; }

    [JsonProperty("sites")]
    public object Sites { get; set; }

    [JsonProperty("roles")]
    public object[] Roles { get; set; }

    [JsonProperty("associatedClientId")]
    public object AssociatedClientId { get; set; }

    [JsonProperty("passwordHashed")]
    public bool PasswordHashed { get; set; }

    [JsonProperty("isNewlyCreated")]
    public bool IsNewlyCreated { get; set; }

    [JsonProperty("associatedServiceProviderId")]
    public object AssociatedServiceProviderId { get; set; }

    [JsonProperty("updateDate")]
    public long UpdateDate { get; set; }

    [JsonProperty("creationDate")]
    public long CreationDate { get; set; }

    [JsonProperty("entityType")]
    public string EntityType { get; set; }

    [JsonProperty("entityId")]
    public string EntityId { get; set; }

    [JsonProperty("entityUrl")]
    public string EntityUrl { get; set; }

    [JsonProperty("legacyData")]
    public Legacydata LegacyData { get; set; }

    [JsonProperty("__doNotNotifyOnRegistration")]
    public bool DoNotNotifyOnRegistration { get; set; }

    [JsonProperty("clients")]
    public object[] Clients { get; set; }

    [JsonProperty("projects")]
    public object[] Projects { get; set; }

    [JsonProperty("boreholes")]
    public object[] Boreholes { get; set; }

    [JsonProperty("boxes")]
    public object[] Boxes { get; set; }

    [JsonProperty("passwordHash")]
    public object PasswordHash { get; set; }

    [JsonProperty("groups")]
    public Groups[] Groups { get; set; }

  }

  public class Groups
  {
    [JsonProperty("entityId")]
    public string EntityId { get; set; }

    [JsonProperty("roles")]
    public GroupsRole[] Roles { get; set; }
  }

  public class GroupsRole
  {
    public string RoleName { get; set; }
  }

  public class Legacydata
  {
    public int UserID { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public object Password { get; set; }
    public object ClientID { get; set; }
  }
}
