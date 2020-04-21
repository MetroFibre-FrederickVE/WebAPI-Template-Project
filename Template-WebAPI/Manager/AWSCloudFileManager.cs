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
    private string bucketName = Environment.GetEnvironmentVariable("AWSS3_BUCKET_NAME");

    // Provides the time period, in seconds, for which the generated presigned URL is valid. For example, 86400 (24 hours).
    //This value is an integer. The minimum value you can set is 1, and the maximum is 604800 (seven days).
    // A presigned URL can be valid for a maximum of seven days because the signing key you use in signature calculation is valid for up to seven days.
    private DateTime experationTime = DateTime.Now.AddMinutes(int.Parse(Environment.GetEnvironmentVariable("AWSS3_PRESIGNED_URL_EXPERATION_TIME_MINUTES")));

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

      var fileTransferUtility = new TransferUtility(awsS3);

      await fileTransferUtility.UploadAsync(fileName, bucketName);

    }

    public async Task<string> RetrieveSignedS3URL(string templateId)
    {
      GetPreSignedUrlRequest request = new GetPreSignedUrlRequest
      {
        BucketName = bucketName,
        Key = templateId + ".xml",
        Expires = experationTime
      };

      var signedUrl = awsS3.GetPreSignedURL(request);
      //return new Tuple<ErrorResponse, string>(null, signedUrl);
      return signedUrl;
    }
  }
}