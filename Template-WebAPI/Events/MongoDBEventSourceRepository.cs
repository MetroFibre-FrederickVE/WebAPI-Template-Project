using MongoDB.Bson;
using MongoDB.Driver;
using System;
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

    public async Task<IEnumerable<Model.Events>> GetAllEventsAsync()
    {
      var all = _dbCollection.Find(Builders<Model.Events>.Filter.Empty).Limit(50);
      return await all.ToListAsync();
    }

    public async Task<IEnumerable<Model.Events>> GetNewerEventsUsingIdAsync(string id)
    {
      _dbCollection = _mongoContext.GetCollection<Model.Events>(typeof(Model.Events).Name);

      double dateTimeFromInputId = new DateTimeOffset(new ObjectId(id).CreationTime).ToUnixTimeSeconds();
      var nextNewerRecords =  await _dbCollection.Find(x => x.CreatedAt > dateTimeFromInputId).Limit(50).ToListAsync();
      return nextNewerRecords;
    }
  }
}
