using System.Text.Json.Serialization;

namespace KCode.SMITEAPI.ResultTypes
{
    internal class PlayerFriendResult : Result
    {
        [JsonPropertyName("player_id")]
        public long PlayerId { get; set; }

        [JsonPropertyName("account_id")]
        public long AccountId { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        // 1 for friend, 32 for blocked, … (?)
        [JsonPropertyName("friend_flags")]
        public long FriendFlags { get; set; }

        // Friend or Blocked
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("avatar_url")]
        public string? AvatarUrl { get; set; }
    }
}
