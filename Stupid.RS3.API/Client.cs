
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Stupid.RS3.API
{
    public class Client
    {
        private HttpClient _httpClient;

        /// <summary>
        /// Client for querying data 
        /// </summary>
        public Client()
        {
            _httpClient = new HttpClient();
        }
       
        /// <summary>
        /// Hiscores Lite 
        /// </summary>
        /// <param name="name">RuneScape 3 player name</param>
        /// <returns>Player's stats</returns>
        public async Task<List<Stats>> GetPlayerStatsByName(string name)
        {
            var response = await _httpClient.GetStringAsync($"https://secure.runescape.com/m=hiscore/index_lite.ws?player={name}");
            var rows = response.Split('\n');
            var stats = new List<Stats>();
            for (int i = 0; i < 30; i++)
            {
                var split = rows[i].Split(',');
                var skill = new Stats();
                skill.Skill = (Skills)i;
                skill.Rank = int.Parse(split[0]);
                skill.Level = int.Parse(split[1]);
                skill.Experience = int.Parse(split[2]);
                stats.Add(skill);
            }
            return stats;
        }

        public async Task<List<Quest>> GetPlayerQuests(string name, Difficulty? filterDifficulty = null, QuestStatus? filterStatus = null)
        {
            var response = await _httpClient.GetStringAsync($"https://apps.runescape.com/runemetrics/quests?user={name}");
            var objects = JsonConvert.DeserializeObject<QuestObject>(response).Quests.ToList();
            if (filterDifficulty.HasValue)
            {
                objects = objects.Where(o => o.Difficulty == filterDifficulty).ToList();
            }
            if (filterStatus.HasValue)
            {
                objects = objects.Where(o => o.Status.ToLower().Contains(filterStatus.ToString().ToLower())).ToList();
            }
            return objects;
        }
    }
}
