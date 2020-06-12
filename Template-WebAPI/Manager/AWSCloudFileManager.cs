using System;
using System.IO;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Template_WebAPI.Model;

namespace Template_WebAPI.Manager
{
  public class AWSCloudFileManager : ICloudFileManager
  {
    private string bucketName = Environment.GetEnvironmentVariable("AWS_S3_BUCKET_NAME");
    private DateTime experationTime;

    private readonly IAmazonS3 awsS3 = new AmazonS3Client(new AmazonS3Config
    {
      Timeout = TimeSpan.FromMinutes(5),            // Default value is 100 seconds
      ReadWriteTimeout = TimeSpan.FromMinutes(5)    // Default value is 300 seconds
    });

    public AWSCloudFileManager(IAmazonS3 s3Client)
    {
      this.awsS3 = s3Client;
    }

    public async Task UploadTemplateXMLFileAsync(Template template)
    {
      var folderName = Path.Combine("Resources", "File");
      var pathToTemplateFile = Path.Combine(Directory.GetCurrentDirectory(), folderName);
      var fileName = Path.Combine(pathToTemplateFile,$"{template.Id}");
      var fileTransferUtility = new TransferUtility(awsS3);
      await fileTransferUtility.UploadAsync(fileName, bucketName);
    }

    public async Task<string> RetrieveSignedURL(string templateId)
    {
      experationTime = DateTime.Now.AddMinutes(int.Parse(Environment.GetEnvironmentVariable("AWS_S3_PRESIGNED_URL_EXPERATION_TIME_MINUTES")));
      GetPreSignedUrlRequest requestToBeSigned = new GetPreSignedUrlRequest
      {
        BucketName = bucketName,
        Key = templateId,
        Expires = experationTime
      };

      var signedUrl = awsS3.GetPreSignedURL(requestToBeSigned);
      return signedUrl;
    }
  }
}