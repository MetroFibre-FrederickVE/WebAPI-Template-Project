using Amazon.S3;
using Amazon.SimpleNotificationService;
using Amazon.SQS;
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
    
    private readonly ICloudMessageBus cloudMessageBus;

    public MongoDBClaimsRepository(IMongoContext context, ICloudMessageBus cloudMessageBus) : base(context)
    {
      this.cloudMessageBus = cloudMessageBus;
    }

    public async Task securityClaimsReturnValue(SecurityClaims security)
    {
      await Task.Delay(1000);
    }

    public async Task UpdateDBFromSQSMessageBody()
    {
      CancellationTokenSource cts = new CancellationTokenSource();
      CancellationToken token = cts.Token;

      //
      //Func<SecurityClaims, Task> func = sc => Task.FromResult(sc) ;

      await cloudMessageBus.StartQueueSubscription<SecurityClaims>("UpdateSecurityClaims", securityClaimsReturnValue, token);

      // MessageBody-TO-Document in Collection

      // func?
      //var idFromMessage = await func.messageBody.Id;
      //var bodyFromMessage = await func.messageBody;

      // Loop through all documents?
      //var objectId = new ObjectId(idFromMessage);
      //await _dbCollection.UpdateOneAsync(Builders<SecurityClaims>.Filter.Eq("_id", objectId), bodyFromMessage);

    }

    public async Task<List<GroupsRole>> GetNewestSecurityClaimsFromDBAsync(string userEntityId)
    {
      // 
      await UpdateDBFromSQSMessageBody();

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
  }
}
