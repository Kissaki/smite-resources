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
    createInfos(gods, 'AttackSpeed')
    createInfos(gods, 'AttackSpeedPerLevel')
    createInfos(gods, 'HP5PerLevel')
    createInfos(gods, 'Health')
    createInfos(gods, 'HealthPerFive')
    createInfos(gods, 'HealthPerLevel')
    createInfos(gods, 'MP5PerLevel')
    createInfos(gods, 'MagicProtection')
    createInfos(gods, 'MagicProtectionPerLevel')
    createInfos(gods, 'MagicalPower')
    createInfos(gods, 'MagicalPowerPerLevel')
    createInfos(gods, 'Mana')
    createInfos(gods, 'ManaPerFive')
    createInfos(gods, 'ManaPerLevel')
    createInfos(gods, 'PhysicalPower')
    createInfos(gods, 'PhysicalPowerPerLevel')
    createInfos(gods, 'PhysicalProtection')
    createInfos(gods, 'PhysicalProtectionPerLevel')
    createInfos(gods, 'Speed')
}
function rgbString(r, g, b) {
    return 'rgb(' + r + ', ' + g + ', ' + b + ')'
}
function createInfos(gods, property){
    var godsBy = {}
    var valueRange = {min:999, max:0}
    gods.forEach(function(god){
        var value = god[property]
        godsBy[value] = godsBy[value] || []
        godsBy[value].push(god)
        if (valueRange.min > value) {
            valueRange.min = value
        } else if (valueRange.max < value) {
            valueRange.max = value
        }
    })
    
    var table = document.createElement('table')
    table.innerHTML = '<thead><th colspan="2">'+property+'</th><th>Gods</th></thead><tbody></tbody>'
    document.querySelector('#godstats-container').appendChild(table)
    
    var tbody = table.querySelector('tbody')
    var values = []
    for (var value in godsBy){
        values.push(value)
    }
    values.sort(function (a, b) {
        return (parseFloat(a) - parseFloat(b))
    })
    values.forEach(function(value)
    {
        var tr = document.createElement('tr')
        tr.innerHTML = '<td>'+value+'</td><td class="box-container">'+getBox(value, valueRange)+'</td><td>' + getGodInfoList(godsBy[value]) + '</td>'
        tbody.appendChild(tr)
    })
}
function getBox(value, valueRange){
    var diff = valueRange.max - valueRange.min
    var relative = diff === 0 ? 100 : (value - valueRange.min) / diff * 100
    return '<div class="box" style="width:' + Math.round(relative) + 'px"></div>'
}
function getGodInfoList(gods){
    var infolist = ''
    gods.forEach(function(god){
        infolist += (infolist.length > 0 ?', ' : '') + getGodInfo(god)
    })
    return infolist
}
function getGodInfo(god){
    return god.Name + getGodRoleIcon(god)
}
function getGodRoleIcon(god){
    var role = god.Roles.replace(' ', '').toLowerCase()
    var caption = role[0].toUpperCase()
    return '<span class="godrole godrole-'+role+'">'+caption+'</span>'
}
