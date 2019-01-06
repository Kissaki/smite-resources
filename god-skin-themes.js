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
        document.imageLoader.init()
        detailsExpandStateSaver.init()
    },
}

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

detailsExpandStateSaver = {
    init: function() {
        if (window.localStorage === undefined) {
            return
        }
        var els = document.querySelectorAll('details[id] summary')
        for (var i = 0; i < els.length; ++i) {
            var el = els[i]
            var details = el.parentElement
            if (!details.id) {
                continue
            }
            el.addEventListener('click', this.onClick)
            var isOpen = window.localStorage.getItem('details-expand-state.' + details.id) === 'true'
            details.open = isOpen
        }
    },
    onClick: function(ev) {
        var details = this.parentElement;
        if (!details.id) {
            return
        }
        window.localStorage.setItem('details-expand-state.' + details.id, !details.open)
    },
}

document.dataHandler.init()
