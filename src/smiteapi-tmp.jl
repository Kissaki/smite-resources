module pull

import JSON
import OrderedCollections

include("common.jl")
include("smiteapi.jl")

# SmiteApi.ping(SmiteApi.Endpoint.PC)

session = SmiteApi.loadsession()

# SmiteApi.ping(SmiteApi.Endpoint.PC)
# @info SmiteApi.test(session)

# status = SmiteApi.serverstatus(session)
# @info "status", status[1]["entry_datetime"]
# @info "status", status[1]["status"]
# @info "status", status[1]["version"]

# patchinfo = SmiteApi.version(session)
# @info "version", patchinfo["version_string"]
# @info "ret", patchinfo["ret_msg"] == nothing

# quota = SmiteApi.quota(session)
# @info "quota", quota[1]
#OrderedCollections.OrderedDict{String,Any}("Active_Sessions"=>5,"Concurrent_Sessions"=>50,"Request_Limit_Daily"=>15000,"Session_Cap"=>500,"Session_Time_Limit"=>15,"Total_Requests_Today"=>20,"Total_Sessions_Today"=>10,"ret_msg"=>nothing)

# res = SmiteApi.getplayer(session, "Kissaki0")
# write("data/players/kissaki0.json", res)
# playerid = res[1]["Id"]
# @info playerid

# res = SmiteApi.getplayer_name(session, "Kissaki0")
# res2 = SmiteApi.getplayer_id(session, 505271)
# @info "playerByNameAndIdEqual" (res == res2)

# Kissaki0 playerid
# write("data/players/player-505271.json", SmiteApi.getplayer(session, 505271))
# write("data/players/player-505271-achievements.json", SmiteApi.getachievements(session, 505271))
# write("data/players/player-505271-godranks.json", SmiteApi.getgodranks(session, 505271))
# write("data/players/player-505271-friends.json", SmiteApi.getfriends(session, 505271))

# write("data/players/player-1230098.json", SmiteApi.getplayer(session, 1230098))
# write("data/players/player-8340565.json", SmiteApi.getplayer(session, 8340565))
# write("data/players/player-9857857.json", SmiteApi.getplayer(session, 9857857))

end
