using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Generic;

namespace Template_WebAPI.Authentication
{
  internal class ClaimsRequirementHandler : AuthorizationHandler<ClaimsRequirment>
  {
    private readonly IClaimsRepository _claimsRepository;
    private readonly IClaimsManager claimsManager;

    public ClaimsRequirementHandler(IClaimsRepository claimsRepository, IClaimsManager claimsManager)
    {
      _claimsRepository = claimsRepository;
      this.claimsManager = claimsManager;
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

        var tokenClaimsToJsonModel = JsonSerializer.Deserialize<ClaimsFromToken>(claimsValue, options);

        var listOfNewestGroupRolesFromDB = await _claimsRepository.GetNewestSecurityClaimsFromDBAsync(tokenClaimsToJsonModel.EntityId.ToString());

        List<Groups> listOfGroups = new List<Groups>();
        
        foreach (var role in listOfNewestGroupRolesFromDB)
        {
          var generatedGroupsObj = new Groups { EntityId = "Role From DB", Roles = new GroupsRole[] { new GroupsRole { RoleName = role.RoleName } } };
          listOfGroups.Add(generatedGroupsObj);
        }

        tokenClaimsToJsonModel.Groups = listOfGroups.ToArray();

        foreach (var group in tokenClaimsToJsonModel.Groups)
        {
          foreach (var role in group.Roles)
          {
            if (role.RoleName.ToString().Contains(policyRequirment))
            {
              context.Succeed(policyRequirement);
              return;
            }
          }
        }
      }
      catch
      {
        return;
      }
      return;
    }
  }

  

}
