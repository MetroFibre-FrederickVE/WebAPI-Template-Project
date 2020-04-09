using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Template_WebAPI.Upload
{
    public class UploadUtil
    {
        public void UserInput()
        {
            Utils ut = new Utils();
            ut.FileName = "testFile.zip";
            ut.TempFolder = Path.Combine("http://localhost:8000/dir/api/upload/", "Temp");
            if (!Directory.Exists(ut.TempFolder))
                Directory.CreateDirectory(ut.TempFolder);
            ut.MaxFileSizeMB = 1;
            ut.SplitFile();
            foreach (string File in ut.FileParts) 
            {
                UploadFile(File);
            }
        }

        public bool UploadFile(string FileName)
        {
            bool rslt = false;
            using (var client = new HttpClient())
            {
                using (var content = new MultipartFormDataContent())
                {
                    var fileContent = new ByteArrayContent(System.IO.File.ReadAllBytes(FileName));
                    fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = Path.GetFileName(FileName)
                    };
                    content.Add(fileContent);

                    var requestUri = "http://localhost:8000/dir/api/upload/";
                    try
                    {
                        var result = client.PostAsync(requestUri, content).Result;
                        rslt = true;
                    }
                    catch (Exception ex)
                    {
                        // log error
                        rslt = false;
                    }
                }
            }
            return rslt;
        }
    }
}
