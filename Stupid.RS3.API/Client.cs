
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Stupid.RS3.API
{
    public class Client
    {
        private HttpClient _httpClient;

        public Client()
        {
            _httpClient = new HttpClient();
        }
       
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
    }
}
