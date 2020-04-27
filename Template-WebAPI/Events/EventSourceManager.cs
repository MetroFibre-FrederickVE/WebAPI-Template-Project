using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Template_WebAPI.Enums;
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

    public async Task<Tuple<ErrorResponse, IEnumerable<Model.TemplateEvent>>> GetAllEventsAsync()
    {
      var events = await repository.GetAllEventsAsync();
      return new Tuple<ErrorResponse, IEnumerable<Model.TemplateEvent>>(null, events);
    }

    public async Task<Tuple<ErrorResponse, IEnumerable<Model.TemplateEvent>>> GetAllCreatedAfterIdAsync(string eventId)
    {
      var events = await repository.GetNewerEventsUsingIdAsync(eventId);
      return new Tuple<ErrorResponse, IEnumerable<Model.TemplateEvent>>(null, events);
    }

    public async Task<Tuple<ErrorResponse, TemplateEvent>> CreateTemplateEvent(Template template)
    {
      var templateEvent = new TemplateEvent
      {
        EventType = EventType.Create
      };
      return await PersistTemplateEvent(template, EventType.Update);
    }

    public async Task<Tuple<ErrorResponse, TemplateEvent>> UpdateTemplateEvent(Template template)
    {
      
      return await PersistTemplateEvent(template, EventType.Update);
    }

    private async Task<Tuple<ErrorResponse, TemplateEvent>> PersistTemplateEvent(Template template, EventType eventType)
    {
      var templateEvent = new TemplateEvent
      {
        EventType = eventType,
        Template = template
      };
      var createdEvent = await repository.CreateTemplateEvent(templateEvent);
      return new Tuple<ErrorResponse, TemplateEvent>(null, createdEvent);
    }

    public async Task<Tuple<ErrorResponse, TemplateEvent>> RemoveTemplateEvent(Template template)
    {
      return await PersistTemplateEvent(template, EventType.Remove);
    }
  }
}
