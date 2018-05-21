
function forEveryGod(fn)
{
	var gods = document.querySelectorAll('.god');
	for (var i = 0; i < gods.length; ++i)
	{
		fn(gods[i]);
	}
}

function onGodiconsizeChanged()
{
	var value = document.querySelector('#god-icon-size').value;
	document.querySelector('#god-icon-size-value').innerHTML = value;
	var newSize = value + 'px';
	var updateSize = function(god)
	{
		god.style.width = newSize;
		var pseudoIcon = god.querySelector('.godnoicon');
		if (pseudoIcon !== null)
		{
			pseudoIcon.style.height = newSize
		}
	}
	forEveryGod(updateSize);
}
function onGodiconsizeClear()
{
	document.querySelector('#god-icon-size').value = '64';
	onGodiconsizeChanged();
}
document.querySelector('#god-icon-size').addEventListener('input', onGodiconsizeChanged);
document.querySelector('#god-icon-size-clear').addEventListener('click', onGodiconsizeClear)
onGodiconsizeChanged();

function setVisible(el, visible)
{
	el.style.display = visible ? '' : 'none';
}

function onGodFilterChanged()
{
	var filterValue = document.querySelector('#filter-god').value.toLowerCase();
	var updateGod = function(god)
	{
		var visible = god.querySelector('.godname').textContent.toLowerCase().indexOf(filterValue) !== -1;
		setVisible(god, visible);
	}
	forEveryGod(updateGod);
}
function onGodFilterClear()
{
	document.querySelector('#filter-god').value = '';
	onGodFilterChanged();
}
document.querySelector('#filter-god').addEventListener('input', onGodFilterChanged);
document.querySelector('#filter-god-clear').addEventListener('click', onGodFilterClear)
onGodFilterChanged();


function setAll(cbs, selected)
{
	for (var i = 0; i < cbs.length; ++i)
	{
		var cb = cbs[i];
		cb.checked = selected;
	}
}
function invertSelection(cbs)
{
	for (var i = 0; i < cbs.length; ++i)
	{
		var cb = cbs[i];
		cb.checked = !cb.checked;
	}
}
function setRandom1(cbs)
{
	setAll(cbs, false);
	var i = NaN;
	do
	{
		i = Math.random();
	} while (i === 1);
	i = Math.floor(i * cbs.length);
	cbs[i].checked = true;
}
function onPantheonFilterChanged()
{
	var options = document.querySelectorAll('.filter-pantheon-option');
	for (var i = 0; i < options.length; ++i)
	{
		var option = options[i];
		var pantheon = option.id.substr('filter-pantheon-option-'.length);
		var visible = option.querySelector('.filter-pantheon-cb').checked;
		var row = document.querySelector('#pantheon-row-' + pantheon)
		setVisible(row, visible);
	}
}
function onPantheonFilterClear()
{
	var cbs = document.querySelectorAll('.filter-pantheon-cb');
	setAll(cbs, true);
	onPantheonFilterChanged();
}
function onPantheonFilterAll()
{
	var cbs = document.querySelectorAll('.filter-pantheon-cb');
	setAll(cbs, false);
	onPantheonFilterChanged();
}
function onPantheonFilterInvert()
{
	var cbs = document.querySelectorAll('.filter-pantheon-cb');
	invertSelection(cbs);
	onPantheonFilterChanged();
}
function onPantheonFilterRandom1()
{
	var cbs = document.querySelectorAll('.filter-pantheon-cb');
	setRandom1(cbs);
	onPantheonFilterChanged();
}
var cbs = document.querySelectorAll('.filter-pantheon-cb');
for (var i = 0; i < cbs.length; ++i)
{
	cbs[i].addEventListener('change', onPantheonFilterChanged)
}
document.querySelector('#filter-pantheon-clear').addEventListener('click', onPantheonFilterClear)
document.querySelector('#filter-pantheon-none').addEventListener('click', onPantheonFilterAll)
document.querySelector('#filter-pantheon-invert').addEventListener('click', onPantheonFilterInvert)
document.querySelector('#filter-pantheon-random').addEventListener('click', onPantheonFilterRandom1)
onPantheonFilterChanged();

function onRoleFilterChanged()
{
	var columnheads = document.querySelector('#smitegodstable tr').querySelectorAll('th');
	for (var i = 1; i < columnheads.length; ++i)
	{
		var roleCell = columnheads[i];
		var role = roleCell.id.substr("role-column-".length);
		var filter = document.querySelector('#filter-role-cb-' + role);
		var visible = filter.checked;
		setVisible(roleCell, visible);
		// rows have a head column, so td index is 1-based; which matches our off-by-one heads
		var offset = i - 1;
		var dataCells = document.querySelectorAll('#smitegodstable td');
		while (dataCells[offset] !== undefined)
		{
			setVisible(dataCells[offset], visible);
			offset += columnheads.length - 1;
		}
	}
}
function onRoleFilterClear()
{
	var cbs = document.querySelectorAll('.filter-role-cb');
	setAll(cbs, true);
	onRoleFilterChanged();
}
function onRoleFilterAll()
{
	var cbs = document.querySelectorAll('.filter-role-cb');
	setAll(cbs, false);
	onRoleFilterChanged();
}
function onRoleFilterInvert()
{
	var cbs = document.querySelectorAll('.filter-role-cb');
	invertSelection(cbs);
	onRoleFilterChanged();
}
function onRoleFilterRandom1()
{
	var cbs = document.querySelectorAll('.filter-role-cb');
	setRandom1(cbs);
	onRoleFilterChanged();
}
var cbs = document.querySelectorAll('.filter-role-cb');
for (var i = 0; i < cbs.length; ++i)
{
	cbs[i].addEventListener('change', onRoleFilterChanged)
}
document.querySelector('#filter-role-clear').addEventListener('click', onRoleFilterClear);
document.querySelector('#filter-role-none').addEventListener('click', onRoleFilterAll);
document.querySelector('#filter-role-invert').addEventListener('click', onRoleFilterInvert);
document.querySelector('#filter-role-random').addEventListener('click', onRoleFilterRandom1);
onRoleFilterChanged();

randomGodPicker = {
	init: function(){
		document.getElementById('random-pick-button').addEventListener('click', this.onPickRandom.bind(this));
	},
	onPickRandom: function()
	{
		var gods = document.querySelectorAll('.god')
		if (gods.length > 0)
		{
			var visibleGods = []
			gods.forEach(function(g){ if (g.offsetParent !== null) { visibleGods.push(g) } })
			this.setPickRandomResult(this.pickRandomFromArray(visibleGods))
		}
	},
	pickRandomFromArray: function(array)
	{
		var i = this.getRandomIntInclusive(0, array.length - 1)
		return array[i]
	},
	// https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Math/random#Getting_a_random_integer_between_two_values_inclusive
	getRandomIntInclusive: function(min, max) {
		min = Math.ceil(min);
		max = Math.floor(max);
		return Math.floor(Math.random() * (max - min + 1)) + min; //The maximum is inclusive and the minimum is inclusive 
	},
	setPickRandomResult: function(godDomObject)
	{
		var res = document.getElementById('random-pick-result')
		while (res.firstChild)
		{
			res.removeChild(res.firstChild)
		}
		res.appendChild(godDomObject.cloneNode(true))
	},
}
randomGodPicker.init()
