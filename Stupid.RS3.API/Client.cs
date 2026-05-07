
using Newtonsoft.Json;
using System;
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

        // ── Hiscores ──────────────────────────────────────────────────────────

        /// <summary>
        /// Hiscores Lite — player's rank, level, and experience across all skills
        /// </summary>
        public async Task<List<Stats>> GetPlayerStatsByName(string name)
        {
            var response = await _httpClient.GetStringAsync($"https://secure.runescape.com/m=hiscore/index_lite.ws?player={name}");
            return ParseHiscoreCsv(response);
        }

        /// <summary>
        /// Ironman Hiscores Lite
        /// </summary>
        public async Task<List<Stats>> GetPlayerIronmanStats(string name)
        {
            var response = await _httpClient.GetStringAsync($"https://secure.runescape.com/m=hiscore_ironman/index_lite.ws?player={name}");
            return ParseHiscoreCsv(response);
        }

        /// <summary>
        /// Hardcore Ironman Hiscores Lite
        /// </summary>
        public async Task<List<Stats>> GetPlayerHardcoreIronmanStats(string name)
        {
            var response = await _httpClient.GetStringAsync($"https://secure.runescape.com/m=hiscore_hardcore_ironman/index_lite.ws?player={name}");
            return ParseHiscoreCsv(response);
        }

        /// <summary>
        /// Returns up to the top 50 players for a given skill or activity table
        /// </summary>
        /// <param name="table">Skill/activity table ID (0 = Overall, 1 = Attack, …)</param>
        /// <param name="size">Number of results, max 50</param>
        public async Task<List<HiscoreRanking>> GetHiscoreRanking(int table, int size = 50)
        {
            var response = await _httpClient.GetStringAsync($"https://secure.runescape.com/m=hiscore/ranking.json?table={table}&category=0&size={size}");
            return JsonConvert.DeserializeObject<List<HiscoreRanking>>(response);
        }

        // ── Seasonal Hiscores ─────────────────────────────────────────────────

        /// <summary>
        /// Player's scores for the current (or archived) seasonal event
        /// </summary>
        public async Task<List<SeasonalRanking>> GetSeasonalRankings(string player, bool archived = false)
        {
            var url = $"https://secure.runescape.com/m=temp-hiscores/getRankings.json?player={Uri.EscapeDataString(player)}";
            if (archived) url += "&status=archived";
            var response = await _httpClient.GetStringAsync(url);
            return JsonConvert.DeserializeObject<List<SeasonalRanking>>(response);
        }

        /// <summary>
        /// Metadata for the current (or archived) seasonal event
        /// </summary>
        public async Task<List<SeasonalHiscoreDetails>> GetSeasonalHiscoreDetails(bool archived = false)
        {
            var url = "https://secure.runescape.com/m=temp-hiscores/getHiscoreDetails.json";
            if (archived) url += "?status=archived";
            var response = await _httpClient.GetStringAsync(url);
            return JsonConvert.DeserializeObject<List<SeasonalHiscoreDetails>>(response);
        }

        // ── Clan Hiscores ─────────────────────────────────────────────────────

        /// <summary>
        /// Top three clans by total experience
        /// </summary>
        public async Task<List<ClanRanking>> GetClanRanking()
        {
            var response = await _httpClient.GetStringAsync("https://secure.runescape.com/m=clan-hiscores/clanRanking.json");
            return JsonConvert.DeserializeObject<List<ClanRanking>>(response);
        }

        /// <summary>
        /// All members of a clan with their rank, total XP, and kills
        /// </summary>
        public async Task<List<ClanMember>> GetClanMembers(string clanName)
        {
            var response = await _httpClient.GetStringAsync($"https://secure.runescape.com/m=clan-hiscores/members_lite.ws?clanName={Uri.EscapeDataString(clanName)}");
            var lines = response.Split('\n');
            var members = new List<ClanMember>();
            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i])) continue;
                var split = lines[i].Split(',');
                if (split.Length < 4) continue;
                members.Add(new ClanMember
                {
                    Name = split[0].Trim(),
                    Rank = split[1].Trim(),
                    TotalXp = long.Parse(split[2].Trim()),
                    Kills = int.Parse(split[3].Trim())
                });
            }
            return members;
        }

        // ── Grand Exchange ────────────────────────────────────────────────────

        /// <summary>
        /// Last update timestamp of the Grand Exchange database
        /// </summary>
        public async Task<GrandExchange.Info> GetGrandExchangeInfo()
        {
            var response = await _httpClient.GetStringAsync("https://secure.runescape.com/m=itemdb_rs/api/info.json");
            return JsonConvert.DeserializeObject<GrandExchange.Info>(response);
        }

        /// <summary>
        /// Item counts organised by first letter for a GE category (0–43)
        /// </summary>
        public async Task<GrandExchange.Category> GetGrandExchangeCategory(int categoryId)
        {
            var response = await _httpClient.GetStringAsync($"https://secure.runescape.com/m=itemdb_rs/api/catalogue/category.json?category={categoryId}");
            return JsonConvert.DeserializeObject<GrandExchange.Category>(response);
        }

        /// <summary>
        /// Paginated list of tradeable items within a GE category
        /// </summary>
        /// <param name="categoryId">Category ID (0–43)</param>
        /// <param name="alpha">Starting letter (lowercase; use "%23" for numbers)</param>
        /// <param name="page">Page number, starts at 1 — up to 12 items per page</param>
        public async Task<GrandExchange.ItemList> GetGrandExchangeItems(int categoryId, string alpha, int page = 1)
        {
            var response = await _httpClient.GetStringAsync($"https://secure.runescape.com/m=itemdb_rs/api/catalogue/items.json?category={categoryId}&alpha={alpha}&page={page}");
            return JsonConvert.DeserializeObject<GrandExchange.ItemList>(response);
        }

        /// <summary>
        /// Current price, trend, and 30/90/180-day change for a GE item
        /// </summary>
        public async Task<GrandExchange.DetailItem> GetGrandExchangeDetail(int itemId)
        {
            var response = await _httpClient.GetStringAsync($"https://secure.runescape.com/m=itemdb_rs/api/catalogue/detail.json?item={itemId}");
            return JsonConvert.DeserializeObject<GrandExchange.DetailWrapper>(response).Item;
        }

        /// <summary>
        /// Daily prices and 30-day moving average for the past 180 days
        /// </summary>
        public async Task<GrandExchange.Graph> GetGrandExchangeGraph(int itemId)
        {
            var response = await _httpClient.GetStringAsync($"https://secure.runescape.com/m=itemdb_rs/api/graph/{itemId}.json");
            return JsonConvert.DeserializeObject<GrandExchange.Graph>(response);
        }

        // ── Runemetrics ───────────────────────────────────────────────────────

        /// <summary>
        /// Full RuneMetrics profile — skills, XP, combat level, quest counts, and activity log
        /// </summary>
        /// <param name="activities">Number of recent activity entries to include (default 20)</param>
        public async Task<RunemetricsProfile.Root> GetRunemetricsProfile(string name, int activities = 20)
        {
            var response = await _httpClient.GetStringAsync($"https://apps.runescape.com/runemetrics/profile/profile?user={Uri.EscapeDataString(name)}&activities={activities}");
            return JsonConvert.DeserializeObject<RunemetricsProfile.Root>(response);
        }

        /// <summary>
        /// Player's quest completion statuses, with optional filters
        /// </summary>
        public async Task<List<Quest>> GetPlayerQuests(string name, Difficulty? filterDifficulty = null, QuestStatus? filterStatus = null)
        {
            var response = await _httpClient.GetStringAsync($"https://apps.runescape.com/runemetrics/quests?user={name}");
            var objects = JsonConvert.DeserializeObject<QuestObject>(response).Quests.ToList();
            if (filterDifficulty.HasValue)
                objects = objects.Where(o => o.Difficulty == filterDifficulty).ToList();
            if (filterStatus.HasValue)
                objects = objects.Where(o => o.Status.ToLower().Contains(filterStatus.ToString().ToLower())).ToList();
            return objects;
        }

        /// <summary>
        /// Monthly XP gains for the past 12 months, optionally filtered to a single skill
        /// </summary>
        public async Task<XpMonthly.Root> GetMonthlyXp(string name, Skills? skill = null)
        {
            var baseUrl = $"https://apps.runescape.com/runemetrics/xp-monthly?searchName={name.ToLower()}";
            if (skill.HasValue && skill != Skills.Overall)
                baseUrl += $"&skillid={(int)skill}";
            var response = await _httpClient.GetStringAsync(baseUrl);
            return JsonConvert.DeserializeObject<XpMonthly.Root>(response);
        }

        // ── Website Data ──────────────────────────────────────────────────────

        /// <summary>
        /// Title, clan affiliation, and recruitment status for one or more players
        /// </summary>
        public async Task<List<PlayerDetails>> GetPlayerDetails(string[] names)
        {
            var namesJson = "[\"" + string.Join("\",\"", names) + "\"]";
            var url = $"https://secure.runescape.com/m=website-data/playerDetails.ws?names={Uri.EscapeDataString(namesJson)}&callback=jQuery000000000000000_0000000000&_=0";
            var response = await _httpClient.GetStringAsync(url);
            // Strip JSONP wrapper: jQuery000000000000000_0000000000([...])
            var start = response.IndexOf('(') + 1;
            var end = response.LastIndexOf(')');
            var json = response.Substring(start, end - start);
            return JsonConvert.DeserializeObject<List<PlayerDetails>>(json);
        }

        // ── Other ─────────────────────────────────────────────────────────────

        /// <summary>
        /// Current online player count across RuneScape and OSRS — returns 0 on failure
        /// </summary>
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

        /// <summary>
        /// Total number of accounts registered across all RuneScape platforms
        /// </summary>
        public async Task<long> GetTotalRegisteredUsers()
        {
            var response = await _httpClient.GetStringAsync("https://secure.runescape.com/m=account-creation-reports/rsusertotal.ws");
            long.TryParse(response.Trim(), out long result);
            return result;
        }

        // ── Private Helpers ───────────────────────────────────────────────────

        private List<Stats> ParseHiscoreCsv(string response)
        {
            var rows = response.Split('\n');
            var stats = new List<Stats>();
            for (int i = 0; i < 30; i++)
            {
                var split = rows[i].Split(',');
                stats.Add(new Stats
                {
                    Skill = (Skills)i,
                    Rank = int.Parse(split[0]),
                    Level = int.Parse(split[1]),
                    Experience = int.Parse(split[2])
                });
            }
            return stats;
        }
    }
}
