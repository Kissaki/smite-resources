using System;
using System.IO;
using System.Text.Json;

namespace KCode.SMITEAPI
{
    internal static class SessionStore
    {
        [Serializable]
        private class SessionObject
        {
            public int? DevId { get; set; }
            public string? AuthKey { get; set; }
            public string? SessionId { get; set; }
            public DateTime? AuthDateTime { get; set; }

            public bool IsValid() => DevId != null && AuthKey != null;
            public bool HasId() => SessionId != null && AuthDateTime != null;

            public SessionObject() { }
            public SessionObject(SessionData other) { DevId = other.DevId; AuthKey = other.AuthKey; SessionId = other.SessionId; AuthDateTime = other.AuthDateTime; }

            public SessionData? ToSession()
            {
                if (!IsValid()) return null;
                if (!HasId()) return new SessionData(DevId!.Value, AuthKey!);
                if (SessionId == null || AuthDateTime == null) return null;
                return new SessionData(DevId!.Value, AuthKey!, SessionId!, AuthDateTime.Value);
            }
        }

        private const string Filename = ".tmp.session";

        public static void Store(SessionData session)
        {
            var fi = new FileInfo(Filename);
            // The stored session data is newer than the session creation date -> do not write
            if (fi.Exists && fi.LastWriteTimeUtc < session.AuthDateTime) return;
            var json = JsonSerializer.Serialize(new SessionObject(session));
            File.WriteAllText(fi.FullName, json);
        }

        public static SessionData? Get()
        {
            var fi = new FileInfo(Filename);
            if (!fi.Exists) return null;

            var json = File.ReadAllText(fi.FullName);
            var read = JsonSerializer.Deserialize<SessionObject>(json)?.ToSession();

            if (read?.IsValidForAWhile ?? false) return read;

            fi.Delete();
            return null;
        }
    }
}
