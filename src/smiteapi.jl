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
include("smiteapi/base.jl")
include("smiteapi/credentials.jl")

function createfunction_base(method::Method.MethodType)
    (endpoint::Endpoint.EndpointType=Endpoint.PC; verbose=0, logresjson=false) -> begin
        url = createurl(method, endpoint)
        send(url; verbose=verbose, logresjson=logresjson)
    end
end

function createfunction(method::Method.MethodType)
    (session::Session; verbose=0, logresjson=false) -> begin
        url = createurl(method, session)
        send(url; verbose=verbose, logresjson=logresjson)
    end
end

function createfunction(method::Method.MethodType, pathfn)
    (session::Session, params...; verbose=0, logresjson=false) -> begin
        url = createurl(method, session, pathfn(params...))
        send(url; verbose=verbose, logresjson=logresjson)
    end
end

function createfunction_fixed(method::Method.MethodType, createurl_args...)
    (session::Session; verbose=0, logresjson=false) -> begin
        url = createurl(method, session, createurl_args...)
        send(url; verbose=verbose, logresjson=logresjson)
    end
end

# function createfunction(method::Method.MethodType, args...)
#     (session::Session, args...; verbose=0, logresjson=false) -> begin
#         url = createurl(method, session, args...)
#         send(url)
#     end
# end

function createfunction_lang(method::Method.MethodType)
    # createfunction(method, (lang::Language.LangType) -> "/$(lang.code)")
    (session::Session, lang::Language.LangType; verbose=0, logresjson=false) -> begin
        url = createurl(method, session, lang)
        send(url; verbose=verbose, logresjson=logresjson)
    end
end

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

getgods_en = createfunction_fixed(Method.GetGods, Language.English)
getgods = createfunction_lang(Method.GetGods)
getitems = createfunction_lang(Method.GetItems)

getplayer = createfunction(Method.Player, (nameOrId) -> "/$nameOrId")
getplayer_name = createfunction(Method.Player, (name::String) -> "/$name")
getplayer_id = createfunction(Method.Player, (id::Int) -> "/$id")

end
