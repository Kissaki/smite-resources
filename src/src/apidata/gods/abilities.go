package gods

type Ability struct {
	Description AbilityDescription `json:"Description"`
	ID          int                `json:"Id"`
	Summary     string             `json:"Summary"`
	URL         string             `json:"URL"`
}

type AbilityDescription struct {
	ItemDescription struct {
		// For basic attacks: empty
		Cooldown string `json:"cooldown"`
		// For basic attacks: empty
		Cost string `json:"cost"`
		// For basic attacks: empty
		Description string `json:"description"`
		// List of key-value pairs.
		// For abilities the following keys apply:
		//   "Ability:", "Affects:", "Damage:", "Radius:"
		// For Basic attacks, the following keys apply:
		//   "Damage:", "Progression:"
		Menuitems []struct {
			Description string `json:"description"`
			Value       string `json:"value"`
		} `json:"menuitems"`
		// List of key-value pairs.
		// For abilities:
		// For basic attack: empty
		Rankitems []struct {
			Description string `json:"description"`
			Value       string `json:"value"`
		} `json:"rankitems"`
		SecondaryDescription string `json:"secondaryDescription"`
	} `json:"itemDescription"`
}
