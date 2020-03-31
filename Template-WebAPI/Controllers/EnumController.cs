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
        [Route("sensorid")]
        public async Task<ActionResult<IEnumerable<Sensor>>> GetSensorAsync()
        {
            var all = await _enumExtensions.GetValuesAsync<Sensor>();
            return Ok(all);
        }

        [HttpGet]
        [Route("processlevel")]
        public async Task<ActionResult<IEnumerable<ProcessLevel>>> GetProcessAsync()
        {
            var all = await _enumExtensions.GetValuesAsync<ProcessLevel>();
            return Ok(all);
        }
    }
}
