using Microsoft.AspNetCore.Authorization;

namespace Template_WebAPI.Authentication
{
  public class ClaimsRequirment : IAuthorizationRequirement
  {
    public ClaimsRequirment(string entityId)
    {
      MatchingEntityId = entityId;
    }

    public string MatchingEntityId { get; private set; }
  }
}
