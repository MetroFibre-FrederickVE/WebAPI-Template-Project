using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Template_WebAPI.Authentication
{
  public class AuthentificationService
  {
    private readonly AppSettings _appSettings;

    public AuthentificationService(IOptions<AppSettings> appSettings)
    {
      _appSettings = appSettings.Value;
    }

    

  }
}
