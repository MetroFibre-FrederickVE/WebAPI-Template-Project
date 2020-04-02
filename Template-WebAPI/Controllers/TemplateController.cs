using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Template_WebAPI.Model;
using Template_WebAPI.Repository;
//using System.Web.Http;

namespace Template_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplateController : ControllerBase
    {
        private readonly ITemplateRepository _templateRepository;
        
        public TemplateController(ITemplateRepository templateRepository)
        {
            _templateRepository = templateRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Template>>> GetAsync()
        {
            var templates = await _templateRepository.GetAllAsync();
            return Ok(templates);
        }

        [HttpGet("{id:length(24)}", Name = "GetTemplate")]
        public async Task<ActionResult<Template>> GetAsync(string id)
        {
            var template = await _templateRepository.GetByIdAsync(id);

            if (template == null)
            {
                return NotFound();
            }

            return Ok(template);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateAsync(Template templateIn, string id)
        {
            var template = await _templateRepository.GetByIdAsync(id);

            if (template == null)
            {
                return NotFound();
            }

            await _templateRepository.UpdateAsync(templateIn, id);

            return Ok(templateIn);
        }

        [HttpPost]
        public async Task<ActionResult<Template>> CreateAsync(Template template)
        {
            await _templateRepository.AddAsync(template);

            return CreatedAtRoute("GetTemplate", new { id = template.Id.ToString() }, template);
        }

        [HttpPost]
        [Route("{id}/project")]
        public async Task<ActionResult<Template>> CreateAsync(Template template, [FromRoute] string id)
        {
            await _templateRepository.AddByIdAsync(template, id);

            return CreatedAtRoute("GetTemplate", new { id = template.Id.ToString() }, template);
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var template = await _templateRepository.GetByIdAsync(id);

            if (template == null)
            {
                return NotFound();
            }

            await _templateRepository.RemoveAsync(template.Id);

            return NoContent();
        }

        // TODO
        [HttpDelete("{id:length(24)}")]
        [Route("api/[controller]/{templateId}/project/{projectId}")]
        public async Task<IActionResult> DeleteAsync(string id, string projectId)
        {
            return NoContent();
        }
    }
}
