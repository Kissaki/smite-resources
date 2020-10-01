using System;
using System.Text.Json.Serialization;

namespace KCode.SMITEAPI.ResultTypes
{
    [Serializable]
    internal class PlayerIdResult : Result
    {
        [JsonPropertyName("player_id")]
        public int? PlayerId { get; set; }

        [JsonPropertyName("portal_id")]
        public string? PortalId { get; set; }

        [JsonPropertyName("Portal")]
        public string? PortalName { get; set; }

        /// <summary>
        /// 'y' or 'n'
        /// </summary>
        [JsonPropertyName("privacy_flag")]
        public string? PrivacyFlag { get; set; }
    }
}
