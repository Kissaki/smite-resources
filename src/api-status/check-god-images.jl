import JSON
import HTTP

function check_url_exists(url::String)
    @info "Checking url $url…"
    res = HTTP.request("HEAD", url; status_exception = false, retry = false, redirect = false, readtimeout = 1)
    res.status == 200
end

function check_god_images(datasrc="data/godswithskins.json")
    gods = JSON.parsefile(datasrc; dicttype=OrderedCollections.OrderedDict)

    godscount = 0
    godscardcount = 0
    godscardmissingcount = 0
    godsiconcount = 0
    godsiconmissingcount = 0
    godsskinscount = 0
    godsskinscardcount = 0
    godsskinscardmissingcount = 0
    godsskinscardsmissing = []
    godsskinscardsempty = []

    for god in gods
        godscount = godscount + 1

        url_ability1 = god["godAbility1_URL"]
        url_ability2 = god["godAbility2_URL"]
        url_ability3 = god["godAbility3_URL"]
        url_ability4 = god["godAbility4_URL"]
        url_ability5 = god["godAbility5_URL"]
        url_card = god["godCard_URL"]
        if length(url_card) > 0
            godscardcount = godscardcount + 1
            if check_url_exists(url_card) == false
                godscardmissingcount = godscardmissingcount + 1
            end
        end
        url_icon = god["godIcon_URL"]
        if length(url_icon) > 0
            godsiconcount = godsiconcount + 1
            if check_url_exists(url_icon) == false
                godsiconmissingcount = godsiconmissingcount + 1
            end
        end

        for skin in god["godskins"]
            godsskinscount = godsskinscount + 1

            # TODO: Check that god icon URL is the same (within this skin) than god
            # if skin["godIcon_URL"] != url_icon
            url_skin = skin["godSkin_URL"]

            if length(url_skin) > 0
                godsskinscardcount = godsskinscardcount + 1
                if check_url_exists(url_skin) == false
                    godsskinscardmissingcount = godsskinscardmissingcount + 1
                    push!(godsskinscardsmissing, url_skin)
                end
            else
                push!(godsskinscardsempty, Dict("godid"=>god["id"], "godname"=>god["Name"], "skinid"=>skin["skin_id1"], "skinname"=>skin["skin_name"]))
            end
        end
    end

    Dict(
        "godscount"=>godscount,
        "godscardcount"=>godscardcount,
        "godscardmissingcount"=>godscardmissingcount,
        "godsiconcount"=>godsiconcount,
        "godsiconmissingcount"=>godsiconmissingcount,
        "godsskinscount"=>godsskinscount,
        "godsskinscardcount"=>godsskinscardcount,
        "godsskinscardmissingcount"=>godsskinscardmissingcount,
        "godsskinscardsmissing"=>godsskinscardsmissing,
        "godsskinscardsempty"=>godsskinscardsempty
    )
end
