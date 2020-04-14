using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Template_WebAPI
{
  internal class TimedHostedService : IHostedService, IDisposable
  {
    private readonly ILogger _logger;
    private Timer _timer;

    public TimedHostedService(ILogger<TimedHostedService> logger)
    {
      _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
      _logger.LogInformation("Timed Background Service is starting.");

      _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

      return Task.CompletedTask;
    }

    private void DoWork(object state)
    {
      _logger.LogInformation("Timed Background Service is working.");

      ClearDraftDir();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
      _logger.LogInformation("Timed Background Service is stopping.");

      _timer?.Change(Timeout.Infinite, 0);

      return Task.CompletedTask;
    }

    public void Dispose()
    {
      _timer?.Dispose();
    }

    public void ClearDraftDir()
    {
      var dirName = "C:/PyTestHttp/dir/api/upload";
      string[] files = Directory.GetFiles(dirName);

      foreach (string file in files)
      {
        FileInfo fileInfo = new FileInfo(file);
        if (fileInfo.LastWriteTime < DateTime.Now.AddMinutes(60)) fileInfo.Delete();
      }
    }
  }
}
