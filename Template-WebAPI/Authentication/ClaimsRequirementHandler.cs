using Microsoft.AspNetCore.Authorization;
using MongoDB.Bson;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Template_WebAPI.Authentication
{
  internal class ClaimsRequirementHandler : AuthorizationHandler<ClaimsRequirment>
  {
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClaimsRequirment policyRequirement)
    {
      var claimsFieldValue = ((ClaimsIdentity)context.User.Identity).Claims.Where(c => c.Type == "claims").Select(c => c.Value).ToJson();

      if (claimsFieldValue.Contains("groups"))
      {
        string strOfGroupField = claimsFieldValue.Substring(claimsFieldValue.IndexOf("groups")).Trim();
        if (strOfGroupField.Contains(policyRequirement.MatchingEntityId))
        {
          context.Succeed(policyRequirement);
        }
      }
      return Task.CompletedTask;
    }
  }

  

}
