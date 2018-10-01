module Endpoint
struct EndpointType
    Baseurl::String
end
const PC = EndpointType("http://api.paladins.com/paladinsapi.svc")
const Xbox = EndpointType("http://api.xbox.paladins.com/paladinsapi.svc")
const PS4 = EndpointType("http://api.ps4.paladins.com/paladinsapi.svc")
end
