using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Template_WebAPI.Event;
using Template_WebAPI.Events;
using Template_WebAPI.Repository;

namespace Template_WebAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class EventSourceController : ControllerBase
  {
    private readonly IEnumRepository _enumExtensions;
    private readonly IEventSourceManager _eventsManager;

    public EventSourceController(IEnumRepository enumExtensions, IEventSourceManager eventsManager)
    {
      _enumExtensions = enumExtensions;
      _eventsManager = eventsManager;
    }

    [HttpGet]
    [Route("event/eventtypes")]
    public ActionResult<IEnumerable<EventTypes>> GetEventTypesAsync()
    {
      return Ok(_enumExtensions.GetValues<EventTypes>());
    }

    [HttpGet]
    public ActionResult<IEnumerable<EventTypes>> GetAllEventsAsync()
    {
      return Ok();
    }

    [HttpGet]
    [Route("event/{eventId}")]
    public ActionResult<IEnumerable<EventTypes>> GetEventUsingIdAsync(string eventId)
    {
      return Ok();
    }
  }
}