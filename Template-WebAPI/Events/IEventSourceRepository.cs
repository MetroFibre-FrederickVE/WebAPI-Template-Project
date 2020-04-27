using System.Collections.Generic;
using System.Threading.Tasks;
using Template_WebAPI.Model;
using Template_WebAPI.Repository;

namespace Template_WebAPI.Events
{
  public interface IEventSourceRepository : IBaseRepository<Model.TemplateEvent>
  {
    Task<IEnumerable<Model.TemplateEvent>> GetAllEventsAsync();
    Task<IEnumerable<Model.TemplateEvent>> GetNewerEventsUsingIdAsync(string id);
    Task<TemplateEvent> CreateTemplateEvent(TemplateEvent templateEvent);    
  }
}
