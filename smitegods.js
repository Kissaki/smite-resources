
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
	var filterValue = document.querySelector('#filter-pantheon').value.toLowerCase();
	var rows = document.querySelectorAll('#smitegodstable tr');
	for (var i = 1; i < rows.length; ++i)
	{
		var visible = rows[i].querySelector('th').textContent.toLowerCase().indexOf(filterValue) !== -1;
		setVisible(rows[i], visible);
	}
}
function onPantheonFilterClear()
{
	document.querySelector('#filter-pantheon').value = '';
	onPantheonFilterChanged();
}
document.querySelector('#filter-pantheon').addEventListener('input', onPantheonFilterChanged);
document.querySelector('#filter-pantheon-clear').addEventListener('click', onPantheonFilterClear)
onPantheonFilterChanged();

function onRoleFilterChanged()
{
	var filterValue = document.querySelector('#filter-role').value.toLowerCase();
	var columnheads = document.querySelector('#smitegodstable tr').querySelectorAll('th');
	for (var i = 1; i < columnheads.length; ++i)
	{
		var roleCell = columnheads[i];
		var visible = roleCell.textContent.toLowerCase().indexOf(filterValue) !== -1;
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
	document.querySelector('#filter-role').value = '';
	onRoleFilterChanged();
}
document.querySelector('#filter-role').addEventListener('input', onRoleFilterChanged);
document.querySelector('#filter-role-clear').addEventListener('click', onRoleFilterClear);
onRoleFilterChanged();
