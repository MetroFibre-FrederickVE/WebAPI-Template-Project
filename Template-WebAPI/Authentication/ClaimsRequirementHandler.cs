using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Generic;

namespace Template_WebAPI.Authentication
{
  internal class ClaimsRequirementHandler : AuthorizationHandler<ClaimsRequirment>
  {
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClaimsRequirment policyRequirement)
    {
      try
      {
        string policyRequirment = policyRequirement.MatchingRole;

        var tokenClaimsToList = context.User.Claims.Select(c => c.Value).ToList();

        var claimsValue = tokenClaimsToList[4].Substring(0, tokenClaimsToList[4].Length);

        var options = new JsonSerializerOptions
        {
          PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
          WriteIndented = true
        };

        var claimsJsonModel = JsonSerializer.Deserialize<ClaimsFromToken>(claimsValue, options);

        foreach (var group in claimsJsonModel.Groups)
        {
          foreach (var role in group.Roles)
          {
            if (role.RoleName.ToString().Contains(policyRequirment))
            {
              context.Succeed(policyRequirement);
              return Task.CompletedTask;
            }
          }
        }
      }
      catch
      {
        return Task.CompletedTask;
      }
      return Task.CompletedTask;
    }
  }

  

}
