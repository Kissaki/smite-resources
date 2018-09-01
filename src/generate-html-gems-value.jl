module gen

import Mustache

mutable struct PackValue
    gems::Int
    eurocents::Int
    gemvalue_eurocents::Union{Float64, Nothing}
end

data = Dict()
data["sales"] = [
    Dict(
        "name"=>"Gem Sale - Bellona's Big Bash",
        "date_from"=>"2018-08-30",
        "date_to"=>"2018-09-09",
        "pack_values"=>[
            PackValue(200,399,nothing),
            PackValue(400,544,nothing),
            PackValue(800,959,nothing),
            PackValue(1500,1599,nothing),
            PackValue(2500,2099,nothing),
            PackValue(3500,2799,nothing),
            PackValue(8000,5199,nothing),
        ],
        "link"=>"https://www.smitegame.com/news/gem-sale-bellonas-big-bash",
        "screenshot_src"=>"img/gem-sale_2018-08-31.jpg",
    ),
]
for s in data["sales"]
    for v in s["pack_values"]
        # v["gemvalue_eurocents"] = ceil(v["eurocents"] / v["gems"])
        # v["gemvalue_eurocents"] = v["eurocents"] / v["gems"]
        # v["gemvalue_eurocents"] = round(v["eurocents"] / v["gems"]; digits=3)
        v.gemvalue_eurocents = v.eurocents / v.gems
    end
end

template_path = "src/templates/gems-value.mustache"
res = Mustache.render_from_file(template_path, data)
if (res == nothing)
    print("Failed to parse template")
    exit()
end
open("gems-value.html", "w") do io
    write(io, res)
end

end
