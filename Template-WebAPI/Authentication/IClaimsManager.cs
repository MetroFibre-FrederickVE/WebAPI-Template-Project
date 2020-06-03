using System.Threading.Tasks;

namespace Template_WebAPI.Authentication
{
  public interface IClaimsManager
  {
    Task securityClaimsReturnValue(SQSUpdatedClaims SQSMessage);
  }
}