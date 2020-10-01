using System;
using System.Text.Json.Serialization;

namespace KCode.SMITEAPI.ResultTypes
{
    [Serializable]
    public class AbilityItemDescription
    {
#pragma warning disable CA1819 // Properties should not return arrays
        [JsonPropertyName("cooldown")]
        public string? Cooldown { get; set; }

        [JsonPropertyName("cost")]
        public string? Cost { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("menuitems")]
        public Item[]? Menuitems { get; set; }

        [JsonPropertyName("rankitems")]
        public Item[]? Rankitems { get; set; }
#pragma warning restore CA1819 // Properties should not return arrays
    }
}
