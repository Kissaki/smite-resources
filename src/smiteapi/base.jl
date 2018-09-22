import HTTP
import JSON

const response_format = ResFormat.Json

function write(datafile, data)
    f = open(datafile, "w")
    JSON.print(f, data)
    close(f)
end

function send_inner(url::String)
    try
        HTTP.request("GET", url)
    catch e
        error("Response status error: $(e.Response.Status)")
    end
end

function send(url::String; verbose=0, logresjson=false)
    if verbose > 0
        @info "Sending request..." url
    end
    r = send_inner(url)
    if verbose > 0
        @info "Received response" status=r.status bodylen=length(r.body)
    end
    responsebody = String(r.body)
    resdata = JSON.parse(responsebody; dicttype=OrderedCollections.OrderedDict)
    if verbose > 1 || logresjson
        @info "Parsed JSON" resdata
    end
    resdata
end

"""
Simple not authenticated method
Also base url for all other methods
<base>/<method><response_format>
"""
function createurl(method::Method.MethodType, endpoint::Endpoint.EndpointType)
    "$(endpoint.Baseurl)/$(method.name)$(response_format.name)"
end

"""
Use for creating sessions, otherwise session object should be passed to createurl
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

"""
For arbitrary subpaths
"""
function createurl(method::Method.MethodType, session::Session, path::String)
    createurl(method, session) * "/$(path)"
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
