using System.Text.Json.Serialization;

namespace KCode.SMITEAPI.ResultTypes
{
    internal class PlayerGodRanksResult : Result
    {
        [JsonPropertyName("player_id")]
        public string? PlayerId { get; set; }

        [JsonPropertyName("god_id")]
        public string? GodId { get; set; }

        [JsonPropertyName("god")]
        public string? God { get; set; }

        [JsonPropertyName("Rank")]
        public int Rank { get; set; }

        [JsonPropertyName("Worshippers")]
        public int Worshippers { get; set; }

        #region God Stats

        [JsonPropertyName("Kills")]
        public int Kills { get; set; }
        [JsonPropertyName("Deaths")]
        public int Deaths { get; set; }
        [JsonPropertyName("Assists")]
        public int Assists { get; set; }
        [JsonPropertyName("MinionKills")]
        public int MinionKills { get; set; }

        [JsonPropertyName("Wins")]
        public int Wins { get; set; }
        [JsonPropertyName("Losses")]
        public int Losses { get; set; }

        #endregion God Stats
    }
}
