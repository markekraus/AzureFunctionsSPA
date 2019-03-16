using Markekraus.Mekspaaf.Models.Graph;
using Markekraus.Mekspaaf.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

using static Markekraus.Mekspaaf.Util.Utilities;

namespace Markekraus.Mekspaaf
{
    public static class Me
    {
        public const string EndpointUrl = "https://graph.microsoft.com/v1.0/me/";
        public const string Resource = "https://graph.microsoft.com";
        [FunctionName("me")]
        public static async Task<ApiMeResponse> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get")]
            HttpRequest req,
            [Token(Identity = TokenIdentityMode.UserFromRequest, Resource = Resource)]
            string graphToken,
            ILogger log)
        {
            string source = nameof(Me);
            log.LogInformation($"{source} triggered");

            var response = await WebClient.GetRestAsync(EndpointUrl, graphToken, log);

            log.LogInformation($"{source} Process response");
            var responseString = await response.Content.ReadAsStringAsync();
            DebugLog(log,$"{source} Response {responseString}");

            var me = JsonConvert.DeserializeObject<GraphMe>(responseString);

            return new ApiMeResponse(){Me = new List<GraphMe>(){me}};
        }
    }
}
