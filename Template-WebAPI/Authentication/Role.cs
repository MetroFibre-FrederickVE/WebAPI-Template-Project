using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Template_WebAPI.Authentication
{
  public static class Role
  {
    public const string Admin = "Admin";
    public const string User = "User";

    // TODO: For extension
    public static string GetClaim(string token, string claimType)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

      var stringClaimValue = securityToken.Claims.First(claim => claim.Type == claimType).Value;
      return stringClaimValue;
    }
  }
}
