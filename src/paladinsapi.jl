module PaladinsApi

import MD5
import Dates
import JSON
import OrderedCollections

include("paladinsapi/endpoints.jl")

module Method
include("paladinsapi/methods.jl")
end

module Language
include("smiteapi/langcodes.jl")
end

module ResFormat
include("smiteapi/resformats.jl")
end

include("smiteapi/session.jl")
include("paladinsapi/session-reuse.jl")
include("smiteapi/base.jl")
include("smiteapi/credentials.jl")

include("smiteapi/createfunctions.jl")

"""
Returns a string (on success)
For example: SmiteAPI (ver 5.17.4996.0) [PATCH - 5.17] - Ping successful. Server Date:9/21/2018 6:16:32 PM
"""
ping = createfunction_base(Method.Ping)

test = createfunction(Method.TestSession)
serverstatus = createfunction(Method.ServerStatus)
quota = createfunction(Method.ApiQuota)
version = createfunction(Method.Version)
"""20 most recent MOTDs"""
motds = createfunction(Method.MOTDs)
"""Lists the 50 most watched / most recent recorded matches."""
topmatches = createfunction(Method.TopMatches)
spl = createfunction(Method.SPL)

getchampions = createfunction_lang(Method.Champions)
getgods = createfunction_lang(Method.GetGods)
getitems = createfunction_lang(Method.GetItems)
# Example of how functions may be fixed to a language
getgods_en = createfunction_fixed(Method.GetGods, Language.English)

getplayer = createfunction(Method.Player, (nameOrId) -> "/$nameOrId")
getplayer_byname = createfunction(Method.Player, (name::String) -> "/$name")
getplayer_byid = createfunction(Method.Player, (id::Int) -> "/$id")
getplayerloadouts = createfunction(Method.PlayerLoadouts, (id::Int) -> "/$id")
getplayerranks = createfunction(Method.ChampionRanks, (nameOrId) -> "/$nameOrId")
getmatchhistory = createfunction(Method.PlayerMatchHistory, (nameOrId) -> "/$nameOrId")
getfriends = createfunction(Method.PlayerFriends, (nameOrId) -> "/$nameOrId")
getgodranks = createfunction(Method.PlayerGodRanks, (nameOrId) -> "/$nameOrId")
getplayerxboxandswitch = createfunction(Method.PlayerXboxAndSwitch, (nameOrId) -> "/$nameOrId")

getqueuestats = createfunction(Method.PlayerQueueStats, (nameOrId, queueid) -> "/$nameOrId/$queueid")
getachievements = createfunction(Method.PlayerAchievements, (playerid) -> "/$playerid")

getmatch = createfunction(Method.Match, (matchid::Int) -> "/$matchid")
getmatches = createfunction(Method.Matches, (matchids::Array{Int}) -> "/$(join(matchids, ","))")
getmatch_live = createfunction(Method.MatchLive, (matchid::Int) -> "/$matchid")

getchampionskins = createfunction(Method.ChampionSkins, (championid::Int, lang::Language.LangType) -> "/$championid/$(lang.code)")
getchampionrecommendeditems = createfunction(Method.ChampionRecommendedItems, (championid::Int, lang::Language.LangType) -> "/$championid/$(lang.code)")
getgodskins = createfunction(Method.GodSkins, (godid::Int, lang::Language.LangType) -> "/$godid/$(lang.code)")

getclan = createfunction(Method.Clan, (clanid::Int) -> "/$clanid")
getclanmembers = createfunction(Method.ClanMembers, (clanid::Int) -> "/$clanid")
searchclan = createfunction(Method.ClanSearch, (searchstring::String) -> "/$searchstring")

getleagueleaderboard = createfunction(Method.LeagueLeaderboards, (queue::Int, tier, season) -> "/$queue/$tier/$season")
getleagueseasons = createfunction(Method.LeagueSeasons, (queue::Int) -> "/$queue")
getmatchidsbyqueue = createfunction(Method.MatchIDs, (queue::Int, date, hour) -> "/$queue/$date/$hour")

end
