using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.Json.Serialization;

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
namespace KCode.SMITEAPI.ResultTypes
{
    [SuppressMessage(category: "Microsoft.Performance", checkId: "CA1812", Justification = "Used as tempalte parameter for JsonSerializer.Deserialize")]
    internal class CreateSessionResult : Result
    {
        /// <summary>
        /// Format: 0EC0EC0EC0EC0EC0EC0EC0EC0EC0EC0E
        /// </summary>
        [JsonPropertyName("session_id")]
        public string SessionId { get; set; }

        /// <summary>
        /// Format: 2/14/2013 7:50:20 PM
        /// </summary>
        [JsonPropertyName("timestamp")]
        public string Timestamp { get; set; }

        public DateTime TimestampParsed => DateTime.ParseExact(Timestamp, "M/d/yyyy h:mm:ss tt", provider: CultureInfo.GetCultureInfo("en-US"));
    }
}
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
