using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Template_WebAPI.Repository;

namespace Template_WebAPI.Events
{
  public abstract class BaseEventRepository<TEntity> : IBaseEventRepository<TEntity> 
    where TEntity : class
  {
    protected readonly IMongoContext _mongoContext;
    protected IMongoCollection<TEntity> _dbCollection;

    protected BaseEventRepository(IMongoContext context)
    {
      _mongoContext = context;
      _dbCollection = _mongoContext.GetCollection<TEntity>(typeof(TEntity).Name);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
      var all = await _dbCollection.FindAsync(Builders<TEntity>.Filter.Empty);
      return await all.ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(string id)
    {
      var objectId = new ObjectId(id);
      FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq("_id", objectId);

      _dbCollection = _mongoContext.GetCollection<TEntity>(typeof(TEntity).Name);
      return await _dbCollection.FindAsync(filter).Result.FirstOrDefaultAsync();
    }
  }
}
