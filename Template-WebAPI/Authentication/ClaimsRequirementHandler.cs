using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Generic;
using System.Security.Claims;

namespace Template_WebAPI.Authentication
{
  internal class ClaimsRequirementHandler : AuthorizationHandler<ClaimsRequirment>
  {
    private readonly IClaimsRepository _claimsRepository;

    public ClaimsRequirementHandler(IClaimsRepository claimsRepository)
    {
      _claimsRepository = claimsRepository;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ClaimsRequirment policyRequirement)
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

        //
        var grab = await _claimsRepository.GetSecurityClaimsAsync(claimsJsonModel.EntityId.ToString());

        // replace elements in fields within claimsJsonModels
        
        //foreach(var role in grab)
        //{
        //  claimsJsonModel.Groups.Append(new Groups { EntityId = "", Roles = new GroupsRole[] { new GroupsRole { RoleName = role.RoleName } } });
        //}

        //

        foreach (var group in claimsJsonModel.Groups)
        {
          foreach (var role in group.Roles)
          {
            if (role.RoleName.ToString().Contains(policyRequirment))
            {
              context.Succeed(policyRequirement);
              //break;
              return;// Task.CompletedTask;
            }
          }
        }
      }
      catch
      {
        return;// Task.CompletedTask;
      }
      return;// Task.CompletedTask;
    }
  }

  

}
