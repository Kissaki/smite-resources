module pull

import CRC32c
import JSON

include("common.jl")
include("smiteapi.jl")

s = SmiteApi.loadsession()
if s == nothing
    error("Failed to create session")
end

@info "Downloading items data…"
items = SmiteApi.getitems(s, SmiteApi.Language.English)
writeifchanged("data/items.json", items)

end
