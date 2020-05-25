using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Template_WebAPI.Authentication
{
  public class TokenObject
  {
    public string roleName { get; set; }

    public class JWTToken
    {
      public string Iss { get; set; }
      public string Aud { get; set; }
      public long Iat { get; set; }
      public float Exp { get; set; }
      public Claims Claim { get; set; }

      public class Claims
      {
        public string _id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public object Password { get; set; }
        public object Sites { get; set; }
        public object[] Roles { get; set; }
        public object AssociatedClientId { get; set; }
        public bool PasswordHashed { get; set; }
        public bool IsNewlyCreated { get; set; }
        public object AssociatedServiceProviderId { get; set; }
        public long UpdateDate { get; set; }
        public long CreationDate { get; set; }
        public string EntityType { get; set; }
        public string EntityId { get; set; }
        public string EntityUrl { get; set; }
        public Legacydata LegacyData { get; set; }
        public bool __doNotNotifyOnRegistration { get; set; }
        public object[] Clients { get; set; }
        public object[] Projects { get; set; }
        public object[] Boreholes { get; set; }
        public object[] Boxes { get; set; }
        public object PasswordHash { get; set; }
        public static Groups[] Groups { get; set; }
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

      public class Groups
      {
        public static string EntityId { get; set; }
        public static object[] Roles { get; set; }
      }
    }
  }
}
