document.dataHandler = {
    elChests: null,
    gods: {},
    godskins: {},
    init: function(){
        this.elChests = document.querySelector('#chests')
        this.loadData()
    },
    loadData: function(){
        this.loadJson('data/godswithskins.json', this.onGodData.bind(this))
    },
    loadJson: function(url, onLoadHandler){
        var request = new XMLHttpRequest()
        request.open('GET', url)
        request.responseType = 'json'
        request.send()
        request.onload = this.onLoadResult(onLoadHandler)
    },
    onGodData: function(gods){
        let KEY_ID = 'id'
        let KEY_SKINS = 'godskins'
        let KEY_SKINID1 = 'skin_id1'
        for (var i = 0; i < gods.length; ++i){
            let god = gods[i]
            let godId = god[KEY_ID]
            this.gods[godId] = god

            let skins = god[KEY_SKINS]
            for (var j = 0; j < skins.length; ++j){
                let skin = skins[j]
                let skinId = skin[KEY_SKINID1]
                this.godskins[skinId] = {'god': god, 'godskin': skin,}
            }
        }
        this.loadJson('data/chest-data.json', this.onChestData.bind(this))
    },
    onLoadResult: function(dataHandler){
        return function(e){
            var req = e.target
            var res = req.response
            dataHandler(res)
        }
    },
    onChestData: function(data){
        let chests = data.chests
        let chestsHtml = ''
        for (var i = 0; i < chests.length; ++i){
            let chest = chests[i]
            let nameHtml = '<h2 class="chestname">' + chest['name'] + '</h2>'
            let priceHtml = '<div class="chestprice">Price Gems: ' + chest['priceGems'] + '</div>'
            let godskinsHtml = this.getGodskinsHtml(chest['godskins'])
            let voicepacksHtml = this.getVoicepacksHtml(chest['voicepacks'])
            let wardskinsHtml = this.getWardskinsHtml(chest['wardskins'])
            let contentHtml = '<label>Show content</label> <input type="checkbox" class="checkboxshowcontent"><div class="chestcontent">' + godskinsHtml + voicepacksHtml + wardskinsHtml + '</div>'
            let chestHtml = '<article class="chest">' + nameHtml  + priceHtml + contentHtml + '</article>'
            chestsHtml += chestHtml
        }
        this.elChests.innerHTML = chestsHtml
    },
    getGodskinsHtml: function(godskins){
        if (godskins === undefined){
            return ''
        }
        if (godskins.length == 0){
            return ''
        }
        let godskinsHtml = '<h3>God Skins:</h3>'
        for (var j = 0; j < godskins.length; ++j){
            let godskin = godskins[j]
            godskinsHtml += this.getGodskinHtml(godskin)
        }
        return '<section class="chestcontentcategory">' + godskinsHtml + '</section>'
    },
    getGodskinHtml: function(skin){
        let KEY_SKINID1 = 'skin_id1'
        let KEY_SKINNAME = 'name'
        let skinId = skin[KEY_SKINID1]
        if (skinId === undefined){
            return '<div class="godskin">' + skin[KEY_SKINNAME] + '</div>'
        }
        let data = this.godskins[skinId]
        let godData = data['god']
        let skinData = data['godskin']
        let iconGod = skinData['godIcon_URL']
        let iconSkin = skinData['godSkin_URL']
        let godName = godData.Name
        let skinName = skinData.skin_name
        let godHtml = '<div class="god"><img class="godicon" src="' + iconGod + '" alt=""><span class="godname">' + godName + '</span></div>'
        let skinHtml = '<img class="skinicon" src="' + iconSkin + '" alt=""><span class="skinname">' + skinName + '</span>'
        return '<div class="godskin">' + godHtml + skinHtml + '</div>'
    },
    getVoicepacksHtml: function(voicepacks){
        if (voicepacks === undefined){
            return ''
        }
        if (voicepacks.length == 0){
            return ''
        }
        let voicepacksHtml = '<h3>Voicepacks:</h3>'
        for (var j = 0; j < voicepacks.length; ++j){
            let voicepack = voicepacks[j]
            voicepacksHtml += this.getVoicepackHtml(voicepack)
        }
        return '<section class="chestcontentcategory">' + voicepacksHtml + '</section>'
    },
    getVoicepackHtml: function(vp){
        return '<div class="voicepack">' + vp + '</div>'
    },
    getWardskinsHtml: function(wardskins){
        if (wardskins === undefined){
            return ''
        }
        if (wardskins.length == 0){
            return ''
        }
        let wardskinsHtml = '<h3>Ward Skins</h3>'
        for (var j = 0; j < wardskins.length; ++j){
            let wardskin = wardskins[j]
            wardskinsHtml += this.getWardskinHtml(wardskin)
        }
        return '<section class="chestcontentcategory">' + wardskinsHtml + '</section>'
    },
    getWardskinHtml: function(skin){
        let icon = '<a href="https://smite.gamepedia.com/File:Ward_' + skin + '.png">icon</a>'
        let shot = '<a href="https://smite.gamepedia.com/File:WardShot_' + skin + '.png">icon</a>'
        return '<div class="wardskin">' + skin + icon + shot + '</div>'
    },
}
document.dataHandler.init()
