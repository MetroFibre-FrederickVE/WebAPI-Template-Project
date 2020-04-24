using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Template_WebAPI.Event;
using Template_WebAPI.Events;
using Template_WebAPI.Model;
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
    [Route("eventtypes")]
    public ActionResult<IEnumerable<EventTypes>> GetEventTypesAsync()
    {
      return Ok(_enumExtensions.GetValues<EventTypes>());
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Model.Events>>> GetAllEventsAsync()
    {
      var events = await _eventsManager.GetAllEventsAsync();
      return HandInvalidRequest<IEnumerable<Model.Events>>(events, HttpMethod.Get);
    }

    [HttpGet]
    [Route("{eventId:length(24)}")]
    public async Task<ActionResult<IEnumerable<Model.Events>>> GetEventUsingIdAsync(string eventId)
    {
      var eventsResult = await _eventsManager.GetAllCreatedAfterIdAsync(eventId);
      return HandInvalidRequest<IEnumerable<Model.Events>>(eventsResult, HttpMethod.Get);
    }

    private ActionResult<T> HandInvalidRequest<T>(Tuple<ErrorResponse, Model.Events> createResult, HttpMethod method)
    {
      if (createResult.Item1 != null)
      {
        if (createResult.Item1.ResponseCode < 401)
        {
          return BadRequest(createResult.Item1);
        }
        if (createResult.Item1.ResponseCode == 404)
        {
          return NotFound();
        }
      }
      return Ok(createResult.Item2);
    }

    private ActionResult<T> HandInvalidRequest<T>(Tuple<ErrorResponse, IEnumerable<Model.Events>> createResult, HttpMethod method)
    {
      return Ok(createResult.Item2);
    }

  }
}