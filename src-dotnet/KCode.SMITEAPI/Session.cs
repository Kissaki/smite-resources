using System;
using System.Text.Json.Serialization;

namespace KCode.SMITEAPI
{
    [Serializable]
    internal class SessionData
    {
        public static TimeSpan Lifetime => TimeSpan.FromMinutes(15);
        public static TimeSpan RenewAfter => TimeSpan.FromMinutes(13);

        [JsonPropertyName("DevId")]
        public int DevId { get; }
        [JsonPropertyName("AuthKey")]
        public string AuthKey { get; }

        [JsonPropertyName("SessionId")]
        public string? SessionId { get; set; }
        [JsonPropertyName("AuthDateTime")]
        public DateTime? AuthDateTime { get; set; }

        public bool IsValid => SessionId != null && AuthDateTime > DateTime.UtcNow.Add(-Lifetime);
        public bool IsValidForAWhile => IsValid && AuthDateTime > DateTime.UtcNow.Add(-RenewAfter);

        public SessionData(int devId, string authKey)
        {
            DevId = devId;
            AuthKey = authKey;
        }

        public SessionData(int devId, string authKey, string sessionId, DateTime authDateTime)
            : this(devId, authKey)
        {
            SessionId = sessionId;
            AuthDateTime = authDateTime;
        }
    }
}
