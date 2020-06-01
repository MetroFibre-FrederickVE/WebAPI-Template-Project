using MongoDB.Bson;
using MongoDB.Driver;
using MPU.MicroServices.StandardLibrary.CloudMessaging;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Template_WebAPI.Manager;
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

    public async Task UpdateAsync(string id, SecurityClaims obj)
    {
      //
      UpdateDefinition<SecurityClaims> update = new UpdateDefinition<SecurityClaims>(obj);
      //
      var objectId = new ObjectId(id);
      await _dbCollection.UpdateOneAsync(Builders<SecurityClaims>.Filter.Eq("_id", objectId), update);
    }
  }
}
