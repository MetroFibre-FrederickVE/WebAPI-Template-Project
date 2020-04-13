using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Template_WebAPI.Model;

namespace Template_WebAPI.Manager
{
    public interface ITemplateManager
    {
         Task<Tuple<ErrorResponse,IEnumerable<Template>>> GetAllAsync();
         Task<Tuple<ErrorResponse,Template>> GetUsingIdAsync(string templateId);
         Task<Tuple<ErrorResponse,Template>> UpdateAsync(Template templateIn, string templateId);
         Task<Tuple<ErrorResponse,Template>> CreateAsync(Template template);
         Task<Tuple<ErrorResponse,Template>> CreateProjectAssociationAsync(string templateId, string projectId);
         Task<Tuple<ErrorResponse, Template>> DeleteByIdAsync(string templateId);
         Task<Tuple<ErrorResponse, Template>> RemoveProjectAssociationAsync(string templateId, string projectId);
    }
}