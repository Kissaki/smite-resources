namespace KCode.SMITEAPI.Constants
{
    public enum PlayerStatus
    {
        Offline = 0,
        // Basically anywhere except god selection or in game
        InLobby = 1,
        // Player has accepted match and is selecting god before start of game
        GodSelection = 2,
        // Match has started
        InGame = 3,
        // Player is logged in, but may be blocking broadcast of player state
        Online = 4,
        // Player not found
        Unknown = 5,
    }
}
