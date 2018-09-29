struct Session
	endpoint::Endpoint.EndpointType
	devID::Int
	authKey::String
	Id::String
end

function createsession(endpoint::Endpoint.EndpointType; verbose=0, logresjson=false)Union{Session, Nothing}
    cred = loadcredentials()
    method = Method.CreateSession
    url = createurl(method, endpoint, cred.devid, cred.authkey)
    resdata = send(url; verbose=verbose, logresjson=logresjson)
    Session(endpoint, cred.devid, cred.authkey, resdata["session_id"])
end
