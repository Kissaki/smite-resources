module PaladinsApiExec

import JSON
import OrderedCollections

include("common.jl")
include("paladinsapi.jl")

# PaladinsApi.ping(PaladinsApi.Endpoint.PC)

session = PaladinsApi.loadsession()

# PaladinsApi.ping(PaladinsApi.Endpoint.PC)
# @info PaladinsApi.test(session)

# status = PaladinsApi.serverstatus(session)
# @info "status", status[1]["entry_datetime"]
# @info "status", status[1]["status"]
# @info "status", status[1]["version"]

# patchinfo = PaladinsApi.version(session)
# @info "version", patchinfo["version_string"]
# @info "ret", patchinfo["ret_msg"] == nothing

# quota = PaladinsApi.quota(session)
# @info "quota", quota[1]
#OrderedCollections.OrderedDict{String,Any}("Active_Sessions"=>5,"Concurrent_Sessions"=>50,"Request_Limit_Daily"=>15000,"Session_Cap"=>500,"Session_Time_Limit"=>15,"Total_Requests_Today"=>20,"Total_Sessions_Today"=>10,"ret_msg"=>nothing)

res = PaladinsApi.getchampions(session, PaladinsApi.Language.English)
write("data/paladins/champions.json", res)

# res = PaladinsApi.getplayer(session, "Kissaki0")
# write("data/players/kissaki0.json", res)
# playerid = res[1]["Id"]
# @info playerid

# res = PaladinsApi.getplayer_name(session, "Kissaki0")
# res2 = PaladinsApi.getplayer_id(session, 505271)
# @info "playerByNameAndIdEqual" (res == res2)

# Kissaki0 playerid
# write("data/players/player-505271.json", PaladinsApi.getplayer(session, 505271))
# write("data/players/player-505271-achievements.json", PaladinsApi.getachievements(session, 505271))
# write("data/players/player-505271-godranks.json", PaladinsApi.getgodranks(session, 505271))
# write("data/players/player-505271-friends.json", PaladinsApi.getfriends(session, 505271))
# write("data/players/player-505271-matches.json", PaladinsApi.getmatchhistory(session, 505271))

# write("data/players/player-1230098.json", PaladinsApi.getplayer(session, 1230098))
# write("data/players/player-8340565.json", PaladinsApi.getplayer(session, 8340565))
# write("data/players/player-9857857.json", PaladinsApi.getplayer(session, 9857857))

# write("data/players/player-505271-match-419532815.json", PaladinsApi.getmatch(session, 419532815))
# write("data/players/player-505271-matches.json", PaladinsApi.getmatches(session, [419532815, 419529754];verbose=2))

end
