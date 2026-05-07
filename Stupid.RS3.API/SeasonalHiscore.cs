
using Newtonsoft.Json;

namespace Stupid.RS3.API
{
    public class SeasonalRanking
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("score")]
        public int Score { get; set; }
        [JsonProperty("rank")]
        public int Rank { get; set; }
        [JsonProperty("date")]
        public string Date { get; set; }
    }

    public class SeasonalHiscoreDetails
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("start")]
        public string Start { get; set; }
        [JsonProperty("end")]
        public string End { get; set; }
    }
}
