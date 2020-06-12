using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Core.Clusters;
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

      var connectionString = Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING");
      
      var mongodbCredentials = MongoCredential.CreateCredential(databaseName: _configuration["TemplateDatabaseSettings: DatabaseName"], username: "templateAPI-user",password: "XHgzzHySWCYIUzKl");

      // Connection Pooling settings
      //var settings = new MongoClientSettings();
      //settings.MaxConnectionPoolSize = 100;
      //settings.MinConnectionPoolSize = 1;
      //settings.MaxConnectionIdleTime = new TimeSpan(0, 1, 0);
      //settings.WaitQueueTimeout = new TimeSpan(0, 1, 0);
      //settings.Server = new MongoServerAddress(connectionString);
      //settings.Credential = mongodbCredentials;
      //settings.ConnectionMode = ConnectionMode.ReplicaSet;
      //settings.Scheme = MongoDB.Driver.Core.Configuration.ConnectionStringScheme.MongoDBPlusSrv;
      //_mongoClient = new MongoClient(settings);

      // connection pooling has to be extended to accomodate atlas connection
      // as localhost settings causes issues with the connection
      _mongoClient = new MongoClient(connectionString);

      _db = _mongoClient.GetDatabase(_configuration["TemplateDatabaseSettings:DatabaseName"]);
    }

    public IMongoCollection<T> GetCollection<T>(string name)
    {
      return _db.GetCollection<T>(name);
    }
  }
}
