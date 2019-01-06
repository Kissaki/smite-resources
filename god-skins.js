// Fix for IE11
if (typeof NodeList.prototype.forEach !== "function" ) {
    NodeList.prototype.forEach = Array.prototype.forEach;
}
// Fix for IE
// From https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/String/includes#Polyfill
if (!String.prototype.includes) {
    Object.defineProperty(String.prototype, 'includes', {
        value: function(search, start) {
            if (typeof start !== 'number') {
                start = 0
            }

            if (start + search.length > this.length) {
                return false
            } else {
                return this.indexOf(search, start) !== -1
            }
        }
    })
}

document.dataHandler = {
    init: function(){
        document.filterHandler.init()
        document.imageLoader.init()
    },
}

document.filterHandler = {
    elGodname: null,
    elSkinname: null,
    elStandard: null,
    elMastery: null,
    godEls: [],
    skinEls: [],
    init: function(){
        if (this.elGodname !== null){
            console.log('ERROR: Called filterHandler init when it is arleady initialized')
            return
        }
        this.elGodname = document.getElementById('filter-godname')
        this.elSkinname = document.getElementById('filter-skinname')
        this.elStandard = document.getElementById('show-standard')
        this.elMastery = document.getElementById('show-mastery')
        this.elGodname.addEventListener('input', this.onFilterUpdate.bind(this))
        this.elSkinname.addEventListener('input', this.onFilterUpdate.bind(this))
        this.elStandard.addEventListener('input', this.onFilterUpdate.bind(this))
        this.elMastery.addEventListener('input', this.onFilterUpdate.bind(this))
        document.getElementById('filter-godname-clear').addEventListener('click', this.onFilterGodnameClear.bind(this))
        document.getElementById('filter-skinname-clear').addEventListener('click', this.onFilterSkinnameClear.bind(this))
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
                if (skinname == 'Standard ' + godname){
                    // Shorten the standard skin name to just 'Standard' (dropping the god name)
                    skinname = 'Standard'
                }
                let skinMeta = {'name': skinname, 'el': skin, }
                godMeta.skinEls.push(skinMeta)
                ++overallSkinCount
            }
        }

        document.querySelector('#overall-skin-count').innerHTML = '(' + overallSkinCount + ')'

        this.onFilterUpdate()
    },
    onFilterGodnameClear: function(){
        this.elGodname.value = ''
        this.onFilterUpdate()
    },
    onFilterSkinnameClear: function(){
        this.elSkinname.value = ''
        this.onFilterUpdate()
    },
    onFilterGodUpdate: function(){
        let searchGod = this.elGodname.value.toLowerCase()
        let gods = this.godEls
        for (var i = 0; i < gods.length; ++i){
            let godMeta = gods[i]
            let god = godMeta.el
            let godname = godMeta.name
            let isGodMatch = searchGod.length == 0 ? true : godname.includes(searchGod)
            
            god.className = 'god ' + (isGodMatch ? '' : 'hidden')
        }
    },
    onFilterUpdate: function(){
        let searchGod = this.elGodname.value.toLowerCase()
        let searchSkin = this.elSkinname.value.toLowerCase()
        let showStandard = this.elStandard.checked
        let showMastery = this.elMastery.checked
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
                if (isSkinMatch && skinname == 'standard' && !showStandard){
                    isSkinMatch = false
                }
                if (isSkinMatch && !showMastery && (skinname == 'golden' || skinname == 'legendary' || skinname == 'diamond')){
                    isSkinMatch = false
                }
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

document.toggleAll = {
    init: function(){
        document.getElementById('btn-openall').addEventListener('click', this.open.bind(this))
        document.getElementById('btn-closeall').addEventListener('click', this.close.bind(this))
    },
    set: function(isOpen, qualifier) {
        var gods = document.querySelectorAll('.god' + qualifier)
        for (var i = 0; i < gods.length; ++i) {
            var god = gods[i]
            god.open = isOpen
        }
    },
    open: function() {
        this.set(true, ':not(.hidden)')
    },
    close: function() {
        this.set(false, '')
    },
}
document.toggleAll.init()

document.imageLoader = {
    observer: null,
    init: function(){
        var images = document.querySelectorAll('img.skincard')
        if ('IntersectionObserver' in window){
            observer = new IntersectionObserver(this.onItem.bind(this));
            images.forEach(this.observe);
        } else {
            images.forEach(this.loadImage);
        }
    },
    observe: function(img){
        this.observer.observe(img)
    },
    onItem: function(items, observer) {
        items.forEach(this.onItemIndiv.bind(this));
    },
    onItemIndiv: function(item) {
        if (item.isIntersecting){
            let img = item.target
            this.loadImage(img);
            observer.unobserve(img);
        }
    },
    loadImage: function(img){
        img.src = img.dataset.src
    },
}

document.dataHandler.init()
