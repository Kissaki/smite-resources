struct EndpointType
    Baseurl::String
end
const PaladinsPC = EndpointType("http://api.paladins.com/paladinsapi.svc")
const PaladinsXbox = EndpointType("http://api.xbox.paladins.com/paladinsapi.svc")
const PaladinsPS4 = EndpointType("http://api.ps4.paladins.com/paladinsapi.svc")
