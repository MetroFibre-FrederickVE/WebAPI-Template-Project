using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Template_WebAPI
{
  internal class TemplateDraftUploadDirCleaner : BackgroundService
  {
    public void ClearDraftDir()
    {
      var directoryName = Path.Combine("Resources", "File");
      var pathToDirectory = Path.Combine(Directory.GetCurrentDirectory(), directoryName);
      string[] files = Directory.GetFiles(pathToDirectory);

      foreach (string file in files)
      {
        FileInfo fileInfo = new FileInfo(file);

        if (fileInfo.CreationTime < DateTime.Now.AddMinutes(-60))
        {
          fileInfo.Delete();
        }
      }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      while (!stoppingToken.IsCancellationRequested)
      {
        ClearDraftDir();
        await Task.Delay(TimeSpan.FromMinutes(60));
      }
    }
  }
}
