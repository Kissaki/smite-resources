module pull

import JSON

include("smiteapi.jl")

SmiteApi.ping()

s = SmiteApi.createsession(SmiteApi.Endpoint.PC)
if s == nothing
    error("Failed to create session")
end

# SmiteApi.test(s)

gods = SmiteApi.getgods(s)
f = open("data/gods.json", "w")
JSON.print(f, gods)
close(f)

end
