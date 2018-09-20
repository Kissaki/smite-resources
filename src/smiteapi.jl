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

struct Session
	endpoint::Endpoint.EndpointType
	devID::Int
	authKey::String
	Id::String
end

include("smiteapi/credentials.jl")
include("smiteapi/base.jl")

function createsession(endpoint::Endpoint.EndpointType; verbose=0, logresjson=true)Union{Session, Nothing}
    cred = loadcredentials()
    method = Method.CreateSession
    url = createurl(method, endpoint, cred.devid, cred.authkey)
    resdata = send(url)
    Session(endpoint, cred.devid, cred.authkey, resdata["session_id"])
end

function createfunction_base(method::Method.MethodType)
    (endpoint::Endpoint.EndpointType; verbose=0, logresjson=false) -> begin
        url = createurl(method, Endpoint.PC)
        send(url; verbose=verbose, logresjson=logresjson)
    end
end

function createfunction(method::Method.MethodType, createurl_args...; verbose=0, logresjson=true)
    (session::Session) -> begin
        url = createurl(method, session, createurl_args...)
        send(url)
    end
end

function createfunction_lang(method::Method.MethodType; logresjson=true)
    (session::Session, lang::Language.LangType) -> begin
        url = createurl(method, session, lang)
        send(url)
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

getgods_en = createfunction(Method.GetGods, Language.English; logresjson=false)
getgods = createfunction_lang(Method.GetGods; logresjson=false)
getitems = createfunction_lang(Method.GetItems; logresjson=false)

end
