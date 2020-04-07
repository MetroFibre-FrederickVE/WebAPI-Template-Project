using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Template_WebAPI.Model;
using Template_WebAPI.Repository;
using System;

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
        public async Task<ActionResult<IEnumerable<Template>>> GetAllAsync()
        {
            var templates = await _templateRepository.GetAllAsync();
            return Ok(templates);
        }

        [HttpGet("{templateId:length(24)}", Name = "GetTemplate")]
        public async Task<ActionResult<Template>> GetUsingIdAsync(string templateId)
        {
            var template = await _templateRepository.GetByIdAsync(templateId);

            if (template == null)
            {
                return NotFound();
            }

            return Ok(template);
        }

        [HttpPut("{templateId:length(24)}")]
        public async Task<IActionResult> UpdateAsync(Template templateIn, string templateId)
        {
            var template = await _templateRepository.GetByIdAsync(templateId);

            if (template == null)
            {
                return NotFound();
            }

            var returnedBoolValue = await _templateRepository.CheckIfNamesDuplicate(templateIn);
            if (returnedBoolValue == true)
            {
                return StatusCode(412, "Error: The Template name is already in use.");
            }

            foreach (var value in templateIn.ProjectId)
            {
                value.Remove(value.IndexOf(value));
            }

            await _templateRepository.UpdateAsync(templateIn, templateId);

            return Ok(templateIn);
        }

        [HttpPost]
        public async Task<ActionResult<Template>> CreateAsync(Template template)
        {
            var returnedBoolValue = await _templateRepository.CheckIfNamesDuplicate(template);
            if (returnedBoolValue == true)
            {
                return StatusCode(412, "Error: The Template name is already in use.");
            }

            await _templateRepository.AddAsync(template);

            return CreatedAtRoute("GetTemplate", new { templateId = template.Id.ToString() }, template);
        }

        [HttpPost]
        [Route("{templateId}/project")]
        [Route("{templateId}/project/{projectId}")]
        public async Task<ActionResult<Template>> CreateAsync(string templateId, string projectId)
        {
            if (projectId == null || templateId == null)
            {
                throw new ArgumentNullException("Neither the 'Template Id' nor the 'Project Id' can be null.");
            }
            else if (templateId.Length != 24)
            {
                throw new ArgumentOutOfRangeException("The Template Id string has to be 24 alphanumeric characters.");
            }

            await _templateRepository.AddProjectByTemplateIdAsync(templateId, projectId);

            return Ok(await _templateRepository.GetByIdAsync(templateId));
        }

        [HttpDelete("{templateId:length(24)}")]
        public async Task<IActionResult> DeleteByIdAsync(string templateId)
        {
            var template = await _templateRepository.GetByIdAsync(templateId);

            if (template == null)
            {
                return NotFound();
            }

            await _templateRepository.RemoveAsync(template.Id);

            return NoContent();
        }

        [HttpDelete]
        [Route("{templateId}/project")]
        [Route("{templateId}/project/{projectId}")]
        public async Task<IActionResult> DeleteProjectIdAsync(string templateId, string projectId)
        {
            if (projectId == null || templateId == null)
            {
                throw new ArgumentNullException("Neither the 'Template Id' nor the 'Project Id' can be null.");
            }
            else if (templateId.Length != 24)
            {
                throw new ArgumentOutOfRangeException("The Template Id string has to be 24 alphanumeric characters.");
            }

            var template = await _templateRepository.GetByIdAsync(templateId);

            if (template == null)
            {
                return NotFound();
            }

            await _templateRepository.RemoveProjectFromTemplate(template.Id, projectId);

            return Ok(await _templateRepository.GetByIdAsync(templateId));
        }
    }
}
