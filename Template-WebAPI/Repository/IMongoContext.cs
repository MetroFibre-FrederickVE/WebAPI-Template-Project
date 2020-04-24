using MongoDB.Driver;

namespace Template_WebAPI.Repository
{
  public interface IMongoContext
  {
    IMongoCollection<T> GetCollection<T>(string name);
  }
}
