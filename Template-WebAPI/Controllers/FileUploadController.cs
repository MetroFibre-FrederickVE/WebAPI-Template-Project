using Microsoft.AspNetCore.Mvc;
using Template_WebAPI.BackgroundServices;

namespace Template_WebAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class FileUploadController : ControllerBase
  {
    [HttpGet]
    public ActionResult<string> UploadFile()
    {
      FileUploadService.Start();
      return Ok("File upload executed.");
    }
  }
}
