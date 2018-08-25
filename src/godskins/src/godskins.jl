module godskins

import JSON

gods = JSON.parsefile("data/gods.json")
godcount = length(gods)

println("Count of gods: $godcount")

combined = []

for god in gods
    godname = god["Name"]
    godid = god["id"]
    println("Handling god $godname (ID $godid)...")
    godskins = JSON.parsefile("data/godskins-$godid.json")
    god["godskins"] = godskins
end

println("Completed handling $godcount gods")
println("Writing combined data…")

f = open("data/godswithskins.json", "w")
JSON.print(f, gods)
close(f)

end
