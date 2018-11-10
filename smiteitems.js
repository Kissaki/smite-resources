index = {
    rootItems: [],
    allItems: {},
}
function buildIndex()
{
    var items = document.querySelectorAll('.item-root')
    for (var i = 0; i < items.length; ++i)
    {
        var item = items[i]
        var meta = createMeta(item)
        index.rootItems.push(meta)
        index.allItems[item.dataset.id] = [
            meta,
        ]

        var childs = item.querySelectorAll('.item')
        for(var j = 0; j < childs.length; ++j)
        {
            var child = childs[j]
            index.allItems[item.dataset.id].push(createMeta(child))
        }
    }
    console.log('Built item index of ' + index.rootItems.length + ' root items')
}
function createMeta(item)
{
    return {
        'item': item,
        'name': item.querySelector('.name').innerText.toLowerCase(),
    }
}
function treehasitem(item, searchterm)
{
    var childs = index.allItems[item.item.dataset.id]
    for (var j = 0; j < childs.length; ++j)
    {
        var meta = childs[j]
        var isMatch = meta.name.indexOf(searchterm) != -1
        if (isMatch)
        {
            return true
        }
    }
    return false
}
function onFilterChange(e)
{
    var searchterm = this.value.toLowerCase()
    var items = index.rootItems
    for (var i = 0; i < items.length; ++i)
    {
        var meta = items[i]
        var displayValue = treehasitem(meta, searchterm) ? 'grid' : 'none'
        meta.item.style.display = displayValue
    }
}
buildIndex()
var filter = document.getElementById('item-filter')
filter.addEventListener('input', onFilterChange)
filter.focus()
