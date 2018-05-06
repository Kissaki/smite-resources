var KEY_ASPL = 'AttackSpeedPerLevel'
var KEY_AS = 'AttackSpeed'
var KEY_HP = 'Health'
var KEY_HPPL = 'HealthPerLevel'
var KEY_HP5 = 'HealthPerFive'
var KEY_HP5PL = 'HP5PerLevel'
var KEY_NAME = 'Name'
var KEY_PANTHEON = 'Pantheon'
var KEY_ROLES = 'Roles'
var KEY_TYPE = 'Type'
var KEY_MP = 'Mana'
var KEY_MPPL = 'ManaPerLevel'
var KEY_MP5 = 'ManaPerFive'
var KEY_MP5PL = 'MP5PerLevel'
var KEY_SPEED = 'Speed'

var DATA_SRC = 'data/gods.json'
var request = new XMLHttpRequest();
request.open('GET', DATA_SRC);
request.responseType = 'json';
request.send();
request.onload = function(e) {
  var req = e.target
  var res = req.response
  handleGods(res)
}
function compareSpeed(a, b) {
    return a.Speed - b.Speed
}
function compareHealth(a, b) {
    return a.Health - b.Health
}
function createBar(value, min, max) {
    var diff = max - min
    var barWidth = 100 + (value - min) / diff * 100
    return '<div style="float:left; width:' + barWidth + 'px; background-color:' + rgbString(255, 255, 255) + ';">' + value + '</div>'
}
function handleGods(gods) {
    gods.sort(compareSpeed)

    var speeds = document.querySelector('#speeds tbody')
    var godsBySpeed = {}
    gods.forEach(function(god){
        godsBySpeed[god.Speed] = godsBySpeed[god.Speed] || []
        godsBySpeed[god.Speed].push(god)
    })
    for (var speed in godsBySpeed){
        var elRow = document.createElement('tr')
        var elLabel = document.createElement('td')
        elLabel.classList.add('label')
        elLabel.textContent = speed
        elRow.appendChild(elLabel)
        var elBoxCell = document.createElement('td')
        var elBox = document.createElement('div')
        elBox.classList.add('box')
        elBox.style.width = speed + 'px'
        elBoxCell.appendChild(elBox)
        elRow.appendChild(elBoxCell)
        var cellGods = document.createElement('td')
        var elGods = document.createElement('ul')
        elGods.classList.add('gods')
        godsBySpeed[speed].forEach(function(god){
            var elGod = document.createElement('li')
            var icon = new Image()
            icon.src = god.godIcon_URL
            elGod.appendChild(icon)
            var name = document.createElement('span')
            name.textContent = god.Name
            elGod.appendChild(name)
            elGods.appendChild(elGod)
        })
        cellGods.appendChild(elGods)
        elRow.appendChild(cellGods)
        speeds.appendChild(elRow)
    }

    gods.sort(compareHealth)
    var healths = document.querySelector('#healths')
    var min = null
    var max = null
    var minPL = null
    var maxPL = null
    for (var i in gods) {
        var god = gods[i]

        var field = god.Health
        min = min === null || field < min ? field : min
        max = max === null || field > max ? field : max

        var fieldPL = god.HealthPerLevel
        var field20 = fieldPL * 20
        minPL = (minPL === null || field20 < minPL) ? field20 : minPL
        maxPL = (maxPL === null || field20 > maxPL) ? field20 : maxPL
    }
    for (var i in gods) {
        var god = gods[i]
        var g = document.createElement('li')

        var field = god.Health
        var cBar = createBar(field, min, max)

        var fieldPL = god.HealthPerLevel
        var field20 = fieldPL * 20
        var cBarPL = createBar(field20, minPL, maxPL)

        var cName = god.Name

        g.innerHTML = cBar + cBarPL + cName
        healths.appendChild(g)
    }
}
function rgbString(r, g, b) {
    return 'rgb(' + r + ', ' + g + ', ' + b + ')'
}
