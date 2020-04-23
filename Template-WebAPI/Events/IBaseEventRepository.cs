using System.Collections.Generic;
using System.Threading.Tasks;

namespace Template_WebAPI.Events
{
  public interface IBaseEventRepository<TEntity> 
    where TEntity : class
  {
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> GetByIdAsync(string id);
  }
}