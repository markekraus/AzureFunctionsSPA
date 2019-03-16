using Newtonsoft.Json;

namespace Markekraus.Mekspaaf.Models.Graph
{
    public class GraphMe
    {
        [JsonProperty("businessPhones")]
        public string[] BusinessPhones { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("givenName")]
        public string GivenName { get; set; }

        [JsonProperty("jobTitle")]
        public string JobTitle { get; set; }

        [JsonProperty("mail")]
        public string Mail { get; set; }

        [JsonProperty("mobilePhone")]
        public string MobilePhone { get; set; }

        [JsonProperty("officeLocation")]
        public string OfficeLocation { get; set; }

        [JsonProperty("preferredLanguage")]
        public string PreferredLanguage { get; set; }

        [JsonProperty("surname")]
        public string surname { get; set; }

        [JsonProperty("userPrincipalName")]
        public string userPrincipalName { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
