module pull

import CRC32c
import JSON

include("common.jl")
include("smiteapi.jl")

s = SmiteApi.loadsession()
if s == nothing
    error("Failed to create session")
end

gods = SmiteApi.getgods(s, SmiteApi.Language.English)
writeifchanged("data/gods.json", gods)

end
