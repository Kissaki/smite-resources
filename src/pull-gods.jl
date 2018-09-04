module pull

include("smiteapi.jl")

SmiteApi.ping()
s = SmiteApi.createsession(SmiteApi.Endpoint.PC)
if s == nothing
    error("Failed to create session")
end
SmiteApi.test(s)

end
