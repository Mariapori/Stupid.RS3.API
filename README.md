# Stupid.RS3.API Usage Guide
[![Build and Unit tests](https://github.com/Mariapori/Stupid.RS3.API/actions/workflows/dotnet.yml/badge.svg)](https://github.com/Mariapori/Stupid.RS3.API/actions/workflows/dotnet.yml)
## Introduction

The **Stupid.RS3.API** C# package provides a simple and lightweight API wrapper for interacting with the RuneScape 3 (RS3) API.

### Installation

```powershell
Install-Package Stupid.RS3.API
```

## Methods

### Hiscores

#### `GetPlayerStatsByName(string name)`
Returns a player's rank, level, and experience across all 30 skills from the RS3 Hiscores.

#### `GetPlayerIronmanStats(string name)`
Returns a player's stats from the Ironman Hiscores.

#### `GetPlayerHardcoreIronmanStats(string name)`
Returns a player's stats from the Hardcore Ironman Hiscores.

#### `GetHiscoreRanking(int table, int size = 50)`
Returns up to the top 50 players for a given skill or activity table (0 = Overall, 1 = Attack, …).

### Seasonal Hiscores

#### `GetSeasonalRankings(string player, bool archived = false)`
Returns a player's scores for the current or archived seasonal event.

#### `GetSeasonalHiscoreDetails(bool archived = false)`
Returns metadata (title, dates, status) for the current or archived seasonal event.

### Clan Hiscores

#### `GetClanRanking()`
Returns the top three clans by total experience.

#### `GetClanMembers(string clanName)`
Returns all members of a clan with their rank, total XP, and kills.

### Grand Exchange

#### `GetGrandExchangeInfo()`
Returns the last update timestamp of the Grand Exchange database.

#### `GetGrandExchangeCategory(int categoryId)`
Returns item counts organised by first letter for a GE category (IDs 0–43).

#### `GetGrandExchangeItems(int categoryId, string alpha, int page = 1)`
Returns a paginated list of tradeable items within a category (up to 12 per page).

#### `GetGrandExchangeDetail(int itemId)`
Returns the current price, trend, and 30/90/180-day change data for an item.

#### `GetGrandExchangeGraph(int itemId)`
Returns daily prices and 30-day moving average for the past 180 days.

### RuneMetrics

#### `GetRunemetricsProfile(string name, int activities = 20)`
Returns a player's full RuneMetrics profile — skills, total XP, combat level, quest counts, and recent activity log.

#### `GetPlayerQuests(string name, Difficulty? filterDifficulty = null, QuestStatus? filterStatus = null)`
Returns a player's quest completion statuses with optional filters.

#### `GetMonthlyXp(string name, Skills? skill = null)`
Returns monthly XP gains for the past 12 months, optionally filtered to a single skill.

### Website Data

#### `GetPlayerDetails(string[] names)`
Returns title, clan affiliation, and recruitment status for one or more players.

### Other

#### `GetOnlinePlayerCount()`
Returns the combined online player count for RuneScape and OSRS. Returns 0 on failure.

#### `GetTotalRegisteredUsers()`
Returns the total number of accounts registered across all RuneScape platforms.

## Usage Examples

### Retrieve player quests

```csharp
var client = new Client();
var quests = await client.GetPlayerQuests("PlayerName");
foreach (var quest in quests)
    Console.WriteLine($"{quest.Title}: {quest.Status}");

// Filter by difficulty and status
var completed = await client.GetPlayerQuests("PlayerName", Difficulty.Experienced, QuestStatus.COMPLETED);
```

### Look up a Grand Exchange item

```csharp
var client = new Client();
var detail = await client.GetGrandExchangeDetail(4151); // Abyssal whip
Console.WriteLine($"{detail.Name}: {detail.Current.Price}");

var graph = await client.GetGrandExchangeGraph(4151);
foreach (var entry in graph.Daily)
    Console.WriteLine($"{entry.Key}: {entry.Value}gp");
```

### Get a player's RuneMetrics profile

```csharp
var client = new Client();
var profile = await client.GetRunemetricsProfile("PlayerName");
Console.WriteLine($"Combat level: {profile.CombatLevel}, Total XP: {profile.TotalXp}");
```

### Get clan members

```csharp
var client = new Client();
var members = await client.GetClanMembers("My Clan");
foreach (var member in members)
    Console.WriteLine($"{member.Name} ({member.Rank}): {member.TotalXp:N0} XP");
```

## Release Notes

### Version 1.1.0

- Added Grand Exchange methods: `GetGrandExchangeInfo`, `GetGrandExchangeCategory`, `GetGrandExchangeItems`, `GetGrandExchangeDetail`, `GetGrandExchangeGraph`
- Added `GetRunemetricsProfile` for full RuneMetrics profile data
- Added `GetHiscoreRanking` for top players by skill or activity
- Added `GetPlayerIronmanStats` and `GetPlayerHardcoreIronmanStats`
- Added clan hiscore methods: `GetClanRanking`, `GetClanMembers`
- Added `GetPlayerDetails` for player title and clan information
- Added `GetSeasonalRankings` and `GetSeasonalHiscoreDetails` for seasonal events
- Added `GetTotalRegisteredUsers` for total registered account count

### Version 1.0.3

- Added `GetMonthlyXp` for querying monthly XP data

### Version 1.0.2

- Added `GetOnlinePlayerCount` to retrieve online player count (OSRS/RS3)

### Version 1.0.1

- Added `GetPlayerQuests` with optional filtering by difficulty and status

### Version 1.0.0

- Initial release with `GetPlayerStatsByName` for retrieving player hiscore statistics
