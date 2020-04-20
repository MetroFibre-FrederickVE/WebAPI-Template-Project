using System.Threading.Tasks;

namespace Template_WebAPI.Manager
{
    public interface ICloudFileManager
    {
        Task UploadTemplateXMLFileAsync(string FullFileName);
    }
}