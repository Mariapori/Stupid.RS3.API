

using Newtonsoft.Json;
using System.Collections.Generic;

namespace Stupid.RS3.API
{
    public class QuestObject
    {
        [JsonProperty("quests")]
        public List<Quest> Quests { get; set; }
        [JsonProperty("loggedIn")]
        public bool LoggedIn { get; set; }
    }
    public class Quest
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("difficulty")]
        public Difficulty Difficulty { get; set; }
        [JsonProperty("members")]
        public bool Members { get; set; }
        [JsonProperty("questPoints")]
        public int QuestPoints { get; set; }
        [JsonProperty("userEligible")]
        public bool UserEligible { get; set; }
    }

    public enum Difficulty
    {
        Novice,
        Intermediate,
        Experienced,
        Master,
        Grandmaster,
        Special
    }

    public enum QuestStatus
    {
        NOT_STARTED,
        STARTED,
        COMPLETED
    }

}
