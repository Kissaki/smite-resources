using System;
using System.Text.Json.Serialization;

namespace KCode.SMITEAPI.ResultTypes
{
    [Serializable]
    internal partial class PlayerResult : Result
    {
        [JsonPropertyName("Id")]
        public long Id { get; set; }

        [JsonPropertyName("Name")]
        public string? Name { get; set; }

        [JsonPropertyName("hz_gamer_tag")]
        public object? HzGamerTag { get; set; }

        [JsonPropertyName("hz_player_name")]
        public string? HzPlayerName { get; set; }

        [JsonPropertyName("Region")]
        /// <summary>May be one of: Europe, …</summary>
        public string? Region { get; set; }

        [JsonPropertyName("Platform")]
        public string? Platform { get; set; }

        #region Account Information

        [JsonPropertyName("Created_Datetime")]
        public string? CreatedDatetime { get; set; }

        [JsonPropertyName("Last_Login_Datetime")]
        public string? LastLoginDatetime { get; set; }

        [JsonPropertyName("Personal_Status_Message")]
        public string? PersonalStatusMessage { get; set; }

        #endregion

        #region Account Stats

        [JsonPropertyName("Level")]
        public long Level { get; set; }

        [JsonPropertyName("MasteryLevel")]
        public long MasteryLevel { get; set; }

        [JsonPropertyName("HoursPlayed")]
        public long HoursPlayed { get; set; }

        [JsonPropertyName("Leaves")]
        public long Leaves { get; set; }

        [JsonPropertyName("Losses")]
        public long Losses { get; set; }

        [JsonPropertyName("Total_Achievements")]
        public long TotalAchievements { get; set; }

        [JsonPropertyName("Total_Worshippers")]
        public long TotalWorshippers { get; set; }

        [JsonPropertyName("Wins")]
        public long Wins { get; set; }

        #endregion

        #region Account Additional Metadata

        [JsonPropertyName("MergedPlayers")]
        public object? MergedPlayers { get; set; }

        [JsonPropertyName("ActivePlayerId")]
        public long ActivePlayerId { get; set; }

        [JsonPropertyName("Avatar_URL")]
        public string? AvatarUrl { get; set; }

        #endregion

        #region Clan

        [JsonPropertyName("TeamId")]
        public long TeamId { get; set; }

        [JsonPropertyName("Team_Name")]
        public string? TeamName { get; set; }

        #endregion

        #region Ranked

        [JsonPropertyName("Rank_Stat_Conquest")]
        public double RankStatConquest { get; set; }

        [JsonPropertyName("Rank_Stat_Conquest_Controller")]
        public double RankStatConquestController { get; set; }

        [JsonPropertyName("Rank_Stat_Duel")]
        public double RankStatDuel { get; set; }

        [JsonPropertyName("Rank_Stat_Duel_Controller")]
        public double RankStatDuelController { get; set; }

        [JsonPropertyName("Rank_Stat_Joust")]
        public double RankStatJoust { get; set; }

        [JsonPropertyName("Rank_Stat_Joust_Controller")]
        public double RankStatJoustController { get; set; }

        [JsonPropertyName("RankedConquest")]
        public RankedResult? RankedConquest { get; set; }

        [JsonPropertyName("RankedConquestController")]
        public RankedResult? RankedConquestController { get; set; }

        [JsonPropertyName("RankedDuel")]
        public RankedResult? RankedDuel { get; set; }

        [JsonPropertyName("RankedDuelController")]
        public RankedResult? RankedDuelController { get; set; }

        [JsonPropertyName("RankedJoust")]
        public RankedResult? RankedJoust { get; set; }

        [JsonPropertyName("RankedJoustController")]
        public RankedResult? RankedJoustController { get; set; }

        [JsonPropertyName("Tier_Conquest")]
        public long TierConquest { get; set; }

        [JsonPropertyName("Tier_Duel")]
        public long TierDuel { get; set; }

        [JsonPropertyName("Tier_Joust")]
        public long TierJoust { get; set; }

        #endregion
    }
}
