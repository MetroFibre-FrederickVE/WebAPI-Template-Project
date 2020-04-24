using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Template_WebAPI.Model;

namespace Template_WebAPI.Events
{
  public class EventSourceManager : IEventSourceManager
  {
    private readonly IEventSourceRepository repository;

    public EventSourceManager(IEventSourceRepository eventSourceRepository)
    {
      this.repository = eventSourceRepository;
    }

    public async Task<Tuple<ErrorResponse, IEnumerable<Model.Events>>> GetAllEventsAsync()
    {
      var events = await repository.GetAllEventsAsync();
      return new Tuple<ErrorResponse, IEnumerable<Model.Events>>(null, events);
    }

    public async Task<Tuple<ErrorResponse, IEnumerable<Model.Events>>> GetAllCreatedAfterIdAsync(string eventId)
    {
      var events = await repository.GetNewerEventsUsingIdAsync(eventId);
      return new Tuple<ErrorResponse, IEnumerable<Model.Events>>(null, events);
    }
  }
}
