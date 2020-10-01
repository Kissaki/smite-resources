using KCode.SMITEAPI.Reference;
using KCode.SMITEAPI.ResultTypes;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace KCode.SMITEAPI
{
    public sealed class RequestClient : IDisposable
    {
        private readonly DataFormat _dataFormat;
        private readonly PlatformEndpoint _endpoint;
        private readonly HttpClient _client = new HttpClient();
        private SessionData? _session = null;

        /// <param name="endpoint">A <see cref="PlatformEndpoint"/> or null. On null <see cref="PlatformEndpoint.SMITE"/> will be used.</param>
        /// <param name="dataFormat">If not specified <see cref="DataFormat.JSON"/> will be used</param>
        public RequestClient(PlatformEndpoint? endpoint = null, DataFormat? dataFormat = null)
        {
            _endpoint = endpoint ?? PlatformEndpoint.SMITE;
            _dataFormat = dataFormat ?? DataFormat.JSON;
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        public void Configure(int devId, string authKey) => _session = new SessionData(devId: devId, authKey: authKey);
        public void Configure(int devId, string authKey, string sessionId, DateTime authDateTime) => _session = new SessionData(devId: devId, authKey: authKey, sessionId: sessionId, authDateTime: authDateTime);

        public Task<string> SendPingAsync() => SendAsync(path: Methods.Ping.Fill().Format(_dataFormat).Finish());

        private Task<string> SendAsync(string path)
        {
            var url = $"{_endpoint}{path}";
            Console.Error.WriteLine($"TRACE: Sending request to `{url}`…");
            return _client.GetStringAsync(url);
        }

        private Task<string> CreateSessionAsync()
        {
            if (_session == null) throw new InvalidOperationException("Client is not configured yet");
            if (_session.SessionId != null && _session.AuthDateTime > DateTime.UtcNow.AddMinutes(-10)) throw new InvalidOperationException("Still more than 5 minutes left on current session. Misuse.");
            return SendAsync(Methods.Createsession.Fill().Format(_dataFormat).Auth(_session).Timestamp().Finish());
        }

        public Task<string> TestSessionAsync()
        {
            EnsureSession();
            return SendAsync(Methods.Testsession.Fill().Format(_dataFormat).Auth(_session!).Timestamp().Finish());
        }

        private void EnsureSession()
        {
            if (_session == null) throw new InvalidOperationException("Client is not configured yet");
            if (_session.IsValid) return;
            if (_session.IsValidForAWhile) return;

            var s = SessionStore.Get();
            if (s != null)
            {
                _session = s;
                return;
            }

            var t = CreateSessionAsync();
            var result = t.Result;
            var res = JsonSerializer.Deserialize<CreateSessionResult>(result);
            if (res.ResultMessage != "Approved") throw new InvalidOperationException($"Creating an authentication session failed. Result: {res.ResultMessage}");
            _session.SessionId = res.SessionId;
            _session.AuthDateTime = res.TimestampParsed;

            SessionStore.Store(_session);
        }

        #region God

        public Task<string> GetGodsAsync(LangCode langCode = LangCode.English)
        {
            EnsureSession();
            return SendAsync(Methods.Getgods.Fill().Format(_dataFormat).Auth(_session!).Timestamp().LangCode(langCode).Finish());
        }

        public Task<string> GetGodSkinsAsync(int godId, LangCode langCode = LangCode.English)
        {
            EnsureSession();
            return SendAsync(Methods.Getgodskins.Fill().Format(_dataFormat).Auth(_session!).Timestamp().God(godId).LangCode(langCode).Finish());
        }

        #endregion God

        #region Items

        public Task<string> GetItemsAsync(LangCode langCode = LangCode.English)
        {
            EnsureSession();
            return SendAsync(Methods.Getitems.Fill().Format(_dataFormat).Auth(_session!).Timestamp().LangCode(langCode).Finish());
        }

        #endregion Items

        #region Player

        public Task<string> GetPlayerHeadAsync(string playername)
        {
            EnsureSession();
            var url = Methods.Getplayeridbyname.Fill().Format(_dataFormat).Auth(_session!).Timestamp().Playername(playername).Finish();
            return SendAsync(url);
        }

        public Task<string> GetPlayerAsync(int playerId)
        {
            EnsureSession();
            var url = Methods.Getplayer.Fill().Format(_dataFormat).Auth(_session!).Timestamp().Player(playerId).Finish();
            return SendAsync(url);
        }

        public Task<string> GetPlayerFriendsAsync(int playerId)
        {
            EnsureSession();
            var url = Methods.Getfriends.Fill().Format(_dataFormat).Auth(_session!).Timestamp().PlayerId(playerId).Finish();
            return SendAsync(url);
        }

        public Task<string> GetPlayerGodRanksAsync(int playerId)
        {
            EnsureSession();
            var url = Methods.Getgodranks.Fill().Format(_dataFormat).Auth(_session!).Timestamp().PlayerId(playerId).Finish();
            return SendAsync(url);
        }

        // PALADINS ONLY
        public Task<string> GetPlayerChampionRanksAsync(int playerId)
        {
            EnsureSession();
            var url = Methods.Getchampionranks.Fill().Format(_dataFormat).Auth(_session!).Timestamp().PlayerId(playerId).Finish();
            return SendAsync(url);
        }

        // PALADINS ONLY
        public Task<string> GetPlayerLoadoutsAsync(int playerId)
        {
            EnsureSession();
            var url = Methods.Getplayerloadouts.Fill().Format(_dataFormat).Auth(_session!).Timestamp().PlayerId(playerId).Finish();
            return SendAsync(url);
        }

        // SMITE ONLY
        public Task<string> GetPlayerAchievementsAsync(int playerId)
        {
            EnsureSession();
            var url = Methods.Getplayerachievements.Fill().Format(_dataFormat).Auth(_session!).Timestamp().PlayerId(playerId).Finish();
            return SendAsync(url);
        }

        public Task<string> GetPlayerStatusAsync(int playerId)
        {
            EnsureSession();
            var url = Methods.Getplayerstatus.Fill().Format(_dataFormat).Auth(_session!).Timestamp().PlayerId(playerId).Finish();
            return SendAsync(url);
        }

        public Task<string> GetPlayerMatchHistoryAsync(int playerId)
        {
            EnsureSession();
            var url = Methods.Getmatchhistory.Fill().Format(_dataFormat).Auth(_session!).Timestamp().PlayerId(playerId).Finish();
            return SendAsync(url);
        }

        //public Task<string> GetPlayerQueueStatsAsync(int playerId)
        //{
        //    EnsureSession();
        //    var url = Methods.Getmatchhistory.Fill().Format(_dataFormat).Auth(_session!).Timestamp().PlayerId(playerId).Queue(queue).Finish();
        //    return SendAsync(url);
        //}

        //public Task<string> SearchPlayerAsync(string searchString)
        //{
        //    EnsureSession();
        //    var url = Methods.Getmatchhistory.Fill().Format(_dataFormat).Auth(_session!).Timestamp().SearchPlayer(searchString).Finish();
        //    return SendAsync(url);
        //}

        #endregion player
    }
}
