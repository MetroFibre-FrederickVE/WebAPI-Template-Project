using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Template_WebAPI.Enums;
using Template_WebAPI.Interfaces;

namespace Template_WebAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class EnumController : ControllerBase
  {

    private readonly IEnumRepository _enumExtensions;

    public EnumController(IEnumRepository enumExtensions)
    {
      _enumExtensions = enumExtensions;
    }

    [HttpGet]
    [Route("sensor")]
    public async Task<ActionResult<IEnumerable<Sensor>>> GetSensorAsync()
    {
      return Ok(await _enumExtensions.GetValuesAsync<Sensor>());
    }

    [HttpGet]
    [Route("processlevel")]
    public async Task<ActionResult<IEnumerable<ProcessLevel>>> GetProcessAsync()
    {
      return Ok(await _enumExtensions.GetValuesAsync<ProcessLevel>());
    }
  }
}
