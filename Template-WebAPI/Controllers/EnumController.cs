using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Template_WebAPI.Interfaces;
using Template_WebAPI.Models;

namespace Template_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnumController : ControllerBase
    {
        private readonly IEnumRepository _enumRepository;

        public EnumController(IEnumRepository enumRepository)
        {
            _enumRepository = enumRepository;
        }
    }
}
