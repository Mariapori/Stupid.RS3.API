# Stupid.RS3.API Usage Guide
[![Build and Unit tests](https://github.com/Mariapori/Stupid.RS3.API/actions/workflows/dotnet.yml/badge.svg)](https://github.com/Mariapori/Stupid.RS3.API/actions/workflows/dotnet.yml)
## Introduction

The **Stupid.RS3.API** C# package provides a simple and lightweight API wrapper for interacting with the RuneScape 3 (RS3) API. This guide will walk you through the usage of the package, including how to retrieve player quest information and filter quests based on difficulty and status.

### Installation

To install the **Stupid.RS3.API** package, you can use the NuGet Package Manager in your C# project. Execute the following command in the NuGet Package Manager Console:

```powershell
Install-Package Stupid.RS3.API
```

## Usage

### Retrieving Player Quests

The `GetPlayerQuests` method allows you to retrieve a player's quest information. You can optionally filter the quests based on their difficulty and status.

#### Method Signature

```csharp
public async Task<List<Quest>> GetPlayerQuests(string name, Difficulty? filterDifficulty = null, QuestStatus? filterStatus = null)
```

- `name` (string): The RuneScape 3 username for which you want to retrieve quest information.
- `filterDifficulty` (optional): Filter quests by their difficulty (optional).
- `filterStatus` (optional): Filter quests by their status (optional).

#### Example Usage

```csharp
using System;
using Stupid.RS3.API;

namespace RS3QuestRetrieval
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Create an instance of the RS3 API client
            var rs3Client = new Client();

            // Define the player's RuneScape 3 username
            string playerName = "YourPlayerName";

            // Retrieve the player's quests (no filters applied)
            var playerQuests = await rs3Client.GetPlayerQuests(playerName);

            // Display the player's quests
            foreach (var quest in playerQuests)
            {
                Console.WriteLine($"Title: {quest.Title}, Status: {quest.Status}, Difficulty: {quest.Difficulty}");
            }
        }
    }
}
```

### Filter by Difficulty and Status

You can filter quests by difficulty and status by providing the optional parameters:

```csharp
var filteredQuests = await rs3Client.GetPlayerQuests(playerName, Difficulty.Experienced, QuestStatus.COMPLETED);
```

This example retrieves quests that are of "Experienced" difficulty and have a "COMPLETED" status.

## Release Notes

### Version 1.0.2

- Added `GetOnlinePlayerCount` method to retrieve online player count (OSRS/RS3)

### Version 1.0.1

- Added the `GetPlayerQuests` method to retrieve a player's quest information.
- Introduced optional filtering based on quest difficulty and status.
- Updated underlying dependencies for improved performance and compatibility.

### Version 1.0.0

- Initial release with basic functionality for retrieving player statistics.
- Sets the foundation for future expansion and improvements.

Please note that the above release notes are for version 1.0.2, which is an upcoming release. You can refer to the package's documentation for release notes and updates on the current version.

This guide and the release notes should help you get started with the **Stupid.RS3.API** package and understand the new features introduced in version 1.0.2.
