using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Template_WebAPI.Interfaces;

namespace Template_WebAPI.DbContexts
{
    public class MongoContext : IMongoContext
    {
        private IMongoDatabase _db { get; set; }
        private MongoClient _mongoClient { get; set; }
        public IClientSessionHandle Session { get; set; }
        private readonly IConfiguration _configuration;

        public MongoContext(IConfiguration configuration)
        {
            _configuration = configuration;

            // Configuration to be injected later
            // Currently using testable local DB
            _mongoClient = new MongoClient(_configuration["TemplateDatabaseSettings:ConnectionString"]); // Test DB injected
            _db = _mongoClient.GetDatabase(_configuration["TemplateDatabaseSettings:DatabaseName"]);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _db.GetCollection<T>(name);
        }
    }
}
