using System.Collections.Generic;
using Template_WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Template_WebAPI.Repository;

namespace Template_WebAPI.Controllers
{
    [Route("api/templates/[controller]")]
    [ApiController]
    public class TemplateControllers : ControllerBase
    {
        private readonly ITemplateRepository _templateRepository;

        public TemplateControllers(ITemplateRepository templateRepository)
        {
            _templateRepository = templateRepository;
        }

        [HttpGet]

        [HttpPut]

        [HttpPost]

        [HttpDelete]
    }
}
