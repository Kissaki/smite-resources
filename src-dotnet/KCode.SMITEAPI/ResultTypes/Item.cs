using System.Text.Json.Serialization;

namespace KCode.SMITEAPI.ResultTypes
{
    public partial class Item
    {
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("value")]
        public string? Value { get; set; }
    }
}
