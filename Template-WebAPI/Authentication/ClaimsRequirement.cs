using Microsoft.AspNetCore.Authorization;

namespace Template_WebAPI.Authentication
{
  public class ClaimsRequirement : IAuthorizationRequirement
  {
    public ClaimsRequirement(string claimRole)
    {
      Role = claimRole;
    }

    public string Role { get; private set; }
  }
}
