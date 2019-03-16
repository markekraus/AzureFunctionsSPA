using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using static Markekraus.Mekspaaf.Util.Utilities;

namespace Markekraus.Mekspaaf.Util
{
    public static class WebClient
    {
        public static HttpClient Client = new HttpClient();

        public static async Task<HttpResponseMessage> GetRestAsync(string Uri, string Token, ILogger log, [System.Runtime.CompilerServices.CallerMemberName] string caller = "unknown")
        {
            var source = $"{caller}.{nameof(GetRestAsync)}";
            log.LogInformation($"{source} begin");

            DebugLog(log, $"{source} Token {Token}");
            log.LogInformation($"{source} Uri {Uri}");

            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(Uri));
            request.Headers.Authorization = new AuthenticationHeaderValue("bearer", Token);
            DebugLog(log, $"{source} request {request.ToString()}");

            var sw = new Stopwatch();

            log.LogInformation($"{source} Send request");
            sw.Start();
            var response = await Client.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            sw.Stop();
            log.LogMetric($"{source}.SendAsync",sw.ElapsedMilliseconds, new Dictionary<string, object>(){{"Uri",Uri}});
            log.LogInformation($"{source} Request finished in {sw.ElapsedMilliseconds} ms");

            log.LogInformation($"{source} Done");
            return response;
        }
    }
}