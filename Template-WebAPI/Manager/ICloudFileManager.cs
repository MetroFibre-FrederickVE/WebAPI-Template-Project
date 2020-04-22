using System.Threading.Tasks;
using Template_WebAPI.Model;

namespace Template_WebAPI.Manager
{
  public interface ICloudFileManager
  {
    Task UploadTemplateXMLFileAsync(Template template);
    Task<string> RetrieveSignedURL(string templateId);
  }
}