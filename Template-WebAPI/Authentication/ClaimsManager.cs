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
      try
      {
        var securityClaims = SQSMessage.UpdatedSecurityGroupProjectClaims;
        await claimsRepository.UpdateSecurityClaimsGroupsInDbAsync(securityClaims.Id, securityClaims);
      }
      catch
      {
        Console.WriteLine("Failed to update DB with SQS Message value.");
      }
    }

    public async Task UpdateDBFromSQSMessageBody()
    {
      CancellationTokenSource cts = new CancellationTokenSource();
      CancellationToken token = cts.Token;
      if (!token.IsCancellationRequested)
      {
        await cloudMessageBus.StartQueueSubscription<SQSUpdatedClaims>("UpdateSecurityClaims", securityClaimsReturnValue, token);
        //cts.Cancel(); // ?
      }
      //cts.Cancel(); // ?
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      while (!stoppingToken.IsCancellationRequested)
      {
        await UpdateDBFromSQSMessageBody();
        await Task.Delay(TimeSpan.FromMinutes(30));
      }
    }
  }
}
