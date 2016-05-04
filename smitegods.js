
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
	var updateSize = function(god){god.style.width = newSize;}
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
	for (var i = 0; i < cbs.length; ++i)
	{
		var cb = cbs[i];
		cb.checked = true;
	}
	onPantheonFilterChanged();
}
var cbs = document.querySelectorAll('.filter-pantheon-cb');
for (var i = 0; i < cbs.length; ++i)
{
	cbs[i].addEventListener('change', onPantheonFilterChanged)
}
document.querySelector('#filter-pantheon-clear').addEventListener('click', onPantheonFilterClear)
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
	for (var i = 0; i < cbs.length; ++i)
	{
		var cb = cbs[i];
		cb.checked = true;
	}
	onRoleFilterChanged();
}
var cbs = document.querySelectorAll('.filter-role-cb');
for (var i = 0; i < cbs.length; ++i)
{
	cbs[i].addEventListener('change', onRoleFilterChanged)
}
document.querySelector('#filter-role-clear').addEventListener('click', onRoleFilterClear);
onRoleFilterChanged();
