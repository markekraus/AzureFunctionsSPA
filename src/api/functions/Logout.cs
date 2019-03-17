using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace  Markekraus.Mekspaaf
{
    public static class Logout
    {
        [FunctionName("logout")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a logout request.");

            var response = new HttpResponseMessage();
            response.Content = new StringContent("Logging out...");
            response.Headers.Add("Set-Cookie", "AppServiceAuthSession=deleted; path=/; expires=Thu, 01 Jan 1970 00:00:00 GMT");
            response.Headers.Location = new Uri("https://dev.azure.com");
            response.StatusCode = HttpStatusCode.Redirect;
            response.ReasonPhrase = "logout";

            return await Task.FromResult(response);
        }
    }
}
