module gen

import JSON
import OrderedCollections
import Mustache

const inpath = "data/items-blocks.json"
const template_path = "src/templates/items.mustache"
const outpath = "smiteitems.html"

data = JSON.parsefile(inpath; dicttype=OrderedCollections.OrderedDict)

@info "Generating…"

res = Mustache.render_from_file(template_path, data)
if (res == nothing)
    print("Failed to parse template")
    exit()
end
open(outpath, "w") do io
    write(io, res)
end

@info "Done generating items."

end
