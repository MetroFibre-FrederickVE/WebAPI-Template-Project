using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template_WebAPI.Repository;

namespace Template_WebAPI.Authentication
{
  public class MongoDBClaimsRepository : BaseRepository<SecurityClaims>
  {
    public MongoDBClaimsRepository(IMongoContext context) : base(context)
    {

    }

    public async Task<string> GetSecurityClaimsAsync(string id)
    {
      // entire collection
      var all = await _dbCollection.Find(Builders<SecurityClaims>.Filter.Empty).ToListAsync();
      
      // for every document in collection
      foreach(var post in all)
      {
        // for every user in document
        foreach(var user in post.Users)
        {
          // find specific user from token
          if (user.Id.Contains(id))
          {
            // find the role of the user
            foreach(var role in post.Roles)
            {
              return role.RoleName.ToString();
            }
          }
        }
      }
      
      return "";
    }
  }
}
