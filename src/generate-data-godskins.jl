module pull

import CRC32c
import JSON
import DataStructures

include("common.jl")
include("smiteapi.jl")

s = SmiteApi.loadsession()
if s == nothing
    error("Failed to create session")
end

gods = JSON.parsefile("data/gods.json"; dicttype=DataStructures.OrderedDict)
godcount = length(gods)

@info "Starting to pull god skin data for $godcount gods…"

print(string("-"^godcount), "\r")
for god in gods
    godid::Int = god["id"]
    godskin = SmiteApi.getgodskins(s, godid, SmiteApi.Language.English)
    filepath = "data/godskins-$godid.json"
    writeifchanged(filepath, godskin)
    print("X")
end
println()

@info "Done generating god godskin files."

end
