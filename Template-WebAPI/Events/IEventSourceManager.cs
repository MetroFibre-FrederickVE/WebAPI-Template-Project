using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Template_WebAPI.Model;

namespace Template_WebAPI.Events
{
  public interface IEventSourceManager
  {
    Task<Tuple<ErrorResponse, IEnumerable<EventsModel>>> GetAllAsync();
    Task<Tuple<ErrorResponse, EventsModel>> GetUsingIdAsync(string eventId);
  }
}
