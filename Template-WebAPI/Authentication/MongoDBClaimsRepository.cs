using Amazon.S3.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Template_WebAPI.Repository;

namespace Template_WebAPI.Authentication
{
  public class MongoDBClaimsRepository : BaseRepository<SecurityClaims>, IClaimsRepository
  {
    public MongoDBClaimsRepository(IMongoContext context) : base(context)
    {

    }

    public async Task<List<GroupsRole>> GetSecurityClaimsAsync(string id)
    {
      //var test = await _dbCollection.FindAsync(Builders<SecurityClaims>.Filter.AnyEq("users._id", id));

      var test = await _dbCollection.Find(new BsonDocument("users._id", id)).ToListAsync();

      var user = test[0];

      var myList = new List<GroupsRole>();

      foreach (var doc in test)
      {
        foreach (var role in doc.roles)
        {
          myList.Add(new GroupsRole { RoleName = role.roleName });
        }
      }

      return myList;
    }
  }
}
