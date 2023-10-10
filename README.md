# Stupid.RS3.API C# Package

The **Stupid.RS3.API** C# package is a simple and lightweight API wrapper for interacting with the RuneScape 3 (RS3) API. This package is designed to make it easier to query various endpoints of the RuneScape API and retrieve game-related data.

## Installation

You can install the package via NuGet Package Manager. Open your project in Visual Studio or any other C# development environment and execute the following command in the NuGet Package Manager Console:

```powershell
Install-Package Stupid.RS3.API
```

## Quick Start

To use the **Stupid.RS3.API** package in your C# project, follow these steps:

1. Import the `Stupid.RS3.API` namespace in your C# file where you want to use the package:

   ```csharp
   using Stupid.RS3.API;
   ```

2. Create an instance of the `Client` class:

   ```csharp
   var rs3Client = new Client();
   ```

3. Use the `GetPlayerStatsByName` method to retrieve a player's statistics by their RuneScape 3 username:

   ```csharp
   string playerName = "YourPlayerName";
   var playerStats = await rs3Client.GetPlayerStatsByName(playerName);

   foreach (var stat in playerStats)
   {
       Console.WriteLine($"Skill: {stat.Skill}, Rank: {stat.Rank}, Level: {stat.Level}, Experience: {stat.Experience}");
   }
   ```

Replace `"YourPlayerName"` with the actual RuneScape 3 username you want to retrieve statistics for.

This code snippet demonstrates how to fetch player statistics using the **Stupid.RS3.API** package in a C# application.

### Example Usage

Here's an example of how to use the package to retrieve a player's RuneScape 3 statistics:

```csharp
using System;
using Stupid.RS3.API;

namespace RS3StatRetrieval
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Create an instance of the RS3 API client
            var rs3Client = new Client();

            // Define the player's RuneScape 3 username
            string playerName = "YourPlayerName";

            // Retrieve the player's statistics
            var playerStats = await rs3Client.GetPlayerStatsByName(playerName);

            // Display the player's statistics
            foreach (var stat in playerStats)
            {
                Console.WriteLine($"Skill: {stat.Skill}, Rank: {stat.Rank}, Level: {stat.Level}, Experience: {stat.Experience}");
            }
        }
    }
}
```

Remember to replace `"YourPlayerName"` with the actual username you want to retrieve statistics for.
