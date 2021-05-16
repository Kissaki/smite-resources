using System;

namespace KCode.SMITEAPI
{
    public class PlatformEndpoint
    {
        public static readonly PlatformEndpoint SMITE = new PlatformEndpoint(@"https://api.smitegame.com/smiteapi.svc");
        public static readonly PlatformEndpoint Paladins = new PlatformEndpoint(@"https://api.paladins.com/paladinsapi.svc");

        public Uri BaseUrl { get; }

        public PlatformEndpoint(string baseUrl) : this(new Uri(baseUrl)) {}

        public PlatformEndpoint(Uri baseUrl)
        {
            BaseUrl = baseUrl;
        }

        public override string ToString() => BaseUrl.ToString();
    }
}
