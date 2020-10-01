using System.Text.Json.Serialization;

namespace KCode.SMITEAPI.ResultTypes
{
    internal class PlayerMatchHistoryResult : Result
    {
        [JsonPropertyName("ActiveId1")]
        public long ActiveId1 { get; set; }

        [JsonPropertyName("ActiveId2")]
        public long ActiveId2 { get; set; }

        [JsonPropertyName("Active_1")]
        public string? Active1 { get; set; }

        [JsonPropertyName("Active_2")]
        public string? Active2 { get; set; }

        [JsonPropertyName("Active_3")]
        public string? Active3 { get; set; }

        [JsonPropertyName("Assists")]
        public long Assists { get; set; }

        [JsonPropertyName("Ban1")]
        public string? Ban1 { get; set; }

        [JsonPropertyName("Ban10")]
        public string? Ban10 { get; set; }

        [JsonPropertyName("Ban10Id")]
        public long Ban10Id { get; set; }

        [JsonPropertyName("Ban1Id")]
        public long Ban1Id { get; set; }

        [JsonPropertyName("Ban2")]
        public string? Ban2 { get; set; }

        [JsonPropertyName("Ban2Id")]
        public long Ban2Id { get; set; }

        [JsonPropertyName("Ban3")]
        public string? Ban3 { get; set; }

        [JsonPropertyName("Ban3Id")]
        public long Ban3Id { get; set; }

        [JsonPropertyName("Ban4")]
        public string? Ban4 { get; set; }

        [JsonPropertyName("Ban4Id")]
        public long Ban4Id { get; set; }

        [JsonPropertyName("Ban5")]
        public string? Ban5 { get; set; }

        [JsonPropertyName("Ban5Id")]
        public long Ban5Id { get; set; }

        [JsonPropertyName("Ban6")]
        public string? Ban6 { get; set; }

        [JsonPropertyName("Ban6Id")]
        public long Ban6Id { get; set; }

        [JsonPropertyName("Ban7")]
        public string? Ban7 { get; set; }

        [JsonPropertyName("Ban7Id")]
        public long Ban7Id { get; set; }

        [JsonPropertyName("Ban8")]
        public string? Ban8 { get; set; }

        [JsonPropertyName("Ban8Id")]
        public long Ban8Id { get; set; }

        [JsonPropertyName("Ban9")]
        public string? Ban9 { get; set; }

        [JsonPropertyName("Ban9Id")]
        public long Ban9Id { get; set; }

        [JsonPropertyName("Creeps")]
        public long Creeps { get; set; }

        [JsonPropertyName("Damage")]
        public long Damage { get; set; }

        [JsonPropertyName("Damage_Bot")]
        public long DamageBot { get; set; }

        [JsonPropertyName("Damage_Done_In_Hand")]
        public long DamageDoneInHand { get; set; }

        [JsonPropertyName("Damage_Mitigated")]
        public long DamageMitigated { get; set; }

        [JsonPropertyName("Damage_Structure")]
        public long DamageStructure { get; set; }

        [JsonPropertyName("Damage_Taken")]
        public long DamageTaken { get; set; }

        [JsonPropertyName("Damage_Taken_Magical")]
        public long DamageTakenMagical { get; set; }

        [JsonPropertyName("Damage_Taken_Physical")]
        public long DamageTakenPhysical { get; set; }

        [JsonPropertyName("Deaths")]
        public long Deaths { get; set; }

        [JsonPropertyName("Distance_Traveled")]
        public long DistanceTraveled { get; set; }

        [JsonPropertyName("First_Ban_Side")]
        public string? FirstBanSide { get; set; }

        [JsonPropertyName("God")]
        public string? God { get; set; }

        [JsonPropertyName("GodId")]
        public long GodId { get; set; }

        [JsonPropertyName("Gold")]
        public long Gold { get; set; }

        [JsonPropertyName("Healing")]
        public long Healing { get; set; }

        [JsonPropertyName("Healing_Bot")]
        public long HealingBot { get; set; }

        [JsonPropertyName("Healing_Player_Self")]
        public long HealingPlayerSelf { get; set; }

        [JsonPropertyName("ItemId1")]
        public long ItemId1 { get; set; }

        [JsonPropertyName("ItemId2")]
        public long ItemId2 { get; set; }

        [JsonPropertyName("ItemId3")]
        public long ItemId3 { get; set; }

        [JsonPropertyName("ItemId4")]
        public long ItemId4 { get; set; }

        [JsonPropertyName("ItemId5")]
        public long ItemId5 { get; set; }

        [JsonPropertyName("ItemId6")]
        public long ItemId6 { get; set; }

        [JsonPropertyName("Item_1")]
        public string? Item1 { get; set; }

        [JsonPropertyName("Item_2")]
        public string? Item2 { get; set; }

        [JsonPropertyName("Item_3")]
        public string? Item3 { get; set; }

        [JsonPropertyName("Item_4")]
        public string? Item4 { get; set; }

        [JsonPropertyName("Item_5")]
        public string? Item5 { get; set; }

        [JsonPropertyName("Item_6")]
        public string? Item6 { get; set; }

        [JsonPropertyName("Killing_Spree")]
        public long KillingSpree { get; set; }

        [JsonPropertyName("Kills")]
        public long Kills { get; set; }

        [JsonPropertyName("Level")]
        public long Level { get; set; }

        [JsonPropertyName("Map_Game")]
        public string? MapGame { get; set; }

        [JsonPropertyName("Match")]
        public long Match { get; set; }

        [JsonPropertyName("Match_Queue_Id")]
        public long MatchQueueId { get; set; }

        [JsonPropertyName("Match_Time")]
        public string? MatchTime { get; set; }

        [JsonPropertyName("Minutes")]
        public long Minutes { get; set; }

        [JsonPropertyName("Multi_kill_Max")]
        public long MultiKillMax { get; set; }

        [JsonPropertyName("Objective_Assists")]
        public long ObjectiveAssists { get; set; }

        [JsonPropertyName("Queue")]
        public string? Queue { get; set; }

        [JsonPropertyName("Region")]
        public string? Region { get; set; }

        [JsonPropertyName("Skin")]
        public string? Skin { get; set; }

        [JsonPropertyName("SkinId")]
        public long SkinId { get; set; }

        [JsonPropertyName("Surrendered")]
        public long Surrendered { get; set; }

        [JsonPropertyName("TaskForce")]
        public long TaskForce { get; set; }

        [JsonPropertyName("Team1Score")]
        public long Team1Score { get; set; }

        [JsonPropertyName("Team2Score")]
        public long Team2Score { get; set; }

        [JsonPropertyName("Time_In_Match_Seconds")]
        public long TimeInMatchSeconds { get; set; }

        [JsonPropertyName("Wards_Placed")]
        public long WardsPlaced { get; set; }

        [JsonPropertyName("Win_Status")]
        public string? WinStatus { get; set; }

        [JsonPropertyName("Winning_TaskForce")]
        public long WinningTaskForce { get; set; }

        [JsonPropertyName("playerId")]
        public long PlayerId { get; set; }

        [JsonPropertyName("playerName")]
        public string? PlayerName { get; set; }
    }
}
