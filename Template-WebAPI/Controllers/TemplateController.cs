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
    private readonly ITemplateManager templateManager;

    public TemplateController(ITemplateRepository templateRepository, ITemplateManager templateManager)
    {
      this.templateManager = templateManager;
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
    public async Task<ActionResult<Template>> CreateProjectAssociationAsync(string templateId, string projectId)
    {      
      var projectAssociationResult = await templateManager.CreateProjectAssociationAsync(templateId, projectId);
      return HandInvalidRequest<Template>(projectAssociationResult, HttpMethod.Post);
    }

    [HttpPost]
    [Route("{templateId}/project/batch")]
    public async Task<ActionResult<Template>> CreateProjectAssociationAsync(string templateId, List<string> projectIds)
    {
      Tuple<ErrorResponse, Template> updatedTemplateResult = null;
      foreach (var projectId in projectIds)
      {        
        var projectAssociationResult = await templateManager.CreateProjectAssociationAsync(templateId, projectId);
        if (projectAssociationResult.Item1 != null) {
          return BadRequest(projectAssociationResult.Item1);
        } else {
          updatedTemplateResult = projectAssociationResult;
        }
      }

      return HandInvalidRequest<Template>(updatedTemplateResult, HttpMethod.Post);
    }

    [HttpPost]
    [Route("{templateId}/input/batch")]
    public async Task<ActionResult<List<TemplateInputMapping>>> CreateTemplateInputAsync(string templateId, List<TemplateInputMapping> templateInputs)
    {
      var retVal = new List<TemplateInputMapping>();
      foreach (var templateInput in templateInputs)
      {
        var projectAssociationResult = await templateManager.CreateTemplateInputAsync(templateId, templateInput);
        if (projectAssociationResult.Item1 == null)
        {
          retVal.Add(projectAssociationResult.Item2);
        } else {

        }
      }
      return Ok(retVal);
    }

    [HttpPost]
    [Route("file")]
    [HttpPost, DisableRequestSizeLimit]
    public ActionResult<Template> ProcessTemplateFile()
    {
      var file = Request.Form.Files[0];
      var projectAssociationResult = templateManager.ProcessTemplateFile(file);
      return HandInvalidRequest<Template>(projectAssociationResult, HttpMethod.Post);
    }

    [HttpDelete("{templateId:length(24)}")]
    public async Task<ActionResult<Template>> DeleteByIdAsync(string templateId)
    {
      var deleteResult = await templateManager.DeleteByIdAsync(templateId);
      return HandInvalidRequest<Template>(deleteResult, HttpMethod.Delete);
    }

    [HttpDelete]    
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
