using MongoDB.Driver;

namespace Template_WebAPI.Interfaces
{
    public interface IMongoContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}
