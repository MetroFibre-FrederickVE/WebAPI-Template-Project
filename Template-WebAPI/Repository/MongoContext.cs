using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;

namespace Template_WebAPI.Repository
{
  public class MongoContext : IMongoContext
  {
    private IMongoDatabase _db { get; set; }
    private MongoClient _mongoClient { get; set; }
    private readonly IConfiguration _configuration;

    public MongoContext(IConfiguration configuration)
    {
      _configuration = configuration;

      // Connection Pooling settings
      var settings = new MongoClientSettings();
      settings.MaxConnectionPoolSize = 100;
      settings.MinConnectionPoolSize = 1;
      settings.MaxConnectionIdleTime = new TimeSpan(0, 1, 0);
      settings.WaitQueueTimeout = new TimeSpan(0, 1, 0);
      settings.Server = new MongoServerAddress(_configuration["TemplateDatabaseSettings:ConnectionString"]);

      // Configuration to be injected later
      // Currently using testable local DB
      _mongoClient = new MongoClient(settings);
      _db = _mongoClient.GetDatabase(_configuration["TemplateDatabaseSettings:DatabaseName"]);
    }

    public IMongoCollection<T> GetCollection<T>(string name)
    {
      return _db.GetCollection<T>(name);
    }
  }
}
