using System.Threading.Tasks;
using Amazon.S3;

namespace Template_WebAPI.Manager
{
  public class AWSCloudFileManager : ICloudFileManager
  {
    private readonly IAmazonS3 awsS3;
    public AWSCloudFileManager(IAmazonS3 s3)
    {
      this.awsS3 = s3;
    }

    public Task UploadTemplateXMLFileAsync(string FullFileName)
    {
      throw new System.NotImplementedException();
    }
  }
}