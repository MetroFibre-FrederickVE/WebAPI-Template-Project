using System;
using System.Threading.Tasks;
using Template_WebAPI.Model;

namespace Template_WebAPI.Repository
{
  public interface ITemplateRepository : IBaseRepository<Template>
  {
    string GenerateTemplateId();
    Task AddProjectByTemplateIdAsync(string templateId, string projectId);    
    Task CreateTemplateInputAsync(string templateId, TemplateInputMapping templateInputMapping);
    Task RemoveProjectFromTemplate(string templateId, string projectId);
    Task<Boolean> CheckIfNamesDuplicate(Template template);
    Task UpdateAsync(Template obj, string id);
  }
}