using Microsoft.AspNetCore.Authorization;

namespace Template_WebAPI.Authentication
{
  public class ClaimsRequirement : IAuthorizationRequirement
  {
    public string Role { get; }

    public ClaimsRequirement(string claimRole)
    {
      Role = claimRole;
    }
  }
}
