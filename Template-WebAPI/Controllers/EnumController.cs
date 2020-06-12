using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Template_WebAPI.Authentication;
using Template_WebAPI.Enums;
using Template_WebAPI.Repository;

namespace Template_WebAPI.Controllers
{
  [Authorize]
  [Route("templatemanagement/v1/[controller]")]
  [ApiController]
  public class EnumController : ControllerBase
  {

    private readonly IEnumRepository _enumExtensions;

    public EnumController(IEnumRepository enumExtensions)
    {
      _enumExtensions = enumExtensions;
    }

    [Authorize(Policy = "CustomClaimsPolicy - Class Viewer")]
    [HttpGet]
    [Route("sensor")]
    public ActionResult<IEnumerable<Sensor>> GetSensorAsync()
    {
      return Ok(_enumExtensions.GetValues<Sensor>());
    }

    [Authorize(Policy = "CustomClaimsPolicy - Class Viewer")]
    [HttpGet]
    [Route("processlevel")]
    public ActionResult<IEnumerable<ProcessLevel>> GetProcessAsync()
    {
      return Ok(_enumExtensions.GetValues<ProcessLevel>());
    }
  }
}
