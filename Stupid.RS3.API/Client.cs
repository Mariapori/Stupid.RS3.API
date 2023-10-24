
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
        /// <summary>
        /// Get player quests
        /// </summary>
        /// <param name="name">Player's name</param>
        /// <param name="filterDifficulty">Get only filtered difficulty</param>
        /// <param name="filterStatus">Get only filtered status</param>
        /// <returns>Player quests</returns>
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
        /// <summary>
        /// Get online player count (OSRS/RS3)
        /// </summary>
        /// <returns>Get online player count, if fails returns 0</returns>
        public async Task<int> GetOnlinePlayerCount()
        {
            try
            {
                var response = await _httpClient.GetStringAsync("http://www.runescape.com/player_count.js?varname=iPlayerCount&callback=jQuery000000000000000_0000000000&_=0");
                response = response.Remove(0, 33);
                var split = response.Split(')');
                int.TryParse(split[0], out int result);
                return result;
            }
            catch
            {
                return 0;
            }

        }

        public async Task<XpMonthly.Root> GetMonthlyXp(string name,Skills? skill = null)
        {
            var baseUrl = $"https://apps.runescape.com/runemetrics/xp-monthly?searchName={name.ToLower()}";
            if(skill.HasValue && skill != Skills.Overall)
            {
                baseUrl += $"&skillid={(int)skill}";
            }
            var response = await _httpClient.GetStringAsync(baseUrl);
            var json = JsonConvert.DeserializeObject<XpMonthly.Root>(response);
            return json;
        } 
    }
}
