struct MethodType
    name::String
end

# Anonymous; no parameters
const Ping = MethodType("ping")
# End

# Parameters: Format/DevID/signature/timestamp
const CreateSession = MethodType("createsession")
# End

# Parameters: Format/DevID/signature/session/timestamp
const TestSession = MethodType("testsession")
const ServerStatus = MethodType("gethirezserverstatus")
const ApiQuota = MethodType("getdataused")
# Deployed patch version
const Version = MethodType("getpatchinfo")
# 20 most recent MOTDs
const MOTDs      = MethodType("getmotd")
"""Lists the 50 most watched / most recent recorded matches."""
const TopMatches = MethodType("gettopmatches")
# Current season
const SPL = MethodType("getesportsproleaguedetails")
# End

# Parameters (lang): Format/DevID/signature/session/timestamp/languageCode
const GetGods = MethodType("getgods")
const GetItems = MethodType("getitems")
# End

# Parameters (player): Format/DevID/signature/timestamp/player
# /player
const Player = MethodType("getplayer")
# /player
# 0 - Offline
# 1 - In Lobby  (basically anywhere except god selection or in game)
# 2 - god Selection (player has accepted match and is selecting god before start of game)
# 3 - In Game (match has started)
# 4 - Online (player is logged in, but may be blocking broadcast of player state)
# 5 - Unknown (player not found)
const PlayerStatus = MethodType("getplayerstatus")
# /player
const PlayerMatchHistory = MethodType("getmatchhistory")
# /player
const PlayerFriends = MethodType("getfriends")
# /player
const PlayerGodRanks = MethodType("getgodranks")
# /player/queue
# Match​ ​summary​ ​statistics​ ​for​ ​a​ ​(player,​ ​queue)​ ​combination​ ​grouped​ ​by​ ​gods​ ​played
const PlayerQueueStats = MethodType("getqueuestats")
# /playerID
const PlayerAchievements = MethodType("getplayerachievements")
# End

# Parameters (match): Format/DevID/signature/timestamp/matchid
const Match = MethodType("getmatchdetails")
# /matchid,matchid,...
const Matches = MethodType("getmatchdetailsbatch")
const MatchLive  = MethodType("getmatchplayerdetails")
# use getmatchdetails instead
const MatchDeprecated = MethodType("getdemodetails")
# End

# Parameters (god): Format/DevID/signature/timestamp/god/lang
# /god/lang
const GodSkins = MethodType("getgodskins")
# /god/lang
const GodRecommendedItems = MethodType("getgodrecommendeditems")
# End

# Parameters (clan): Format/DevID/signature/timestamp/god/lang plus any additional commented parameter
# /clanid
const Clan = MethodType("getteamdetails")
# /clanid
const ClanMembers = MethodType("getteamplayers")
# /searchstring
const ClanSearch = MethodType("searchteams")
# End

# Parameters (misc): Format/DevID/signature/timestamp, plus specified additional parameters
# /god/queue
# Current season
const GodLeaderboard = MethodType("getgodleaderboard")
# /queue/tier/season
const LeagueLeaderboards = MethodType("getleagueleaderboard")
# /queue
const LeagueSeasons = MethodType("getleagueseasons")
# /queue/date/hour
const MatchIDs = MethodType("getmatchidsbyqueue")
# End
