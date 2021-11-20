
function forEveryGod(fn)
{
	var gods = document.querySelectorAll('.god');
	for (var i = 0; i < gods.length; ++i)
	{
		fn(gods[i]);
	}
}

function setVisible(el, visible)
{
	el.style.display = visible ? '' : 'none';
}

godFilter = {
	init: function(){
		document.querySelector('#filter-god').addEventListener('input', this.onGodFilterChanged.bind(this));
		document.querySelector('#filter-god-clear').addEventListener('click', this.onGodFilterClear.bind(this))
		this.onGodFilterChanged();
	},
	onGodFilterChanged: function()
	{
		var filterValue = document.querySelector('#filter-god').value.toLowerCase();
		var updateGod = function(god)
		{
			var visible = god.querySelector('.godname').textContent.toLowerCase().indexOf(filterValue) !== -1;
			setVisible(god, visible);
		}
		forEveryGod(updateGod);
	},
	onGodFilterClear: function()
	{
		document.querySelector('#filter-god').value = '';
		this.onGodFilterChanged();
	},
}
godFilter.init()

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

pantheonFilter = {
	init: function() {
		var cbs = document.querySelectorAll('.filter-pantheon-cb');
		for (var i = 0; i < cbs.length; ++i)
		{
			cbs[i].addEventListener('change', this.onPantheonFilterChanged.bind(this))
		}
		document.querySelector('#filter-pantheon-clear').addEventListener('click', this.onPantheonFilterClear.bind(this))
		document.querySelector('#filter-pantheon-none').addEventListener('click', this.onPantheonFilterAll.bind(this))
		document.querySelector('#filter-pantheon-invert').addEventListener('click', this.onPantheonFilterInvert.bind(this))
		document.querySelector('#filter-pantheon-random').addEventListener('click', this.onPantheonFilterRandom1.bind(this))
		this.onPantheonFilterChanged();
	},
	onPantheonFilterChanged: function()
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
	},
	onPantheonFilterClear: function()
	{
		var cbs = document.querySelectorAll('.filter-pantheon-cb');
		setAll(cbs, true);
		this.onPantheonFilterChanged();
	},
	onPantheonFilterAll: function()
	{
		var cbs = document.querySelectorAll('.filter-pantheon-cb');
		setAll(cbs, false);
		this.onPantheonFilterChanged();
	},
	onPantheonFilterInvert: function()
	{
		var cbs = document.querySelectorAll('.filter-pantheon-cb');
		invertSelection(cbs);
		this.onPantheonFilterChanged();
	},
	onPantheonFilterRandom1: function()
	{
		var cbs = document.querySelectorAll('.filter-pantheon-cb');
		setRandom1(cbs);
		this.onPantheonFilterChanged();
	},
}
pantheonFilter.init()

roleFilter = {
	init: function(){
		var cbs = document.querySelectorAll('.filter-role-cb');
		for (var i = 0; i < cbs.length; ++i)
		{
			cbs[i].addEventListener('change', this.onRoleFilterChanged.bind(this))
		}
		document.querySelector('#filter-role-clear').addEventListener('click', this.onRoleFilterClear.bind(this));
		document.querySelector('#filter-role-none').addEventListener('click', this.onRoleFilterAll.bind(this));
		document.querySelector('#filter-role-invert').addEventListener('click', this.onRoleFilterInvert.bind(this));
		document.querySelector('#filter-role-random').addEventListener('click', this.onRoleFilterRandom1.bind(this));
		this.onRoleFilterChanged();
	},
	onRoleFilterChanged: function()
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
	},
	onRoleFilterClear: function()
	{
		var cbs = document.querySelectorAll('.filter-role-cb');
		setAll(cbs, true);
		this.onRoleFilterChanged();
	},
	onRoleFilterAll: function()
	{
		var cbs = document.querySelectorAll('.filter-role-cb');
		setAll(cbs, false);
		this.onRoleFilterChanged();
	},
	onRoleFilterInvert: function()
	{
		var cbs = document.querySelectorAll('.filter-role-cb');
		invertSelection(cbs);
		this.onRoleFilterChanged();
	},
	onRoleFilterRandom1: function()
	{
		var cbs = document.querySelectorAll('.filter-role-cb');
		setRandom1(cbs);
		this.onRoleFilterChanged();
	},
}
roleFilter.init()

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

godDefailsOverlay = {
	overlayContainer: null,
	init: function(){
		this.overlayContainer = document.querySelector('#god-overlay')
		this.overlayContainer.addEventListener('click', this.onClose.bind(this))

		var els = document.querySelectorAll('.godinfo-handle')
		for (let i = 0; i < els.length; ++i)
		{
			let handle = els[i]
			handle.addEventListener('click', this.onClick.bind(this))
		}
	},
	onClose: function(ev)
	{
		if (ev.target !== this.overlayContainer)
		{
			return
		}
		this.overlayContainer.classList.remove('active')
	},
	onCloseExplicit: function(ev)
	{
		this.overlayContainer.classList.remove('active')
	},
	onClick: function(sender)
	{
		var god = sender.target.parentElement
		var deepCopy = true
		var details = god.querySelector('.goddetails').cloneNode(deepCopy)
		this.overlayContainer.innerHTML = '<div id="god-overlay-close-handle">X</div>'
		this.overlayContainer.querySelector('#god-overlay-close-handle').addEventListener('click', this.onCloseExplicit.bind(this))
		this.overlayContainer.appendChild(details)
		this.overlayContainer.classList.add('active')
	}
}
godDefailsOverlay.init()
