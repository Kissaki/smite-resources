module gen

import JSON
import OrderedCollections
import Mustache

const template_path = "src/templates/items.mustache"
const outpath = "smiteitems.html"

items = JSON.parsefile("data/items.json"; dicttype=OrderedCollections.OrderedDict)
res = Mustache.render_from_file(template_path, items)
if (res == nothing)
    print("Failed to parse template")
    exit()
end
open(outpath, "w") do io
    write(io, res)
end

end
