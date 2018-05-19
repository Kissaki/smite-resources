package apidata

// API functions
// no parameters
const (
	Ping = "ping"
)

// API functions
// must be followed by Format/DevID/signature/timestamp
const (
	CreateSession = "createsession"
	TestSession   = "testsession"
	ServerStatus  = "gethirezserverstatus"
	DataUsed      = "getdataused"
	// Deployed patch version
	Version = "getpatchinfo"
	// 20 most recent MOTDs
	MOTDs      = "getmotd"
	TopMatches = "gettopmatches"
)

// API function sub paths; must be followed by Format/DevID/signature/session/timestamp/languageCode
const (
	Gods  = "getgods"
	Items = "getitems"
)

// API functions for player data
// must be followed by Format/DevID/signature/timestamp/player
const (
	// Player and league information
	// /player
	Player = "getplayer"
	// /player
	PlayerStatus = "getplayerstatus"
	// /player
	PlayerMatchHistory = "getmatchhistory"
	// /player
	PlayerFriends = "getfriends"
	// /player
	PlayerGodRanks = "getgodranks"
	// /player/queue
	// Match​ ​summary​ ​statistics​ ​for​ ​a​ ​(player,​ ​queue)​ ​combination​ ​grouped​ ​by​ ​gods​ ​played
	PlayerQueueStats = "getqueuestats"
	// /playerID
	PlayerAchievements = "getplayerachievements"
)

// API functions for match data
// must be followed by Format/DevID/signature/timestamp/matchid
const (
	Match = "getmatchdetails"
	// /matchid,matchid,...
	MatchBatch = "getmatchdetailsbatch"
	MatchLive  = "getmatchplayerdetails"
	// use getmatchdetails instead
	MatchDeprecated = "getdemodetails"
)

// API function sub paths; must be followed by Format/DevID/signature/timestamp/god/lang
const (
	// /god/lang
	GodSkins = "getgodskins"
	// /god/lang
	GodRecommendedItems = "getgodrecommendeditems"
)

// API functions for clan data
// must be followed by Format/DevID/signature/timestamp/god/lang plus any additional commented parameter
const (
	// /clanid
	Clan = "getteamdetails"
	// /clanid
	ClanMembers = "getteamplayers"
	// /searchstring
	ClanSearch = "searchteams"
)

// API functions for leaderboards etc
// must be followed by Format/DevID/signature/timestamp, plus specified additional parameters
const (
	// Current season
	SPL = "getesportsproleaguedetails"
	// /god/queue
	// Current season
	GodLeaderboard = "getgodleaderboard"
	// /queue/tier/season
	LeagueLeaderboards = "getleagueleaderboard"
	// /queue
	LeagueSeasons = "getleagueseasons"
	// /queue/date/hour
	MatchIDs = "getmatchidsbyqueue"
)
