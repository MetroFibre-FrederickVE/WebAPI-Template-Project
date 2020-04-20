using Amazon.S3;
using Amazon.S3.Transfer;
using System;
using System.Threading.Tasks;

namespace Template_WebAPI.BackgroundServices
{
  public class FileUploadService
  {
    private const string bucketName = "firsttempbucket";
    private const string keyName = "Test_File_4";
    private const string filePath = "C:/Users/TKK/Downloads/8c9acddb7725865128cc4eda3e289ca8.png";

    private static IAmazonS3 s3Client;

    public static void Start()
    {
      s3Client = new AmazonS3Client();
      UploadFileAsync().Wait();
    }

    private static async Task UploadFileAsync()
    {
      try
      {
        var fileTransferUtility = new TransferUtility(s3Client);

        await fileTransferUtility.UploadAsync(filePath, bucketName);
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
