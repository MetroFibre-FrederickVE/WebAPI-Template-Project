using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Template_WebAPI.Repository;

namespace Template_WebAPI.Authentication
{
  public class MongoDBClaimsRepository : BaseRepository<SecurityClaims>, IClaimsRepository
  {
    public MongoDBClaimsRepository(IMongoContext context) : base(context)
    {
    }

    public async Task<List<GroupsRole>> GetNewestSecurityClaimsFromDBAsync(string userEntityId)
    {
      var allDocumentsContainingUser = await _dbCollection.Find(new BsonDocument("users._id", userEntityId)).ToListAsync();

      var groupsRolesObjList = new List<GroupsRole>();

      foreach (var doc in allDocumentsContainingUser)
      {
        foreach (var role in doc.Roles)
        {
          groupsRolesObjList.Add(new GroupsRole { RoleName = role.RoleName });
        }
      }

      return groupsRolesObjList;
    }

    public async Task UpdateSecurityClaimsGroupsInDbAsync(string claimsGroupId, SecurityClaims newSecurityClaims)
    {
      var dbIterator = await _dbCollection.FindAsync(o => o.Id == claimsGroupId);
      if (await dbIterator.AnyAsync() == true)
      {
        await _dbCollection.FindOneAndReplaceAsync((sc => sc.Id == claimsGroupId), newSecurityClaims);
      }
      else
      {
        await _dbCollection.InsertOneAsync(newSecurityClaims);
      }
    }
  }
}
