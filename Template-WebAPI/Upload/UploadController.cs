using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Template_WebAPI.Upload
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IUploadRepository _uploadRepository;
        public UploadController(IUploadRepository uploadRepository)
        {
            _uploadRepository = uploadRepository;
        }

        [HttpPost]
        public Task UploadFile()
        {
            // Old framework call? - "Request.Files" Moved to "Request.Form.Files"
            foreach (var file in Request.Form.Files)
            {
                
            }

            var FileDataContent = Request.Form.Files;
            if (FileDataContent != null && FileDataContent.Length > 0)
            {
                // Old framework call? - ".InputStream" Moved to ".OpenReadStream"
                var stream = FileDataContent.OpenReadStream;
                var fileName = Path.GetFileName(FileDataContent.FileName);

                //var UploadPath = MapPath("~/dir/api/upload/");
                //Directory.CreateDirectory(UploadPath);
                string path = Path.Combine("~/dir/api/upload/", fileName);
                try
                {
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                    using (var fileStream = System.IO.File.Create(path))
                    {
                        stream.CopyTo(fileStream);
                    }
                    Utils UT = new Utils();
                    UT.MergeFile(path);
                }
                catch (IOException ex)
                {

                }
            }

        }

        [HttpPost]
        public string MultiUpload()
        {
            var chunks = Request.InputStream;

            string path = HttpContext.Current.Server.MapPath("~/App_Data/Uploads/Tamp");
            string newpath = Path.Combine(path, Path.GetRandomFileName());

            using (System.IO.FileStream fs = System.IO.File.Create(newpath))
            {
                byte[] bytes = new byte[77570];

                int bytesRead;
                while ((bytesRead = Request.InputStream.Read(bytes, 0, bytes.Length)) > 0)
                {
                    fs.Write(bytes, 0, bytesRead);
                }
            }
            return "test";
        }

    }
}
