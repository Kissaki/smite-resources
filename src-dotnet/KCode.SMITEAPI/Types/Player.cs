using KCode.SMITEAPI.Constants;
using KCode.SMITEAPI.ResultTypes;

namespace KCode.SMITEAPI.Types
{
    public class PlayerHead
    {
        internal static PlayerHead? FromResult(PlayerIdResult pid)
        {
            if (pid.PlayerId == null || pid.PortalId == null || pid.PrivacyFlag == null) return null;
            return new PlayerHead(id: pid.PlayerId.Value, portal: (PortalId)int.Parse(pid.PortalId, provider: null), isProfilePrivate: pid.PrivacyFlag == "n");
        }

        public int PlayerId { get; }
        public PortalId Portal { get; }
        public bool IsProfilePrivate { get; }

        internal PlayerHead(int id, PortalId portal, bool isProfilePrivate)
        {
            PlayerId = id;
            Portal = portal;
            IsProfilePrivate = isProfilePrivate;
        }

        public override string ToString()
        {
            return $"Player {PlayerId}: {Portal} {(IsProfilePrivate ? "private" : "")}";
        }
    }
}
