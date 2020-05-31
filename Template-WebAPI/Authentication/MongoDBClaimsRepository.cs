using Amazon.S3;
using Amazon.SimpleNotificationService;
using Amazon.SQS;
using MongoDB.Bson;
using MongoDB.Driver;
using MPU.MicroServices.StandardLibrary.CloudMessaging;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Template_WebAPI.Manager;
using Template_WebAPI.Repository;

namespace Template_WebAPI.Authentication
{
  public class MongoDBClaimsRepository : BaseRepository<SecurityClaims>, IClaimsRepository
  {
    private readonly AWSMessageService cloudMessageBus;
    //private readonly ICloudMessageBus cloudMessageBus;

    public MongoDBClaimsRepository(IMongoContext context, AWSMessageService cloudMessageBus) : base(context)
    {
      var snsClient = new AmazonSimpleNotificationServiceClient();
      var sqsClient = new AmazonSQSClient();
      var s3Client = new AmazonS3Client();

      cloudMessageBus = new AWSMessageService(snsClient, sqsClient, null, s3Client, null);

      this.cloudMessageBus = cloudMessageBus;
    }

    public async Task UpdateDBFromSQSMessageBody()
    {
      CancellationTokenSource cts = new CancellationTokenSource();
      CancellationToken token = cts.Token;

      Func<SecurityClaims, Task> func = sc => Task.FromResult(sc) ;

      await cloudMessageBus.StartQueueSubscription<SecurityClaims>("UpdateSecurityClaims", func, token);

      // MessageBody-TO-Document in Collection

      // func?
      var idFromMessage = await func.messageBody.Id;
      var bodyFromMessage = await func.messageBody;

      var objectId = new ObjectId(idFromMessage);
      await _dbCollection.UpdateOneAsync(Builders<SecurityClaims>.Filter.Eq("_id", objectId), bodyFromMessage);

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
