using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Template_WebAPI.Event;
using Template_WebAPI.Repository;

namespace Template_WebAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class EventController : ControllerBase
  {
    private readonly IEnumRepository _enumExtensions;

    public EventController(IEnumRepository enumExtensions)
    {
      _enumExtensions = enumExtensions;
    }

    [HttpGet]
    [Route("event/eventtypes")]
    public ActionResult<IEnumerable<EventTypes>> GetEventTypesAsync()
    {
      return Ok(_enumExtensions.GetValues<EventTypes>());
    }

    [HttpGet]
    public ActionResult<IEnumerable<EventTypes>> GetAllEventsdAsync()
    {
      return Ok();
    }

    [HttpGet]
    [Route("event/{eventId}")]
    public ActionResult<IEnumerable<EventTypes>> GetUsingEventIdAsync()
    {
      return Ok();
    }
  }
}