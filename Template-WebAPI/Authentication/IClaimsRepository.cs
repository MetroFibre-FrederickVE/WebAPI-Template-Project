using System.Collections.Generic;
using System.Threading.Tasks;

namespace Template_WebAPI.Authentication
{
  public interface IClaimsRepository
  {
    Task<List<GroupsRole>> GetSecurityClaimsAsync(string id);
  }
}