using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Template_WebAPI.Interfaces
{
    public interface IBaseRepository<TEntity>
        where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(string id);
        Task Add(TEntity obj);
        Task Update(TEntity obj, string id);
        void Remove(string id);
    }
}
