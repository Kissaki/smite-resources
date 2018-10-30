module godswithskins

import JSON
import OrderedCollections

include("common.jl")

const inpath = "data/items.json"
const outpath = "data/items-blocks.json"

function isstarter(item)
    return item["StartingItem"]
end

function isconsumable(item)
    return item["Type"] == "Consumable"
end

function isnormalitem(item)
    return item["Type"] == "Item"
end

function isactive(item)
    return item["Type"] == "Active"
end

function generate(inpath, outpath)
    items = JSON.parsefile("data/items.json"; dicttype=OrderedCollections.OrderedDict)

    itembyid = Dict{Int, Any}()

    @info "Indexing $(length(items)) items…"
    for item in items
        id = item["ItemId"]
        itembyid[id] = item
    end

    @info "Identifying hierarchy…"

    rootitems = Dict{Int, Any}()

    for item in items
        id = item["ItemId"]
        parentid = item["ChildItemId"]
        if parentid == 0
            rootitems[id] = item
        else
            parent = itembyid[parentid]
            if !haskey(parent, "childitems")
                parent["childitems"] = []
            end
            push!(parent["childitems"], item)
            parent["childitemscount"] = length(parent["childitems"])
        end
    end

    @info "Identifying starter and consumable items…"

    starteritems = []
    consumables = []
    normalitems = []
    actives = []
    for (id, item) in rootitems
        if isstarter(item)
            push!(starteritems, item)
        elseif isconsumable(item)
            push!(consumables, item)
        elseif isactive(item)
            push!(actives, item)
        elseif isnormalitem(item)
            push!(normalitems, item)
        else
            @warn "Ignoring unexpected item type $(item["Type"])"
        end
    end

    @info "Constructing data…"

    data = Dict(
        "normalitems" => normalitems,
        "starters" => starteritems,
        "consumables" => consumables,
        "actives" => actives,
    )

    @info "Writing data if changed…"

    writeifchanged(outpath, data)

    @info "Merge done."
end

generate(inpath, outpath)

end
