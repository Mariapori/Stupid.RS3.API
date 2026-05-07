
using Newtonsoft.Json;

namespace Stupid.RS3.API
{
    public class HiscoreRanking
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("score")]
        public int Score { get; set; }
        [JsonProperty("rank")]
        public int Rank { get; set; }
    }
}
