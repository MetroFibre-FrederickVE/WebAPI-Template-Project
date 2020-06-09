using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Template_WebAPI.Controllers
{
  [Route("[controller]")]
  //[Route("templatemanagement/v1/[controller]")]
  [ApiController]
  public class HealthCheckController : ControllerBase
  {
    [HttpGet]
    public ActionResult<string> GetApiStatus()
    {
      return Ok("Template services up and running.");
    }
  }
}