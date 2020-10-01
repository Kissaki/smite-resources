using System;
using System.Text.Json.Serialization;

namespace KCode.SMITEAPI.ResultTypes
{
    [Serializable]
    public partial class Ability
    {
        [JsonPropertyName("Description")]
        public AbilityDescription? Description { get; set; }

        [JsonPropertyName("Id")]
        public int? Id { get; set; }

        [JsonPropertyName("Summary")]
        public string? Summary { get; set; }

        [JsonPropertyName("URL")]
        public Uri? Url { get; set; }
    }
}
