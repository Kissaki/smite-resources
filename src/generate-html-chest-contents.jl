module gen

import JSON
import DataStructures
import Mustache

# chests = JSON.parsefile("data/chest-data.json"; dicttype=DataStructures.OrderedDict)
chests = JSON.parsefile("data/chest-data.json")
gods = JSON.parsefile("data/godswithskins.json")

godBySkinId = Dict()
godSkinById = Dict()
for god in gods
    for godskin in god["godskins"]
        skinId = godskin["skin_id1"]
        godSkinById[skinId] = godskin
        godBySkinId[skinId] = god
    end
end

skipped = 0
handled = 0
for chest in chests["chests"]
    for godskin in chest["godskins"]
        skinId = Base.get(godskin, "skin_id1", nothing)
        if skinId == nothing
            godskin["skinname"] = godskin["name"]
            global skipped
            skipped = skipped + 1
            continue
        end
        godData = godBySkinId[skinId]
        godskin["godicon"] = godData["godIcon_URL"]
        godskin["godname"] = godData["Name"]
        skinData = godSkinById[skinId]
        godskin["skinicon"] = skinData["godSkin_URL"]
        godskin["skinname"] = skinData["skin_name"]
        global handled
        handled = handled + 1
    end
end
println("skipped: $skipped, handled: $handled")

const template_path = "src/templates/chest-contents.mustache"

res = Mustache.render_from_file(template_path, chests)
if (res == nothing)
    print("Failed to parse template")
    exit()
end
open("chest-contents.html", "w") do io
    write(io, res)
end

end
