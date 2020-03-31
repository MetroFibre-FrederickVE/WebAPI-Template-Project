using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using Template_WebAPI.Interfaces;

namespace Template_WebAPI.DbContexts
{
    public class MongoContext : IMongoContext
    {
        private IMongoDatabase _db { get; set; }
        private MongoClient _mongoClient { get; set; }
        private readonly IConfiguration _configuration;

        public MongoContext(IConfiguration configuration)
        {
            _configuration = configuration;

            // Configuration to be injected later
            // Currently using testable local DB
            _mongoClient = new MongoClient(_configuration["TemplateDatabaseSettings:ConnectionString"]);
            _db = _mongoClient.GetDatabase(_configuration["TemplateDatabaseSettings:DatabaseName"]);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _db.GetCollection<T>(name);
        }
    }
}
