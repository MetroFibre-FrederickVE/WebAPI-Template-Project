﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template_WebAPI.Model;
using Template_WebAPI.Repository;

namespace Template_WebAPI.Events
{
  public interface IEventSourceRepository : IBaseRepository<Model.Events>
  {

  }
}
