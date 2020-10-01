using KCode.SMITEAPI.Reference;
using KCode.SMITEAPI.ResultTypes;
using KCode.SMITEAPI.Types;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace KCode.SMITEAPI
{
    public sealed class Client : IDisposable
    {
        private readonly PlatformEndpoint _endpoint;
        private readonly RequestClient _client;

        /// <param name="endpoint">A <see cref="PlatformEndpoint"/> or null. On null <see cref="PlatformEndpoint.SMITE"/> will be used.</param>
        public Client(PlatformEndpoint? endpoint = null)
        {
            _endpoint = endpoint ?? PlatformEndpoint.SMITE;
            _client = new RequestClient(_endpoint);
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        public void Configure(int devId, string authKey) => _client.Configure(devId, authKey);
        public void Configure(int devId, string authKey, string sessionId, DateTime authDateTime) => _client.Configure(devId, authKey, sessionId, authDateTime);

        public Task<PlayerHead[]> GetPlayerHeads(string playername)
            => _client.GetPlayerHeadAsync(playername)
                .ContinueWith(t =>
                {
                    var results = JsonSerializer.Deserialize<PlayerIdResult[]>(t.Result);
                    return results.Select(x => PlayerHead.FromResult(x)).Where(x => x != null).Cast<PlayerHead>().ToArray();
                }, TaskScheduler.Default);

        //public Task<Player[]> GetPlayer(int playerId)
        //    => _client.GetPlayerAsync(playerId).ContinueWith(t =>
        //{
        //    var results = JsonSerializer.Deserialize<PlayerResult[]>(t.Result);
        //    // TODO: Convert and typed model result
        //    //var players = results.Select(x => )
        //    return results[0].Name!;
        //}, TaskScheduler.Default);

        public Task<string> GetPlayerFriendsAsync(int playerId)
            => _client.GetPlayerFriendsAsync(playerId);
        //return SendAsync(url).ContinueWith(t =>
        //{
        //    var results = JsonSerializer.Deserialize<PlayerResult[]>(t.Result);
        //    // TODO: Convert and typed model result
        //    //var players = results.Select(x => )
        //    return results[0].Name!;
        //}, TaskScheduler.Default);

        //public Task<string> GetPlayerGodRanksAsync(int playerId)
        //=> _client.GetPlayerGodRanksAsync(playerId);

        public Task<string> GetPlayerGodRanksAsync(int playerId)
            => _client.GetPlayerGodRanksAsync(playerId).ContinueWith(t =>
            {
                var results = JsonSerializer.Deserialize<PlayerGodRanksResult[]>(t.Result);

                return string.Join("\n", results.Select(x => $"{x.God}: {x.Rank} with {x.Worshippers}"));
                // TODO: Convert and typed model result
                //var players = results.Select(x => )
                //return results[0].Name!;
            }, TaskScheduler.Default);
    }
}
