using System.Collections.Generic;
using System.Threading.Tasks;

namespace Template_WebAPI.Authentication
{
  public interface IClaimsRepository
  {
    Task<IEnumerable<SecurityClaims>> GetSecurityClaimsAsync(SecurityClaims securityClaims);
  }
}