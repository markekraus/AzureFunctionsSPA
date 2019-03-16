using Newtonsoft.Json;
using System.Collections.Generic;

namespace Markekraus.Mekspaaf.Models.Graph
{
    public class ApiMeResponse
    {
        [JsonProperty("me")]
        public IList<GraphMe> Me { get; set; }
    }
}