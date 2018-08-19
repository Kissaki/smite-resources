document.dataHandler = {
    elChests: null,
    init: function(){
        this.elChests = document.querySelector('#chests')
        this.loadJson('data/chest-data.json', this.onChestData.bind(this))
    },
    loadJson: function(url, onLoadHandler){
        var request = new XMLHttpRequest()
        request.open('GET', url)
        request.responseType = 'json'
        request.send()
        request.onload = this.onLoadResult(onLoadHandler)
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
            let nameHtml = '<div class="chestname">' + chest['name'] + '</div>'
            let priceHtml = '<div class="chestprice">Price Gems: ' + chest['priceGems'] + '</div>'
            let godskinsHtml = this.getGodskinsHtml(chest['godskins'])
            let voicepacksHtml = this.getVoicepacksHtml(chest['voicepacks'])
            let wardskinsHtml = this.getWardskinsHtml(chest['wardskins'])
            let chestHtml = nameHtml + priceHtml + godskinsHtml + voicepacksHtml + wardskinsHtml
            chestsHtml += chestHtml
        }
        this.elChests.innerHTML = chestsHtml
    },
    getGodskinsHtml: function(godskins){
        if (godskins.length == 0){
            return ''
        }
        let godskinsHtml = '<b>God Skins:</b>'
        for (var j = 0; j < godskins.length; ++j){
            let godskin = godskins[j]
            godskinsHtml += this.getGodskinHtml(godskin)
        }
        return godskinsHtml
    },
    getGodskinHtml: function(skin){
        return '<div class="godskin">' + skin['name'] + '</div>'
    },
    getVoicepacksHtml: function(voicepacks){
        if (voicepacks.length == 0){
            return ''
        }
        let voicepacksHtml = '<b>Voicepacks:</b>'
        for (var j = 0; j < voicepacks.length; ++j){
            let voicepack = voicepacks[j]
            voicepacksHtml += this.getVoicepackHtml(voicepack)
        }
        return voicepacksHtml
    },
    getVoicepackHtml: function(vp){
        return '<div class="voicepack">' + vp + '</div>'
    },
    getWardskinsHtml: function(wardskins){
        if (wardskins.length == 0){
            return ''
        }
        let wardskinsHtml = '<b>Ward Skins</b>:'
        for (var j = 0; j < wardskins.length; ++j){
            let wardskin = wardskins[j]
            wardskinsHtml += this.getWardskinHtml(wardskin)
        }
        return wardskinsHtml
    },
    getWardskinHtml: function(skin){
        let icon = '<a href="https://smite.gamepedia.com/File:Ward_' + skin + '.png">icon</a>'
        let shot = '<a href="https://smite.gamepedia.com/File:WardShot_' + skin + '.png">icon</a>'
        return '<div class="wardskin">' + skin + icon + shot + '</div>'
    },
}
document.dataHandler.init()
