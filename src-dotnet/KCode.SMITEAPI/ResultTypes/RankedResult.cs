using System;
using System.Text.Json.Serialization;

namespace KCode.SMITEAPI.ResultTypes
{
    [Serializable]
    internal class RankedResult : Result
    {
        [JsonPropertyName("Leaves")]
        public long Leaves { get; set; }

        [JsonPropertyName("Losses")]
        public long Losses { get; set; }

        [JsonPropertyName("Name")]
        public string? Name { get; set; }

        [JsonPropertyName("Points")]
        public long Points { get; set; }

        [JsonPropertyName("PrevRank")]
        public long PrevRank { get; set; }

        [JsonPropertyName("Rank")]
        public long Rank { get; set; }

        [JsonPropertyName("Rank_Stat")]
        public double RankStat { get; set; }

        [JsonPropertyName("Rank_Variance")]
        public double RankVariance { get; set; }

        [JsonPropertyName("Season")]
        public long Season { get; set; }

        [JsonPropertyName("Tier")]
        public long Tier { get; set; }

        [JsonPropertyName("Trend")]
        public long Trend { get; set; }

        [JsonPropertyName("Wins")]
        public long Wins { get; set; }

        [JsonPropertyName("player_id")]
        public object? PlayerId { get; set; }
    }
}
