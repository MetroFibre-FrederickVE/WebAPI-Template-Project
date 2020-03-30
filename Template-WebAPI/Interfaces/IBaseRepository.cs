using System.Collections.Generic;
using System.Threading.Tasks;

namespace Template_WebAPI.Interfaces
{
    public interface IBaseRepository<TEntity>
        where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(string id);
        Task AddAsync(TEntity obj);
        Task UpdateAsync(TEntity obj, string id);
        Task RemoveAsync(string id);
    }
}
