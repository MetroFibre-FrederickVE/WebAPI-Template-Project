using Microsoft.AspNetCore.Authorization;
using MongoDB.Bson;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Template_WebAPI.Authentication
{
  internal class ClaimsRequirementHandler : AuthorizationHandler<ClaimsRequirment>
  {
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClaimsRequirment requirement)
    {
      var roles = ((ClaimsIdentity)context.User.Identity).Claims.Where(c => c.Type == "claims").Select(c => c.Value).ToJson();

      if (roles.Contains("groups"))
      {
        string newString = roles.Substring(roles.IndexOf("groups")).Trim();
        if (newString.Contains(requirement.MatchingEntityId))
        {
          context.Succeed(requirement);
        }
      }
      return Task.CompletedTask;
    }
  }

  

}
