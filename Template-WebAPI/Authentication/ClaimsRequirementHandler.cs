using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Template_WebAPI.Authentication
{
  internal class ClaimsRequirementHandler : AuthorizationHandler<ClaimsRequirement>
  {
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClaimsRequirement requirement)
    {
      // Bail out if the office number claim isn't present
      if (!context.User.HasClaim(c => c.Type == "office"))
      {
        return Task.CompletedTask;
      }

      // Bail out if we can't read an int from the 'office' claim
      int officeNumber;
      if (!int.TryParse(context.User.FindFirst(c => c.Type == "office").Value, out officeNumber))
      {
        return Task.CompletedTask;
      }

      // Finally, validate that the office number from the claim is not greater
      // than the requirement's maximum
      if (officeNumber <= requirement.Role)
      {
        // Mark the requirement as satisfied
        context.Succeed(requirement);
      }

      return Task.CompletedTask;
    }
  }

  internal class ClaimsRequirement : IAuthorizationRequirement
  {
    public ClaimsRequirement(int claimRole)
    {
      Role = claimRole;
    }

    public int Role { get; private set; }
  }
}
