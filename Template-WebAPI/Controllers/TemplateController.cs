using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Template_WebAPI.Model;
using Template_WebAPI.Repository;
using System;
using Template_WebAPI.Manager;
using System.Net.Http;

namespace Template_WebAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TemplateController : ControllerBase
  {
    private readonly ITemplateRepository _templateRepository;
    private readonly ITemplateManager templateManager;

    public TemplateController(ITemplateRepository templateRepository, ITemplateManager templateManager)
    {
      this.templateManager = templateManager;
      _templateRepository = templateRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Template>>> GetAllAsync()
    {
      var templates = await templateManager.GetAllAsync();
      return HandInvalidRequest<IEnumerable<Template>>(templates, HttpMethod.Get);
    }

    [HttpGet("{templateId:length(24)}", Name = "GetTemplate")]
    public async Task<ActionResult<Template>> GetUsingIdAsync(string templateId)
    {
      var templateResult = await templateManager.GetUsingIdAsync(templateId);
      return HandInvalidRequest<Template>(templateResult, HttpMethod.Get);
    }

    [HttpPut("{templateId:length(24)}")]
    public async Task<ActionResult<Template>> UpdateAsync(Template templateIn, string templateId)
    {
      var createResult = await templateManager.UpdateAsync(templateIn, templateId);
      return HandInvalidRequest<Template>(createResult, HttpMethod.Put);
    }

    [HttpPost]
    public async Task<ActionResult<Template>> CreateAsync(Template template)
    {
      var createResult = await templateManager.CreateAsync(template);
      return HandInvalidRequest<Template>(createResult, HttpMethod.Post);
    }

    [HttpPost]
    [Route("{templateId}/project")]
    [Route("{templateId}/project/{projectId}")]
    public async Task<ActionResult<Template>> CreateAsync(string templateId, string projectId)
    {
      await _templateRepository.AddProjectByTemplateIdAsync(templateId, projectId);
      var projectAssociationResult = await templateManager.CreateProjectAssociationAsync(templateId, projectId);
      return HandInvalidRequest<Template>(projectAssociationResult, HttpMethod.Post);
    }

    [HttpDelete("{templateId:length(24)}")]
    public async Task<ActionResult<Template>> DeleteByIdAsync(string templateId)
    {
      var deleteResult = await templateManager.DeleteByIdAsync(templateId);
      return HandInvalidRequest<Template>(deleteResult, HttpMethod.Delete);
    }

    [HttpDelete]
    [Route("{templateId}/project")]
    [Route("{templateId}/project/{projectId}")]
    public async Task<ActionResult<Template>> DeleteProjectIdAsync(string templateId, string projectId)
    {
      var deleteResult = await templateManager.RemoveProjectAssociationAsync(templateId, projectId);
      return HandInvalidRequest<Template>(deleteResult, HttpMethod.Delete);
    }

    private ActionResult<T> HandInvalidRequest<T>(Tuple<ErrorResponse, Template> createResult, HttpMethod method)
    {
      if (createResult.Item1 != null)
      {
        if (createResult.Item1.ResponseCode < 401)
        {
          return BadRequest(createResult.Item1);
        }
        if (createResult.Item1.ResponseCode == 404)
        {
          return NotFound();
        }
      }

      switch (method.Method.ToUpper())
      {
        case "POST":
          return CreatedAtRoute("GetTemplate", new { templateId = createResult.Item2.Id.ToString() }, createResult.Item2);
        case "DELETE":
          return NoContent();
        default:
          return Ok(createResult.Item2);
      }
    }

    private ActionResult<T> HandInvalidRequest<T>(Tuple<ErrorResponse, IEnumerable<Template>> createResult, HttpMethod method)
    {
      return Ok(createResult.Item2);
    }
  }
}
