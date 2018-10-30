module godswithskins

import JSON
import OrderedCollections

include("common.jl")

gods = JSON.parsefile("data/gods.json"; dicttype=OrderedCollections.OrderedDict)
godcount = length(gods)

@info "Starting to merge god skin data for $godcount gods…"

combined = []

print(string("-"^godcount), "\r")
for god in gods
    godid = god["id"]
    godskins = JSON.parsefile("data/godskins-$godid.json")
    god["godskins"] = godskins
    print("X")
end
println()

writeifchanged("data/godswithskins.json", gods)

@info "Merge done."

end
