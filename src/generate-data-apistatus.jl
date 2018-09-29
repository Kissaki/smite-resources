module ApiStatus

import HTTP
import JSON
import OrderedCollections
import Mustache
import Dates

include("api-status/check-god-images.jl")
include("smiteapi.jl")

datafile = "data/apistatus.json"

function generate(datafile; checkgodimages=false, status=true)
    data = Dict()
    # If data already exists, load it (as we may to only want to extend it)
    if isfile(datafile)
        data = JSON.parsefile(datafile; dicttype=OrderedCollections.OrderedDict)
    end

    data["LastUpdate"] = Dates.now()

    if checkgodimages
        goddata = check_god_images()
        goddata["lastchecked"] = Dates.now()
        data["goddata"] = goddata
    end

    if status
        s = SmiteApi.createsession(SmiteApi.Endpoint.PC)
        if s == nothing
            error("Failed to create session")
        end

        res = SmiteApi.serverstatus(s)
        data["Status"] = res[1]["status"]
        data["Version"] = res[1]["version"]
    end

# @info "status", status[1]["entry_datetime"]
# @info "status", status[1]["status"]
# @info "status", status[1]["version"]

# patchinfo = SmiteApi.version(s)
# @info "version", patchinfo["version_string"]
# @info "ret", patchinfo["ret_msg"] == nothing


    write(datafile, data)
end

function write(datafile, data)
    f = open(datafile, "w")
    JSON.print(f, data)
    close(f)
end

generate(datafile)

end
