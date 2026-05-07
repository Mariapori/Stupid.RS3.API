
using Newtonsoft.Json;

namespace Stupid.RS3.API
{
    public class ClanMember
    {
        public string Name { get; set; }
        public string Rank { get; set; }
        public long TotalXp { get; set; }
        public int Kills { get; set; }
    }

    public class ClanRanking
    {
        [JsonProperty("clanName")]
        public string ClanName { get; set; }
        [JsonProperty("clanScore")]
        public long ClanScore { get; set; }
        [JsonProperty("rank")]
        public int Rank { get; set; }
    }
}
