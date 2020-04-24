using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Template_WebAPI.Repository;

namespace Template_WebAPI.Events
{
  public class MongoDBEventSourceRepository : BaseRepository<Model.Events>, IEventSourceRepository
  {
    public MongoDBEventSourceRepository(IMongoContext context) : base(context)
    {

    }

    public async Task<IEnumerable<Model.Events>> GetAllAsync()
    {
      var all = _dbCollection.Find(Builders<Model.Events>.Filter.Empty).Limit(50);
      return await all.ToListAsync();
    }


  }
}
