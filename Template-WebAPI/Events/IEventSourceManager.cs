using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Template_WebAPI.Model;

namespace Template_WebAPI.Events
{
  public interface IEventSourceManager
  {
    Task<Tuple<ErrorResponse, IEnumerable<Model.TemplateEvent>>> GetAllEventsAsync();
    Task<Tuple<ErrorResponse, IEnumerable<Model.TemplateEvent>>> GetAllCreatedAfterIdAsync(string eventId);
    Task<Tuple<ErrorResponse,TemplateEvent>> CreateTemplateEvent(Template template);
    Task<Tuple<ErrorResponse, TemplateEvent>> UpdateTemplateEvent(Template template);
    Task<Tuple<ErrorResponse, TemplateEvent>> RemoveTemplateEvent(Template template);
  }
}
