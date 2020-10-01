using System.Text.Json.Serialization;

namespace KCode.SMITEAPI.ResultTypes
{
    internal class PlayerAchievementsResult : Result
    {
        [JsonPropertyName("AssistedKills")]
        public long AssistedKills { get; set; }

        [JsonPropertyName("CampsCleared")]
        public long CampsCleared { get; set; }

        [JsonPropertyName("Deaths")]
        public long Deaths { get; set; }

        [JsonPropertyName("DivineSpree")]
        public long DivineSpree { get; set; }

        [JsonPropertyName("DoubleKills")]
        public long DoubleKills { get; set; }

        [JsonPropertyName("FireGiantKills")]
        public long FireGiantKills { get; set; }

        [JsonPropertyName("FirstBloods")]
        public long FirstBloods { get; set; }

        [JsonPropertyName("GodLikeSpree")]
        public long GodLikeSpree { get; set; }

        [JsonPropertyName("GoldFuryKills")]
        public long GoldFuryKills { get; set; }

        [JsonPropertyName("Id")]
        public long Id { get; set; }

        [JsonPropertyName("ImmortalSpree")]
        public long ImmortalSpree { get; set; }

        [JsonPropertyName("KillingSpree")]
        public long KillingSpree { get; set; }

        [JsonPropertyName("MinionKills")]
        public long MinionKills { get; set; }

        [JsonPropertyName("Name")]
        public string? Name { get; set; }

        [JsonPropertyName("PentaKills")]
        public long PentaKills { get; set; }

        [JsonPropertyName("PhoenixKills")]
        public long PhoenixKills { get; set; }

        [JsonPropertyName("PlayerKills")]
        public long PlayerKills { get; set; }

        [JsonPropertyName("QuadraKills")]
        public long QuadraKills { get; set; }

        [JsonPropertyName("RampageSpree")]
        public long RampageSpree { get; set; }

        [JsonPropertyName("ShutdownSpree")]
        public long ShutdownSpree { get; set; }

        [JsonPropertyName("SiegeJuggernautKills")]
        public long SiegeJuggernautKills { get; set; }

        [JsonPropertyName("TowerKills")]
        public long TowerKills { get; set; }

        [JsonPropertyName("TripleKills")]
        public long TripleKills { get; set; }

        [JsonPropertyName("UnstoppableSpree")]
        public long UnstoppableSpree { get; set; }

        [JsonPropertyName("WildJuggernautKills")]
        public long WildJuggernautKills { get; set; }
    }
}
