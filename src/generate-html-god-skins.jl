module gen

import JSON
# import DataStructures
import OrderedCollections
import Mustache

gods = JSON.parsefile("data/godswithskins.json")

# gods = JSON.parsefile("data/godswithskins.json"; dicttype=DataStructures.OrderedDict)
# gods = JSON.parsefile("data/godswithskins.json"; dicttype=OrderedCollections.OrderedDict)
# function custom_isless(a, b)
#     isless(a["Name"], ["Name"])
# end
# gods = sort!(gods; isless=isless)
# isless = a, b => isless(a["name"], b["name"])

function isless_skin(a::Dict{String,Any}, b::Dict{String,Any})
    name1 = a["skin_name"]
    name2 = b["skin_name"]
    if name1 == "Standard"
        return true
    end
    if name2 == "Standard"
        return false
    end
    if name1 == "Golden"
        return true
    end
    if name2 == "Golden"
        return false
    end
    if name1 == "Legendary"
        return true
    end
    if name2 == "Legendary"
        return false
    end
    if name1 == "Diamond"
        return true
    end
    if name2 == "Diamond"
        return false
    end
    obt1 = a["obtainability"]
    obt2 = b["obtainability"]
    if obt1 != obt2
        return isless_obt(obt1, obt2)
    end
    aFavor = a["price_favor"]
    bFavor = b["price_favor"]
    aGems = a["price_gems"]
    bGems = b["price_gems"]
    if aFavor != bFavor
        return isless_price(aFavor, bFavor)
    end
    if aGems != bGems
        return isless_price(aGems, bGems)
    end
    return isless(name1, name2)
end
function isless_obt(a, b)
    if a == "Normal"
        return true
    elseif a == "Exclusive"
        if b == "Normal"
            return false
        else
            return true
        end
    elseif a == "Limited"
        return false
    end
    println("ERROR: Unexpected obtainability $a")
    exit(1)
end
function isless_price(a, b)
    if a == 0
        return false
    end
    if b == 0
        return true
    end
    return a < b
end

for god in gods
    godskins::Array{Dict{String,Any}} = god["godskins"]
    # Data fixup
    for godskin in godskins
        if godskin["skin_name"] == "Standard " * god["Name"]
            godskin["skin_name"] = "Standard"
        end
    end

    # Data additions
    god["skincount"] = length(god["godskins"])

    # Order skins
    sort!(godskins, lt=isless_skin)
    god["godskins"] = godskins
    println("God skin count: $(god["Name"]) $(god["skincount"])")
end
println("godcount: ", length(gods))

const template_path = "src/templates/god-skins.mustache"

res = Mustache.render_from_file(template_path, gods)
if (res == nothing)
    print("Failed to parse template")
    exit()
end
open("god-skins.html", "w") do io
    write(io, res)
end

end
