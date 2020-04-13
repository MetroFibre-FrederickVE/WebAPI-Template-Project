﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Template_WebAPI.Enums;
using Template_WebAPI.Repository;

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
    public ActionResult<IEnumerable<Sensor>> GetSensorAsync()
    {
      return Ok(_enumExtensions.GetValuesAsync<Sensor>());
    }

    [HttpGet]
    [Route("processlevel")]
    public ActionResult<IEnumerable<ProcessLevel>> GetProcessAsync()
    {
      return Ok(_enumExtensions.GetValuesAsync<ProcessLevel>());
    }
  }
}
