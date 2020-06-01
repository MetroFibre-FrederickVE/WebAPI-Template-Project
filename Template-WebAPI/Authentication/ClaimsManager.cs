using MPU.MicroServices.StandardLibrary.CloudMessaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Template_WebAPI.Authentication
{
  public class ClaimsManager
  {
    private readonly ICloudMessageBus cloudMessageBus;
    private readonly IClaimsRepository claimsRepository;

    public ClaimsManager(ICloudMessageBus cloudMessageBus, IClaimsRepository claimsRepository)
    {
      this.cloudMessageBus = cloudMessageBus;
      this.claimsRepository = claimsRepository;
    }

    // FUNC
    public async Task securityClaimsReturnValue(SecurityClaims security)
    {
      // Expand to execute update

      

      await claimsRepository.UpdateAsync(security.Id, security);
    }

    public async Task UpdateDBFromSQSMessageBody()
    {
      CancellationTokenSource cts = new CancellationTokenSource();
      CancellationToken token = cts.Token;

      await cloudMessageBus.StartQueueSubscription<SecurityClaims>("UpdateSecurityClaims", securityClaimsReturnValue, token);

    }
  }
}
