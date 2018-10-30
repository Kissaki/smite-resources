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
newJson = JSON.json(gods)
crc = CRC32c.crc32c(newJson)
filename = "data/gods.json"
existing = open(CRC32c.crc32c, filename)
if crc != existing
    @info "Writing new gods data..."
    write(filename, gods)
else
    @info "Pulled god data does not differ from existing stored data."
end

end
