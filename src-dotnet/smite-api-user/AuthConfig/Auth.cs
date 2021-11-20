namespace KCode.SMITEClient.AuthConfig
{
    public readonly record struct Auth(int DevId, string AuthKey);


    //[Serializable]
    //public record Auth
    //{
    //    public int DevId { get; internal init; }
    //    public string AuthKey { get; internal init; }

    //    public Auth(int devId, string authKey) { DevId = devId; AuthKey = authKey; }
    //}
}
