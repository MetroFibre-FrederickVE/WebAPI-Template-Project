using System;
using System.IO;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Transfer;
using Template_WebAPI.Model;

namespace Template_WebAPI.Manager
{
  public class AWSCloudFileManager : ICloudFileManager
  {
    private const string bucketName = "firsttempbucket";
    private readonly IAmazonS3 awsS3;

    public AWSCloudFileManager(IAmazonS3 s3Client)
    {
      this.awsS3 = s3Client;
      awsS3 = new AmazonS3Client();
    }

    public async Task UploadTemplateXMLFileAsync(Template template)
    {

      var folderName = Path.Combine("Resources", "File");
      var pathToTemplateFile = Path.Combine(Directory.GetCurrentDirectory(), folderName);
      var fileName = $@"{pathToTemplateFile}\{template.Id}.xml";

      try
      {
        var fileTransferUtility = new TransferUtility(awsS3);

        await fileTransferUtility.UploadAsync(fileName, bucketName);
        Console.WriteLine("File upload has been completed");
      }
      catch (AmazonS3Exception e)
      {
        Console.WriteLine("Error encountered on server. Message:'{0}' when writing an object", e.Message);
      }
      catch (Exception e)
      {
        Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
      }

    }
  }
}