module gen

import JSON
import OrderedCollections
import Mustache

const template_path = "src/templates/god-skin-themes.mustache"
const outpath = "god-skin-themes.html"

@info "Reading gods with skin data and generating html file…"
gods = JSON.parsefile("data/godswithskins.json"; dicttype=OrderedCollections.OrderedDict)
skin_themes = JSON.parsefile("data/godskin-theme.json")

themes = Dict()
uncategorized = []

function add!(theme::String, godskin)
    global themes
    if !haskey(themes, theme)
        themes[theme] = []
    end
    push!(themes[theme], godskin)
end

for god in gods
    godskins::Array{Dict{String,Any}} = god["godskins"]
    # Data fixup
    for godskin in godskins
        if godskin["skin_name"] == "Standard " * god["Name"]
            godskin["skin_name"] = "Standard"
        end

        # Add theme
        skinid = godskin["skin_id1"]
        if haskey(skin_themes, "$skinid")
            theme = skin_themes["$skinid"]
            add!(theme, godskin)
        else
            if godskin["skin_name"] == "Standard"
                add!("standard", godskin)
                continue
            elseif godskin["skin_name"] == "Golden"
                add!("golden", godskin)
                continue
            elseif godskin["skin_name"] == "Legendary"
                add!("legendary", godskin)
                continue
            elseif godskin["skin_name"] == "Diamond"
                add!("diamond", godskin)
                continue
            elseif godskin["skin_name"] == "Shadow"
                add!("shadow", godskin)
                continue
            end
            push!(uncategorized, godskin)
        end
    end

    # Data additions
    god["skincount"] = length(god["godskins"])
end

themearray = []
for pair in themes
    obj = Dict(
        "name"=>pair.first,
        "skins"=>pair.second
    )
    push!(themearray, obj)
end

function isless_theme(a::Dict{String, Any}, b::Dict{String, Any})
    name1 = a["name"]
    name2 = b["name"]
    isless(name1, name2)
end

sort!(themearray; lt=isless_theme)

data = Dict(
    "gods" => gods,
    "themes" => themearray,
    "uncategorized" => uncategorized,
)

res = Mustache.render_from_file(template_path, data)
if (res == nothing)
    print("Failed to parse template")
    exit()
end
open(outpath, "w") do io
    write(io, res)
end
@info "Done."

end
