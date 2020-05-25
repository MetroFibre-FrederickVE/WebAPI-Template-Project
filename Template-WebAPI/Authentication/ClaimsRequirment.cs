using Microsoft.AspNetCore.Authorization;

namespace Template_WebAPI.Authentication
{
  public class ClaimsRequirment : IAuthorizationRequirement
  {
    public ClaimsRequirment(string role)
    {
      MatchingRole = role;
    }

    public string MatchingRole { get; private set; }
  }
}
