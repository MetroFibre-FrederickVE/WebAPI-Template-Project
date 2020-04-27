using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Template_WebAPI.Model;
using Template_WebAPI.Repository;

namespace Template_WebAPI.Events
{
  public class MongoDBEventSourceRepository : BaseRepository<Model.TemplateEvent>, IEventSourceRepository
  {
    public MongoDBEventSourceRepository(IMongoContext context) : base(context)
    {
    }

    public async Task<TemplateEvent> CreateTemplateEvent(TemplateEvent templateEvent)
    { 
      await AddAsync(templateEvent);  
      return templateEvent; 
    }

    public async Task<IEnumerable<Model.TemplateEvent>> GetAllEventsAsync()
    {
      var all = _dbCollection.Find(Builders<Model.TemplateEvent>.Filter.Empty).Limit(50);
      return await all.ToListAsync();
    }

    public async Task<IEnumerable<Model.TemplateEvent>> GetNewerEventsUsingIdAsync(string id)
    {
      _dbCollection = _mongoContext.GetCollection<Model.TemplateEvent>(typeof(Model.TemplateEvent).Name);

      double dateTimeFromInputId = new DateTimeOffset(new ObjectId(id).CreationTime).ToUnixTimeSeconds();
      var nextNewerRecords = await _dbCollection.Find(x => x.CreatedAt > dateTimeFromInputId).Limit(50).ToListAsync();
      return nextNewerRecords;
    }
  }
}
