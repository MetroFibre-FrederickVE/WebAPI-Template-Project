using System.Collections.Generic;
using System.Threading.Tasks;
using Template_WebAPI.Repository;

namespace Template_WebAPI.Events
{
  public interface IEventSourceRepository : IBaseRepository<Model.Events>
  {
    Task<IEnumerable<Model.Events>> GetAllAsync();
  }
}
