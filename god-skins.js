
document.dataHandler = {
    SRC_GODS: 'data/gods.json',
    elGods: null,
    loadGodsDone: 0,
    loadSkinsDone: 0,
    init: function(){
        this.elGods = document.getElementById('gods')
        this.loadGods()
    },
    loadGods: function(){
        this.loadJson(this.SRC_GODS, this.onGodsLoaded.bind(this))
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
    onGodsLoaded: function(gods){
        for (var i = 0; i < gods.length; ++i){
            this.handleGod(gods[i])
        }
        this.loadGodsDone = gods.length
        this.loadDone()
    },
    handleGod: function(god){
        let KEY_CARD = 'godCard_URL'
        let KEY_ICON = 'godIcon_URL'
        let KEY_ID = 'id'
        let KEY_NAME = 'Name'

        let el = document.createElement('div')
        let godId = god[KEY_ID]
        el.id = 'god' + godId
        el.className = 'god'
        let nameHtml = '<h2 class="godname">' + god[KEY_NAME] + '</h2>'
        let countHtml = '<span class="godskincount"></span>'
        let iconHtml = '<img class="godicon" src="' + god[KEY_ICON] + '" alt="">'
        let skinsHtml = '<div id="skins' + godId + '" class="skins"></div>'
        el.innerHTML = nameHtml + countHtml + iconHtml + skinsHtml
        this.elGods.appendChild(el)
        this.loadSkins(godId)
    },
    loadSkins: function(godId){
        var src = 'data/godskins-' + godId + '.json'
        this.loadJson(src, this.handleSkins.bind(this))
    },
    handleSkins: function(skins){
        for (var i = 0; i < skins.length; ++i){
            let skin = skins[i]
            this.handleSkin(skin)
        }
        this.loadSkinsDone = this.loadSkinsDone + 1
        this.loadDone()
    },
    handleSkin: function(skin) {
        let KEY_GODID = 'god_id'
        let KEY_GOD_NAME = 'god_name'
        let KEY_NAME = 'skin_name'
        let KEY_CARD = 'godSkin_URL'
        // This is actually always the god icon
        let KEY_ICON = 'godIcon_URL'
        let KEY_OBTAINABILITY = 'obtainability'
        // 0 if not
        let KEY_PRICE_FAVOR = 'price_favor'
        // 0 if not
        let KEY_PRICE_GEMS = 'price_gems'
        // ??
        let KEY_RETMSG = 'ret_msg'
        let KEY_ID = 'skin_id1'
        let KEY_ID2 = 'skin_id2'

        let skinName = skin[KEY_NAME]
        if (skinName === 'Standard ' + skin[KEY_GOD_NAME]){
            // Shorten the standard skin name to just 'Standard' (dropping the god name)
            skinName = 'Standard'
        }

        let godId = skin[KEY_GODID]
        let godEl = document.getElementById('skins' + godId)
        let el = document.createElement('div')
        el.className = 'skin'
        let nameHtml = '<h2 class="skinname">' + skinName + '</h2>'
        let obtainHtml = '<div class="obtainability">' + skin[KEY_OBTAINABILITY] + '</div>'
        let cardHtml = skin[KEY_CARD].length > 0 ? '<a class="skincard" href="' + skin[KEY_CARD] + '"><img class="skincard" src="' + skin[KEY_CARD] + '" alt=""></a>' : '<div class="skincard skincard-missing">missing card art</div>'
        let costFavor = skin[KEY_PRICE_FAVOR] != 0 ? '<div class="price price-favor">' + skin[KEY_PRICE_FAVOR] + ' favor</div>' : ''
        let costGems = skin[KEY_PRICE_GEMS] != 0 ? '<div class="price price-gems">' + skin[KEY_PRICE_GEMS] + ' gems</div>' : ''
        el.innerHTML = nameHtml + obtainHtml + cardHtml + costFavor + costGems
        godEl.appendChild(el)
    },
    loadDone: function(){
        if (this.loadGodsDone > 0 && this.loadSkinsDone === this.loadGodsDone){
            document.filterHandler.init()
        }
    },
}
document.dataHandler.init()

document.filterHandler = {
    elGodname: null,
    godEls: [],
    skinEls: [],
    init: function(){
        if (this.elGodname !== null){
            console.log('ERROR: Called filterHandler init when it is arleady initialized')
            return
        }
        this.elGodname = document.getElementById('filter-godname')
        this.elSkinname = document.getElementById('filter-skinname')
        this.elGodname.addEventListener('input', this.onFilterGodnameChange.bind(this))
        this.elSkinname.addEventListener('input', this.onFilterSkinnameChange.bind(this))
        this.elGodname.disabled = false
        this.elSkinname.disabled = false

        // Construct metadata for faster data access (no need to query select dom)
        let gods = document.querySelectorAll('.god')
        let overallSkinCount = 0
        for (var i = 0; i < gods.length; ++i){
            let god = gods[i]
            let godname = god.querySelector('.godname').innerHTML.toLowerCase()
            let godMeta = {'name': godname, 'el': god, 'skinEls': [],}
            this.godEls.push(godMeta)
            let skins = god.querySelectorAll('.skin')
            for (var j = 0; j < skins.length; ++j){
                let skin = skins[j]
                let skinname = skin.querySelector('.skinname').innerHTML.toLowerCase()
                let skinMeta = {'name': skinname, 'el': skin, }
                godMeta.skinEls.push(skinMeta)
                ++overallSkinCount
            }
        }

        document.querySelector('#overall-skin-count').innerHTML = '(' + overallSkinCount + ')'

        this.onFilterUpdate()
    },
    onFilterGodnameChange: function(){
        this.onFilterUpdate()
    },
    onFilterSkinnameChange: function(){
        this.onFilterUpdate()
    },
    onFilterUpdate: function(){
        let searchGod = this.elGodname.value.toLowerCase()
        let searchSkin = this.elSkinname.value.toLowerCase()
        let gods = this.godEls
        for (var i = 0; i < gods.length; ++i){
            let godMeta = gods[i]
            let god = godMeta.el
            let godname = godMeta.name
            let isGodMatch = searchGod.length == 0 ? true : godname.includes(searchGod)
            
            let skins = godMeta.skinEls
            let skinCount = skins.length
            let visibleCount = 0
            for (var j = 0; j < skins.length; ++j){
                let skinMeta = skins[j]
                let skin = skinMeta.el
                let skinname = skinMeta.name
                let isSkinMatch = searchSkin.length == 0 ? true : skinname.includes(searchSkin)
                skin.className = 'skin ' + (isSkinMatch ? '' : 'hidden')
                if (isSkinMatch){
                    ++visibleCount
                }
            }
            let hasSkins = visibleCount > 0
            god.className = 'god ' + (isGodMatch && hasSkins ? '' : 'hidden')
            god.querySelector('.godskincount').innerHTML = '(' + visibleCount + '/' + skinCount + ')'
        }
    },
}
