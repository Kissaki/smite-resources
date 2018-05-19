function onFilterChange(e)
{
    var term = this.value.toLowerCase()
    var items = document.querySelectorAll('.item')
    for (i in items)
    {
        var item = items[i]
        var displayValue = item.querySelector('.name').innerHTML.toLowerCase().indexOf(term) != -1 ? 'block' : 'none'
        item.parentNode.style.display = displayValue
    }
}
var filter = document.getElementById('item-filter')
filter.addEventListener('input', onFilterChange)
filter.focus()
