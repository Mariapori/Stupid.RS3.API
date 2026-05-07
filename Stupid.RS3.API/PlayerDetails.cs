
using Newtonsoft.Json;

namespace Stupid.RS3.API
{
    public class PlayerDetails
    {
        [JsonProperty("isSuffix")]
        public bool IsSuffix { get; set; }
        [JsonProperty("recruiting")]
        public bool Recruiting { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("clan")]
        public string Clan { get; set; }
    }
}
