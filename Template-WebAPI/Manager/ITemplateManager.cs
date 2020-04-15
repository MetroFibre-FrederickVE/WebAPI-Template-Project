using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Template_WebAPI.Model;

namespace Template_WebAPI.Manager
{
  public interface ITemplateManager
  {
    Task<Tuple<ErrorResponse, Template>> CreateAsync(Template template);
    Task<Tuple<ErrorResponse, Template>> CreateProjectAssociationAsync(string templateId, string projectId);
    Task<Tuple<ErrorResponse, TemplateInputMapping>> CreateTemplateInputAsync(string templateId, TemplateInputMapping templateInputMapping);
    Task<Tuple<ErrorResponse, Template>> DeleteByIdAsync(string templateId);
    Task<Tuple<ErrorResponse, IEnumerable<Template>>> GetAllAsync();
    Task<Tuple<ErrorResponse, Template>> GetUsingIdAsync(string templateId);
    Task<Tuple<ErrorResponse, Template>> UpdateAsync(Template templateIn, string templateId);
    Tuple<ErrorResponse, Template> ProcessTemplateFile(IFormFile file);
    Task<Tuple<ErrorResponse, Template>> RemoveProjectAssociationAsync(string templateId, string projectId);
  }
}