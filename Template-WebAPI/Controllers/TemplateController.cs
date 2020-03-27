using System.Collections.Generic;
using Template_WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Template_WebAPI.Repository;
using System.Text.RegularExpressions;

namespace Template_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplateController : ControllerBase
    {
        private readonly ITemplateRepository _templateRepository;
        private static readonly Regex regex = new Regex("^[a-zA-Z0-9]*$");

        public TemplateController(ITemplateRepository templateRepository)
        {
            _templateRepository = templateRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Templates>>> Get()
        {
            var templates = await _templateRepository.GetAll();
            return Ok(templates);
        }

        [HttpGet("{id:length(24)}", Name = "GetTemplate")]
        public async Task<ActionResult<Templates>> Get(string id)
        {
            var template = await _templateRepository.GetById(id);

            if (template == null)
            {
                return NotFound();
            }

            return Ok(template);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(Templates templateIn, string id)
        {
            var template = await _templateRepository.GetById(id);

            if (template == null)
            {
                return NotFound();
            }

            await _templateRepository.Update(templateIn, id);

            return Ok(templateIn);
        }

        [HttpPost]
        public async Task<ActionResult<Templates>> Create(Templates template)
        {
            await _templateRepository.Add(template);

            return CreatedAtRoute("GetTemplate", new { id = template.Id.ToString() }, template);
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var template = await _templateRepository.GetById(id);

            if (template == null)
            {
                return NotFound();
            }

            await _templateRepository.Remove(template.Id);

            return NoContent();
        }
    }
}
