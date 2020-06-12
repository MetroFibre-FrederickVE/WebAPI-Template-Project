using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Template_WebAPI.Authentication;
using Template_WebAPI.Enums;
using Template_WebAPI.Events;
using Template_WebAPI.Model;
using Template_WebAPI.Repository;

namespace Template_WebAPI.Controllers
{
  [Authorize]
  [Route("templatemanagement/v1/[controller]")]
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

    [Authorize(Policy = "CustomClaimsPolicy - Class Viewer")]
    [HttpGet]
    [Route("eventtypes")]
    public ActionResult<IEnumerable<EventType>> GetEventTypesAsync()
    {
      return Ok(_enumExtensions.GetValues<EventType>());
    }

    [Authorize(Policy = "CustomClaimsPolicy - Class Viewer")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Model.TemplateEvent>>> GetAllEventsAsync()
    {
      var events = await _eventsManager.GetAllEventsAsync();
      return HandInvalidRequest<IEnumerable<Model.TemplateEvent>>(events, HttpMethod.Get);
    }

    [Authorize(Policy = "CustomClaimsPolicy - Class Viewer")]
    [HttpGet]
    [Route("{eventId:length(24)}")]
    public async Task<ActionResult<IEnumerable<Model.TemplateEvent>>> GetEventUsingIdAsync(string eventId)
    {
      var eventsResult = await _eventsManager.GetAllCreatedAfterIdAsync(eventId);
      return HandInvalidRequest<IEnumerable<Model.TemplateEvent>>(eventsResult, HttpMethod.Get);
    }

    private ActionResult<T> HandInvalidRequest<T>(Tuple<ErrorResponse, Model.TemplateEvent> createResult, HttpMethod method)
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

    private ActionResult<T> HandInvalidRequest<T>(Tuple<ErrorResponse, IEnumerable<Model.TemplateEvent>> createResult, HttpMethod method)
    {
      return Ok(createResult.Item2);
    }

  }
}