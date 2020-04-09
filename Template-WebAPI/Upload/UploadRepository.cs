using System;
using System.IO;
using System.Net;
using System.Text;
using Template_WebAPI.Upload;

namespace Template_WebAPI.Repository
{
    public class UploadRepository : IUploadRepository
    {
        public UploadRepository()
        {

        }

        public string SendXMLFile(string xmlFilePath, string uri, int timeout)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);

            request.KeepAlive = false;
            request.ProtocolVersion = HttpVersion.Version10;
            request.ContentType = "application/xml";
            request.Method = "POST";

            StringBuilder sb = new StringBuilder();
            using (StreamReader sr = new StreamReader(xmlFilePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    sb.AppendLine(line);
                }
                byte[] postBytes = Encoding.UTF8.GetBytes(sb.ToString());

                if (timeout < 0)
                {
                    request.ReadWriteTimeout = timeout;
                    request.Timeout = timeout;
                }

                request.ContentLength = postBytes.Length;

                try
                {
                    Stream requestStream = request.GetRequestStream();

                    requestStream.Write(postBytes, 0, postBytes.Length);
                    requestStream.Close();

                    using (var response = (HttpWebResponse)request.GetResponse())
                    {
                        return response.ToString();
                    }
                }
                catch (Exception ex)
                {
                    request.Abort();
                    return string.Empty;
                }
            }
        }
    }
}

