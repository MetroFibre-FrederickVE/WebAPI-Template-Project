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
        var tokenClaimsToList = context.User.Claims.Select(c => c.Value).ToList();

        var claimsValue = tokenClaimsToList[4].Substring(0, tokenClaimsToList[4].Length);

        var options = new JsonSerializerOptions
        {
          PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
          WriteIndented = true
        };

        var claimsJsonModel = JsonSerializer.Deserialize<ClaimsFromToken>(claimsValue, options);


        // We could refactor the contains method to happen on the security groups roles collection.
        // This would avoid creating a transient listOfTokenClaims collection just so that it can be searched.
        List<string> collectionOfRolesFromToken = new List<string>(); //

        // Is this hard coded to the first group only?
        //foreach (var groupRole in claimsJsonModel.Groups.ElementAt(0).Roles) 
        //{
        //  var roleToString = groupRole.RoleName.ToString();
        //  collectionOfRolesFromToken.Add(roleToString);
        //}


        foreach (var group in claimsJsonModel.Groups)
        {
          foreach (var role in group.Roles)
          {
            var roleToString = role.RoleName.ToString();
            collectionOfRolesFromToken.Add(roleToString);
          }
        }

        string policyRequirment = policyRequirement.MatchingRole;

        if (collectionOfRolesFromToken.Contains(policyRequirment))
        {
          context.Succeed(policyRequirement);
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
