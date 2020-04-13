using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Template_WebAPI.Model;
using Template_WebAPI.Repository;

namespace Template_WebAPI.Manager
{
  public class TemplateManager : ITemplateManager
  {
    private readonly ITemplateRepository repository;
    public TemplateManager(ITemplateRepository repository)
    {
      this.repository = repository;
    }

    public async Task<Tuple<ErrorResponse, Template>> CreateAsync(Template template)
    {
      var returnedBoolValue = await repository.CheckIfNamesDuplicate(template);
      if (returnedBoolValue == true)
      {
        return new Tuple<ErrorResponse, Template>(new ErrorResponse(400.1, $"The Template name \"{template.Name}\" is already in use."), null);
      }

      await repository.AddAsync(template);
      return new Tuple<ErrorResponse, Template>(null, template);
    }

    public async Task<Tuple<ErrorResponse, Template>> CreateProjectAssociationAsync(string templateId, string projectId)
    {
      if (projectId == null || templateId == null)
      {
        return new Tuple<ErrorResponse, Template>(new ErrorResponse(400.2, $"Neither the 'Template Id' nor the 'Project Id' can be null."), null);
      }
      else if (templateId.Length != 24)
      {
        return new Tuple<ErrorResponse, Template>(new ErrorResponse(400.2, $"The Template Id string has to be in the BSON Id format."), null);
      }

      await repository.AddProjectByTemplateIdAsync(templateId, projectId);
      var updatedTemplate = await repository.GetByIdAsync(templateId);
      return new Tuple<ErrorResponse, Template>(null, updatedTemplate);
    }

    public async Task<Tuple<ErrorResponse, Template>> DeleteByIdAsync(string templateId)
    {
      var template = await repository.GetByIdAsync(templateId);

      if (template == null)
      {
        return new Tuple<ErrorResponse, Template>(new ErrorResponse(404, $"Template not found"), null);
      }

      await repository.RemoveAsync(template.Id);

      return new Tuple<ErrorResponse, Template>(null, template);
    }

    public async Task<Tuple<ErrorResponse, Template>> RemoveProjectAssociationAsync(string templateId, string projectId)
    {
       if (projectId == null || templateId == null)
      {
        return new Tuple<ErrorResponse, Template>(new ErrorResponse(400.2, $"Neither the 'Template Id' nor the 'Project Id' can be null."), null);
      }
      else if (templateId.Length != 24)
      {
        return new Tuple<ErrorResponse, Template>(new ErrorResponse(400.2, $"The Template Id string has to be in the BSON Id format."), null);
      }

      var template = await repository.GetByIdAsync(templateId);

      if (template == null)
      {
        return new Tuple<ErrorResponse, Template>(new ErrorResponse(404, $"Template not found"), null);
      }

      await repository.RemoveProjectFromTemplate(template.Id, projectId);

      return new Tuple<ErrorResponse, Template>(null, null);
    }

    public async Task<Tuple<ErrorResponse, IEnumerable<Template>>> GetAllAsync()
    {
      var templates = await repository.GetAllAsync();
      return new Tuple<ErrorResponse, IEnumerable<Template>>(null, templates);
    }

    public async Task<Tuple<ErrorResponse, Template>> GetUsingIdAsync(string templateId)
    {
      var template = await repository.GetByIdAsync(templateId);
      if (template == null)
      {
        return new Tuple<ErrorResponse, Template>(new ErrorResponse(404, $"Template not found"), null);
      }
      return new Tuple<ErrorResponse, Template>(null, template);
    }

    public async Task<Tuple<ErrorResponse, Template>> UpdateAsync(Template templateIn, string templateId)
    {
      var template = await repository.GetByIdAsync(templateId);

      if (template == null)
      {
        return new Tuple<ErrorResponse, Template>(new ErrorResponse(404, $"Template not found"), null);
      }

      var returnedBoolValue = await repository.CheckIfNamesDuplicate(templateIn);
      if (returnedBoolValue == true)
      {
        return new Tuple<ErrorResponse, Template>(new ErrorResponse(400.1, $"The Template name \"{template.Name}\" is already in use."), null);
      }

      await repository.UpdateAsync(templateIn, templateId);

      return new Tuple<ErrorResponse, Template>(null, templateIn);
    }
  }
}