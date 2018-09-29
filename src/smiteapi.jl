module SmiteApi

import MD5
import Dates
import JSON
import OrderedCollections

module Endpoint
include("smiteapi/endpoints.jl")
end

module Method
include("smiteapi/methods.jl")
end

module Language
include("smiteapi/langcodes.jl")
end

module ResFormat
include("smiteapi/resformats.jl")
end

include("smiteapi/session.jl")
include("smiteapi/session-reuse.jl")
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

getgods = createfunction_lang(Method.GetGods)
getitems = createfunction_lang(Method.GetItems)
# Example of how functions may be fixed to a language
getgods_en = createfunction_fixed(Method.GetGods, Language.English)

function createfunctions_playerorid(method::SmiteApi.Method.MethodType)
    return (
        createfunction(method, (nameOrId) -> "/$nameOrId"),
        createfunction(method, (name::String) -> "/$name"),
        createfunction(method, (id::Int) -> "/$id"),
    )
end

(getplayer, getplayer_byname, getplayer_byid) = createfunctions_playerorid(Method.Player)
# getplayer = createfunction(Method.Player, (nameOrId) -> "/$nameOrId")
# getplayer_byname = createfunction(Method.Player, (name::String) -> "/$name")
# getplayer_byid = createfunction(Method.Player, (id::Int) -> "/$id")
getmatchhistory = createfunction(Method.PlayerMatchHistory, (nameOrId) -> "/$nameOrId")
getfriends = createfunction(Method.PlayerFriends, (nameOrId) -> "/$nameOrId")
getgodranks = createfunction(Method.PlayerGodRanks, (nameOrId) -> "/$nameOrId")

getqueuestats = createfunction(Method.PlayerQueueStats, (nameOrId, queueid) -> "/$nameOrId/$queueid")
getachievements = createfunction(Method.PlayerAchievements, (playerid) -> "/$playerid")

getmatch = createfunction(Method.Match, (matchid::Int) -> "/$matchid")
getmatches = createfunction(Method.Matches, (matchids::Array{Int}) -> "/$(join(matchids, ","))")
getmatch_live = createfunction(Method.MatchLive, (matchid::Int) -> "/$matchid")

getgodskins = createfunction(Method.GodSkins, (godid::Int, lang::Language.LangType) -> "/$godid/$(lang.code)")
getgodrecommendeditems = createfunction(Method.GodRecommendedItems, (godid::Int, lang::Language.LangType) -> "/$godid/$(lang.code)")

getclan = createfunction(Method.Clan, (clanid::Int) -> "/$clanid")
getclanmembers = createfunction(Method.ClanMembers, (clanid::Int) -> "/$clanid")
searchclan = createfunction(Method.ClanSearch, (searchstring::String) -> "/$searchstring")

"""
Current season
"""
getgodleaderboard = createfunction(Method.GodLeaderboard, (godid::Int, queue::Int) -> "/$godid/$queue")
getleagueleaderboard = createfunction(Method.LeagueLeaderboards, (queue::Int, tier, season) -> "/$queue/$tier/$season")
getleagueseasons = createfunction(Method.LeagueSeasons, (queue::Int) -> "/$queue")
getmatchidsbyqueue = createfunction(Method.MatchIDs, (queue::Int, date, hour) -> "/$queue/$date/$hour")

end
