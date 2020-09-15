using System.Collections.Generic;
using System.Threading.Tasks;

namespace Template_WebAPI.Authentication
{
  public interface IClaimsRepository
  {
    Task<List<GroupsRole>> GetNewestSecurityClaimsFromDBAsync(string userEntityId);
    Task UpdateSecurityClaimsGroupsInDbAsync(string claimsGroupId, SecurityClaims newSecurityClaims);
  }
}