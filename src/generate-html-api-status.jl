module gen

import HTTP
import JSON
import OrderedCollections
import Mustache
import Dates

data = JSON.parsefile("data/apistatus.json")

const template_path = "src/templates/api-status.mustache"
const out_path = "api-status.html"

res = Mustache.render_from_file(template_path, data)
if (res == nothing)
    print("Failed to parse template")
    exit()
end
open(out_path, "w") do io
    write(io, res)
end

end
