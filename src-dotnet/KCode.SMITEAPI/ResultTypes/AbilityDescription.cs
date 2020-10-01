using System;
using System.Text.Json.Serialization;

namespace KCode.SMITEAPI.ResultTypes
{
    [Serializable]
    public class AbilityDescription
    {
        [JsonPropertyName("itemDescription")]
        public AbilityItemDescription? Description { get; set; }
    }
}
