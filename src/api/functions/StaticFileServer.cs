using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

using static Markekraus.Mekspaaf.Util.Utilities;

namespace Markekraus.Mekspaaf
{
    /// <summary>
    /// Servers static files from the STATIC_FILES_PATH folder
    /// </summary>
    public static class StaticFileServer
    {
        [FunctionName("StaticFileServer")]
        public static HttpResponseMessage Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string mySource = nameof(StaticFileServer);
            log.LogInformation($"{mySource} triggered");
            try
            {
                var filePath = GetFilePath(req, log);
                log.LogInformation($"{mySource} filePath {filePath}");
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                var stream = new FileStream(filePath, FileMode.Open);
                response.Content = new StreamContent(stream);
                response.Content.Headers.ContentType = 
                    new MediaTypeHeaderValue(GetMimeType(filePath));
                return response;
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
        }
    }
}
