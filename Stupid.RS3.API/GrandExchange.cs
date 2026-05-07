
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Stupid.RS3.API
{
    public class GrandExchange
    {
        public class Info
        {
            [JsonProperty("lastConfigUpdateRuneday")]
            public int LastUpdateRuneday { get; set; }
        }

        public class Category
        {
            [JsonProperty("types")]
            public List<LetterCount> Types { get; set; }
        }

        public class LetterCount
        {
            [JsonProperty("letter")]
            public string Letter { get; set; }
            [JsonProperty("items")]
            public int Items { get; set; }
        }

        public class ItemList
        {
            [JsonProperty("total")]
            public int Total { get; set; }
            [JsonProperty("items")]
            public List<Item> Items { get; set; }
        }

        public class Item
        {
            [JsonProperty("icon")]
            public string Icon { get; set; }
            [JsonProperty("icon_large")]
            public string IconLarge { get; set; }
            [JsonProperty("id")]
            public int Id { get; set; }
            [JsonProperty("type")]
            public string Type { get; set; }
            [JsonProperty("typeIcon")]
            public string TypeIcon { get; set; }
            [JsonProperty("name")]
            public string Name { get; set; }
            [JsonProperty("description")]
            public string Description { get; set; }
            [JsonProperty("current")]
            public PriceTrend Current { get; set; }
            [JsonProperty("today")]
            public PriceTrend Today { get; set; }
            [JsonProperty("members")]
            public string Members { get; set; }
        }

        public class DetailItem : Item
        {
            [JsonProperty("day30")]
            public ChangeTrend Day30 { get; set; }
            [JsonProperty("day90")]
            public ChangeTrend Day90 { get; set; }
            [JsonProperty("day180")]
            public ChangeTrend Day180 { get; set; }
        }

        public class DetailWrapper
        {
            [JsonProperty("item")]
            public DetailItem Item { get; set; }
        }

        public class PriceTrend
        {
            [JsonProperty("trend")]
            public string Trend { get; set; }
            [JsonProperty("price")]
            public string Price { get; set; }
        }

        public class ChangeTrend
        {
            [JsonProperty("trend")]
            public string Trend { get; set; }
            [JsonProperty("change")]
            public string Change { get; set; }
        }

        public class Graph
        {
            [JsonProperty("daily")]
            public Dictionary<string, int> Daily { get; set; }
            [JsonProperty("average")]
            public Dictionary<string, int> Average { get; set; }
        }
    }
}
