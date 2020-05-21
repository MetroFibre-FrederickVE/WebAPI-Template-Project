using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Template_WebAPI.Authentication
{
  public static class Role
  {
    public const string Admin = "Admin";
    public const string User = "Claims";

    // TODO: For extension
    public static string GetClaim(string token, string claimType)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

      var stringClaimValue = securityToken.Claims.First(claim => claim.Type == claimType).Value;
      return stringClaimValue;
    }

    public class Rootobject
    {
      public string iss { get; set; }
      public string aud { get; set; }
      public long iat { get; set; }
      public float exp { get; set; }
      public Claims claims { get; set; }

      public class Claims
      {
        public string _id { get; set; }
        public string username { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public object password { get; set; }
        public object sites { get; set; }
        public object[] roles { get; set; }
        public object associatedClientId { get; set; }
        public bool passwordHashed { get; set; }
        public bool isNewlyCreated { get; set; }
        public object associatedServiceProviderId { get; set; }
        public long updateDate { get; set; }
        public long creationDate { get; set; }
        public string entityType { get; set; }
        public string entityId { get; set; }
        public string entityUrl { get; set; }
        public Legacydata legacyData { get; set; }
        public bool __doNotNotifyOnRegistration { get; set; }
        public object[] clients { get; set; }
        public object[] projects { get; set; }
        public object[] boreholes { get; set; }
        public object[] boxes { get; set; }
        public object passwordHash { get; set; }
        public static Group[] groups { get; set; }
      }

      public class Legacydata
      {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public object password { get; set; }
        public object ClientID { get; set; }
      }

      public class Group
      {
        public static string entityId { get; set; }
        public static Role[] roles { get; set; }
      }

      public class Role
      {
        public string roleName { get; set; }
      }

    }
  }
}
