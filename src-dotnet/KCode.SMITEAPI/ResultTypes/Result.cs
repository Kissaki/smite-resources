using System;
using System.Text.Json.Serialization;

namespace KCode.SMITEAPI.ResultTypes
{
    [Serializable]
    public abstract class Result
    {
        /// <summary>
        /// May be 'Approved'
        /// </summary>
        [JsonPropertyName("ret_msg")]
        public string? ResultMessage { get; set; }
    }
}
