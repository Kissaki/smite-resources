using KCode.SMITEAPI.ResultTypes;
using System.Text.Json.Serialization;

namespace KCode.SMITEClient.Data
{
    [Serializable]
    public class GodJsonModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("latestGod")]
        public string? LatestGod { get; set; }

        [JsonPropertyName("Name")]
        public string? Name { get; set; }

        [JsonPropertyName("OnFreeRotation")]
        public string? OnFreeRotation { get; set; }

        [JsonPropertyName("Pantheon")]
        public string? Pantheon { get; set; }

        [JsonPropertyName("Lore")]
        public string? Lore { get; set; }

        #region Stats

        [JsonPropertyName("AttackSpeed")]
        public double? AttackSpeed { get; set; }

        [JsonPropertyName("AttackSpeedPerLevel")]
        public double? AttackSpeedPerLevel { get; set; }

        [JsonPropertyName("HP5PerLevel")]
        public double? Hp5PerLevel { get; set; }

        [JsonPropertyName("Health")]
        public int? Health { get; set; }

        [JsonPropertyName("HealthPerFive")]
        public int? HealthPerFive { get; set; }

        [JsonPropertyName("HealthPerLevel")]
        public int? HealthPerLevel { get; set; }

        [JsonPropertyName("MP5PerLevel")]
        public double? Mp5PerLevel { get; set; }

        [JsonPropertyName("MagicProtection")]
        public int? MagicProtection { get; set; }

        [JsonPropertyName("MagicProtectionPerLevel")]
        public double? MagicProtectionPerLevel { get; set; }

        [JsonPropertyName("MagicalPower")]
        public int? MagicalPower { get; set; }

        [JsonPropertyName("MagicalPowerPerLevel")]
        public double? MagicalPowerPerLevel { get; set; }

        [JsonPropertyName("Mana")]
        public int? Mana { get; set; }

        [JsonPropertyName("ManaPerFive")]
        public double? ManaPerFive { get; set; }

        [JsonPropertyName("ManaPerLevel")]
        public int? ManaPerLevel { get; set; }

        [JsonPropertyName("basicAttack")]
        public AbilityDescription? BasicAttack { get; set; }

        [JsonPropertyName("PhysicalPower")]
        public int? PhysicalPower { get; set; }

        [JsonPropertyName("PhysicalPowerPerLevel")]
        public double? PhysicalPowerPerLevel { get; set; }

        [JsonPropertyName("PhysicalProtection")]
        public int? PhysicalProtection { get; set; }

        [JsonPropertyName("PhysicalProtectionPerLevel")]
        public double? PhysicalProtectionPerLevel { get; set; }

        #endregion Stats

        #region Abilities

        [JsonPropertyName("Ability_1")]
        public Ability? Ability1 { get; set; }
        [JsonPropertyName("AbilityId1")]
        public int? Ability1Id { get; set; }
        [JsonPropertyName("Ability1")]
        public string? Ability1Name { get; set; }
        [JsonPropertyName("godAbility1_URL")]
        public Uri? GodAbility1Url { get; set; }
        [JsonPropertyName("abilityDescription1")]
        public AbilityDescription? AbilityDescription1 { get; set; }

        [JsonPropertyName("Ability_2")]
        public Ability? Ability2 { get; set; }
        [JsonPropertyName("AbilityId2")]
        public int? Ability2Id { get; set; }
        [JsonPropertyName("Ability2")]
        public string? Ability2Name { get; set; }
        [JsonPropertyName("godAbility2_URL")]
        public Uri? GodAbility2Url { get; set; }
        [JsonPropertyName("abilityDescription2")]
        public AbilityDescription? AbilityDescription2 { get; set; }

        [JsonPropertyName("Ability_3")]
        public Ability? Ability3 { get; set; }
        [JsonPropertyName("AbilityId3")]
        public int? Ability3Id { get; set; }
        [JsonPropertyName("Ability3")]
        public string? Ability3Name { get; set; }
        [JsonPropertyName("godAbility3_URL")]
        public Uri? GodAbility3Url { get; set; }
        [JsonPropertyName("abilityDescription3")]
        public AbilityDescription? AbilityDescription3 { get; set; }

        [JsonPropertyName("Ability_4")]
        public Ability? Ability4 { get; set; }
        [JsonPropertyName("AbilityId4")]
        public int? Ability4Id { get; set; }
        [JsonPropertyName("Ability4")]
        public string? Ability4Name { get; set; }
        [JsonPropertyName("godAbility4_URL")]
        public Uri? GodAbility4Url { get; set; }
        [JsonPropertyName("abilityDescription4")]
        public AbilityDescription? AbilityDescription4 { get; set; }

        [JsonPropertyName("Ability_5")]
        public Ability? Ability5 { get; set; }
        [JsonPropertyName("AbilityId5")]
        public int? Ability5Id { get; set; }
        [JsonPropertyName("Ability5")]
        public string? Ability5Name { get; set; }
        [JsonPropertyName("godAbility5_URL")]
        public Uri? GodAbility5Url { get; set; }
        [JsonPropertyName("abilityDescription5")]
        public AbilityDescription? AbilityDescription5 { get; set; }

        #endregion Abilities

        #region Evaluation

        [JsonPropertyName("Cons")]
        public string? Cons { get; set; }

        #endregion Evaluation

        [JsonPropertyName("Pros")]
        public string? Pros { get; set; }

        [JsonPropertyName("Roles")]
        public string? Roles { get; set; }

        [JsonPropertyName("Speed")]
        public int? Speed { get; set; }

        [JsonPropertyName("Title")]
        public string? Title { get; set; }

        [JsonPropertyName("Type")]
        public string? Type { get; set; }


        [JsonPropertyName("godCard_URL")]
        public Uri? GodCardUrl { get; set; }

        [JsonPropertyName("godIcon_URL")]
        public Uri? GodIconUrl { get; set; }

        #region Extension
        [JsonPropertyName("godskins")]
        public GodSkin[]? Skins { get; set; }
        #endregion Extension
    }
}
