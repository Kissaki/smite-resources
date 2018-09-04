module SmiteApi

import HTTP
import MD5
import YAML
import Dates
import JSON

module Endpoint
    struct EndpointType
        Baseurl::String
    end
    const PC = EndpointType("http://api.smitegame.com/smiteapi.svc")
    const Xbox = EndpointType("http://api.xbox.smitegame.com/smiteapi.svc")
    const PS4  = EndpointType("http://api.ps4.smitegame.com/smiteapi.svc")
end

module Method
    struct MethodType
        name::String
    end
    const Ping = MethodType("ping")
    const CreateSession = MethodType("createsession")
    const TestSession = MethodType("testsession")
    const ServerStatus = MethodType("gethirezserverstatus")
    const ApiQuota = MethodType("getdataused")
end

struct Session
	endpoint::Endpoint.EndpointType
	devID::Int
	authKey::String
	Id::String
end

const response_format = "json"

"""
Simple not authenticated method
<base>/<method><response_format>
"""
function createurl(method::Method.MethodType, endpoint::Endpoint.EndpointType)
    "$(endpoint.Baseurl)/$(method.name)$response_format"
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

function createtimestamp()
    Dates.format(Dates.now(Dates.UTC), "yyyymmddHHMMSS")
end

function createsignature(method::Method.MethodType, developerid::Int, authkey::String, timestampUtc::String)
    MD5.bytes2hex(MD5.md5("$developerid$(method.name)$authkey$timestampUtc"))
end

function createsignature(method::Method.MethodType, session::Session, timestampUtc::String)
    createsignature(method, session.devID, session.authKey, timestampUtc)
end

function send(url::String)
    try
    HTTP.request("GET", url)
    catch
        error("Response status error")
    end
end

function ping()
    method = Method.Ping
    url = createurl(method, Endpoint.PC)
    logsend(method, url)
    r = send(url)
    logrecv(method, r)
end

const authfile = ".auth.yaml"
data = YAML.load(open(authfile))
function createsession(endpoint::Endpoint.EndpointType)Union{Session, Nothing}
    developerid = data["devid"]
    authkey = data["authkey"]
    method = Method.CreateSession
    url = createurl(method, endpoint, developerid, authkey)
    logsend(method, url)
    r = send(url)
    logrecv(method, r)
    responsebody = String(r.body)
    resdata = JSON.parse(responsebody)
    logjson(resdata)
    Session(endpoint, developerid, authkey, resdata["session_id"])
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

function logsend(method::Method.MethodType, url)
    @info "Sending $(method.name) request..." url
end

function logrecv(method::Method.MethodType, res)
    @info "Received $(method.name) response" res.status bodylen=length(res.body)
end
function logjson(resdata)
    @info "Parsed JSON" resdata
end

end
