
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Stupid.RS3.API
{
    public class RunemetricsProfile
    {
        public class Root
        {
            [JsonProperty("loggedIn")]
            public string LoggedIn { get; set; }
            [JsonProperty("name")]
            public string Name { get; set; }
            [JsonProperty("rank")]
            public string Rank { get; set; }
            [JsonProperty("totalxp")]
            public long TotalXp { get; set; }
            [JsonProperty("combatlevel")]
            public int CombatLevel { get; set; }
            [JsonProperty("totalskill")]
            public int TotalSkill { get; set; }
            [JsonProperty("questscomplete")]
            public int QuestsComplete { get; set; }
            [JsonProperty("questsstarted")]
            public int QuestsStarted { get; set; }
            [JsonProperty("questsnotstarted")]
            public int QuestsNotStarted { get; set; }
            [JsonProperty("skillvalues")]
            public List<SkillValue> SkillValues { get; set; }
            [JsonProperty("activities")]
            public List<Activity> Activities { get; set; }
            [JsonProperty("magic")]
            public int Magic { get; set; }
            [JsonProperty("melee")]
            public int Melee { get; set; }
            [JsonProperty("ranged")]
            public int Ranged { get; set; }
        }

        public class SkillValue
        {
            [JsonProperty("id")]
            public int Id { get; set; }
            [JsonProperty("level")]
            public int Level { get; set; }
            [JsonProperty("xp")]
            public long Xp { get; set; }
            [JsonProperty("rank")]
            public int Rank { get; set; }
        }

        public class Activity
        {
            [JsonProperty("date")]
            public string Date { get; set; }
            [JsonProperty("details")]
            public string Details { get; set; }
            [JsonProperty("text")]
            public string Text { get; set; }
        }
    }
}
