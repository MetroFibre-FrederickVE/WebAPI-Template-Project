using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Template_WebAPI.Authentication
{
  public class ClaimsManager
  {
    private readonly ICloudMessageBus cloudMessageBus;

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
  }
}
