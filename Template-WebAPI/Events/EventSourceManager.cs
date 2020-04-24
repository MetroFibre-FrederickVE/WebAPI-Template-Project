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

    public async Task<Tuple<ErrorResponse, IEnumerable<Model.Events>>> GetAllAsync()
    {
      var events = await repository.GetAllAsync();
      return new Tuple<ErrorResponse, IEnumerable<Model.Events>>(null, events);
    }

    public async Task<Tuple<ErrorResponse, Model.Events>> GetUsingIdAsync(string eventId)
    {
      var events = await repository.GetByIdAsync(eventId);
      return new Tuple<ErrorResponse, Model.Events>(null, events);
    }
  }
}
