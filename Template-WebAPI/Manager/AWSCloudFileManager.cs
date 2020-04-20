using System.IO;
using System.Threading.Tasks;
using Amazon.S3;
using Template_WebAPI.Model;

namespace Template_WebAPI.Manager
{
  public class AWSCloudFileManager : ICloudFileManager
  {
    private readonly IAmazonS3 awsS3;
    public AWSCloudFileManager(IAmazonS3 s3)
    {
      this.awsS3 = s3;
    }

    public Task UploadTemplateXMLFileAsync(Template template)
    {
      var folderName = Path.Combine("Resources", "File");
      var pathToTemplateFile = Path.Combine(Directory.GetCurrentDirectory(), folderName);
      var fileName = $@"{pathToTemplateFile}\{template.Id}.xml";
      throw new System.NotImplementedException();
    }
  }
}