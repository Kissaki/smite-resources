module SmiteApi

import MD5
import Dates
import JSON

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

const response_format = ResFormat.Json

include("smiteapi/credentials.jl")
include("smiteapi/base.jl")

"""
Simple not authenticated method
<base>/<method><response_format>
"""
function createurl(method::Method.MethodType, endpoint::Endpoint.EndpointType)
    "$(endpoint.Baseurl)/$(method.name)$(response_format.name)"
end

"""
<base>/<method><response_format>/developerid/signature/timestamp
"""
function createurl(method::Method.MethodType, endpoint::Endpoint.EndpointType, devid::Int, authkey::String)
    baseurl = createurl(method, endpoint)
    timestamp = createtimestamp()
    signature = createsignature(method, devid, authkey, timestamp)
    baseurl * "/$(devid)/$(signature)/$timestamp"
end

"""
<base>/<method><response_format>/developerid/signature/session/timestamp
"""
function createurl(method::Method.MethodType, session::Session)
    baseurl = createurl(method, session.endpoint)
    timestamp = createtimestamp()
    signature = createsignature(method, session, timestamp)
    baseurl * "/$(session.devID)/$(signature)/$(session.Id)/$timestamp"
end

function createurl(method::Method.MethodType, session::Session, language::Language.LangType)
    createurl(method, session) * "/$(language.code)"
end

function createtimestamp()
    Dates.format(Dates.now(Dates.UTC), "yyyymmddHHMMSS")
end

function createsignature(method::Method.MethodType, developerid::Int, authkey::String, timestampUtc::String)
    MD5.bytes2hex(MD5.md5("$developerid$(method.name)$authkey$timestampUtc"))
end

function createsignature(method::Method.MethodType, session::Session, timestampUtc::String)
    createsignature(method, session.devID, session.authKey, timestampUtc)
end

function ping()
    method = Method.Ping
    url = createurl(method, Endpoint.PC)
    logsend(method, url)
    r = send(url)
    logrecv(method, r)
end

function createsession(endpoint::Endpoint.EndpointType)Union{Session, Nothing}
    cred = loadcredentials()
    method = Method.CreateSession
    url = createurl(method, endpoint, cred.devid, cred.authkey)
    logsend(method, url)
    r = send(url)
    logrecv(method, r)
    responsebody = String(r.body)
    resdata = JSON.parse(responsebody)
    logjson(resdata)
    Session(endpoint, cred.devid, cred.authkey, resdata["session_id"])
end

function test(session::Session)
    method = Method.TestSession
    url = createurl(method, session)
    logsend(method, url)
    r = send(url)
    logrecv(method, r)
    responsebody = String(r.body)
    resdata = JSON.parse(responsebody)
    logjson(resdata)
    resdata
end

function getgods(session::Session)
    method = Method.GetGods
    lang = Language.English
    url = createurl(method, session, lang)
    logsend(method, url)
    r = send(url)
    logrecv(method, r)
    body = String(r.body)
    resdata = JSON.parse(body)
    # logjson(resdata)
    resdata
end

end
