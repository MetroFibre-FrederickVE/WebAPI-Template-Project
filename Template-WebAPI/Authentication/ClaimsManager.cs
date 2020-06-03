using Microsoft.Extensions.Hosting;
using MPU.MicroServices.StandardLibrary.CloudMessaging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Template_WebAPI.Authentication
{
  public class ClaimsManager : BackgroundService, IClaimsManager
  {
    private readonly ICloudMessageBus cloudMessageBus;
    private readonly IClaimsRepository claimsRepository;

    public ClaimsManager(ICloudMessageBus cloudMessageBus, IClaimsRepository claimsRepository)
    {
      this.cloudMessageBus = cloudMessageBus;
      this.claimsRepository = claimsRepository;
    }

    public async Task securityClaimsReturnValue(SQSUpdatedClaims SQSMessage)
    {
      var securityClaims = SQSMessage.UpdatedSecurityGroupProjectClaims;
      await claimsRepository.UpdateSecurityClaimsGroupsInDbAsync(securityClaims.Id, securityClaims);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      await cloudMessageBus.StartQueueSubscription<SQSUpdatedClaims>("UpdateSecurityClaims", securityClaimsReturnValue, stoppingToken);
    }
  }
}
