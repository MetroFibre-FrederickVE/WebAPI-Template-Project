using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template_WebAPI.Model;

namespace Template_WebAPI.Events
{
  public interface IEventSourceRepository : IBaseEventRepository<EventsModel>
  {

  }
}
