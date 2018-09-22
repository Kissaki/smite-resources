module pull

import JSON

include("common.jl")
include("smiteapi.jl")

SmiteApi.ping()

s = SmiteApi.loadsession()
if s == nothing
    error("Failed to create session")
end

gods = SmiteApi.getgods(s, SmiteApi.Language.English)
write("data/gods.json", gods)

end
