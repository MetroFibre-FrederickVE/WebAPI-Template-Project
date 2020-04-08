using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Xml.Linq;
using Template_WebAPI.Interfaces;

namespace Template_WebAPI.Controllers
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

        [Route("sendXML")]
        public async void SendXML()
        {
            var request = new HttpRequestMessage();

            XDocument doc = XDocument.Load(await request.Content.ReadAsStreamAsync());
            string fileName = "something.xml"; // insert filename spawner
            string saveLoc = string.Format(@"", fileName); // insert path
            doc.Save(saveLoc);

        }
    }
}
