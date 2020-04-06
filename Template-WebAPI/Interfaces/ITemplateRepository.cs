using System.Threading.Tasks;
using Template_WebAPI.Interfaces;
using Template_WebAPI.Model;

namespace Template_WebAPI.Repository
{
    public interface ITemplateRepository : IBaseRepository<Template>
    {
        Task AddProjectByTemplateIdAsync(Template templateObj, string projectId);
        Task RemoveProjectFromTemplate(string templateId, string projectId);
    }
}