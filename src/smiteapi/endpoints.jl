struct EndpointType
    Baseurl::String
end
const PC = EndpointType("http://api.smitegame.com/smiteapi.svc")
const Xbox = EndpointType("http://api.xbox.smitegame.com/smiteapi.svc")
const PS4  = EndpointType("http://api.ps4.smitegame.com/smiteapi.svc")
