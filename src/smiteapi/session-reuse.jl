import JSON
import Dates

"""
Loads a new session, either from a tempfile or by querying the API
Reuse old sesion if it data exists and is less than 15mins old
"""
function loadsession(endpoint::Endpoint.EndpointType=SmiteApi.Endpoint.PC, sessionfilename = ".tmp.session.json")
    session::Union{Nothing, Session} = nothing
    if isfile(sessionfilename) && Dates.unix2datetime(mtime(sessionfilename)) > Dates.now(Dates.UTC) - Dates.Minute(15)
        try
            data = JSON.parsefile(sessionfilename; dicttype=OrderedCollections.OrderedDict)
            session = Session(Endpoint.EndpointType(data["endpoint"]["Baseurl"]), data["devID"], data["authKey"], data["Id"])
            @info "Reusing session..."
        catch
            @info "Failed to read session from file"
        end
    end

    if session == nothing
        @info "Creating new session"
        session = createsession(endpoint)
        if session == nothing
            error("Failed to create session")
        else
            write(".tmp.session.json", session)
        end
    end

    session
end
