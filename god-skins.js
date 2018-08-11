
document.dataHandler = {
    SRC_GODS: 'data/gods.json',
    elGods: null,
    loadGodsDone: false,
    loadSkinsDone: false,
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
        this.loadGodsDone = true
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
        el.innerHTML = '<h2 class="godname">' + god[KEY_NAME] + '</h2>' + '<img class="godicon" src="' + god[KEY_ICON] + '" alt=""><div id="skins' + godId + '" class="skins"></div>'
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
        this.loadSkinsDone = true
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

        let godId = skin[KEY_GODID]
        let godEl = document.getElementById('skins' + godId)
        let el = document.createElement('div')
        el.className = 'skin'
        let nameHtml = '<h2 class="skinname">' + skin[KEY_NAME] + '</h2>'
        let obtainHtml = '<div class="obtainability">' + skin[KEY_OBTAINABILITY] + '</div>'
        let cardHtml = skin[KEY_CARD].length > 0 ? '<a class="skincard" href="' + skin[KEY_CARD] + '"><img class="skincard" src="' + skin[KEY_CARD] + '" alt=""></a>' : '<div class="skincard skincard-missing">missing card art</div>'
        let costFavor = skin[KEY_PRICE_FAVOR] != 0 ? '<div class="price price-favor">' + skin[KEY_PRICE_FAVOR] + ' favor</div>' : ''
        let costGems = skin[KEY_PRICE_GEMS] != 0 ? '<div class="price price-gems">' + skin[KEY_PRICE_GEMS] + ' gems</div>' : ''
        el.innerHTML = nameHtml + obtainHtml + cardHtml + costFavor + costGems
        godEl.appendChild(el)
    },
    loadDone: function(){
        if (this.loadGodsDone && this.loadSkinsDone){
            document.filterHandler.init()
        }
    },
}
document.dataHandler.init()

document.filterHandler = {
    elGodname: null,
    elSkinname: null,
    init: function(){
        this.elGodname = document.getElementById('filter-godname')
        this.elSkinname = document.getElementById('filter-skinname')
        this.elGodname.addEventListener('input', this.onFilterGodnameChange.bind(this))
        this.elSkinname.addEventListener('input', this.onFilterSkinnameChange.bind(this))
        this.elGodname.disabled = false
        this.elSkinname.disabled = false
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
        let gods = document.querySelectorAll('.god')
        for (var i = 0; i < gods.length; ++i){
            let god = gods[i]
            let godname = god.querySelector('.godname').innerHTML
            let isGodMatch = searchGod.length == 0 ? true : godname.toLowerCase().includes(searchGod)
            
            let skins = god.querySelectorAll('.skin')
            for (var j = 0; j < skins.length; ++j){
                let skin = skins[j]
                let skinname = skin.querySelector('.skinname').innerHTML
                let isSkinMatch = searchSkin.length == 0 ? true : skinname.toLowerCase().includes(searchSkin)
                skin.className = 'skin ' + (isSkinMatch ? '' : 'hidden')
            }
            let hasSkins = god.querySelectorAll('.skin:not(.hidden)').length > 0
            god.className = 'god ' + (isGodMatch && hasSkins ? '' : 'hidden')
        }
    },
}
