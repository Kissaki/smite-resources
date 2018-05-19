package types

type Gods []struct {
	Ability1        string `json:"Ability1"`
	Ability2        string `json:"Ability2"`
	Ability3        string `json:"Ability3"`
	Ability4        string `json:"Ability4"`
	Ability5        string `json:"Ability5"`
	AbilityID1      int64  `json:"AbilityId1"`
	AbilityID2      int64  `json:"AbilityId2"`
	AbilityID3      int64  `json:"AbilityId3"`
	AbilityID4      int64  `json:"AbilityId4"`
	AbilityID5      int64  `json:"AbilityId5"`
	Ability1Details struct {
		Description struct {
			ItemDescription struct {
				Cooldown    string `json:"cooldown"`
				Cost        string `json:"cost"`
				Description string `json:"description"`
				Menuitems   []struct {
					Description string `json:"description"`
					Value       string `json:"value"`
				} `json:"menuitems"`
				Rankitems []struct {
					Description string `json:"description"`
					Value       string `json:"value"`
				} `json:"rankitems"`
				SecondaryDescription string `json:"secondaryDescription"`
			} `json:"itemDescription"`
		} `json:"Description"`
		ID      int64  `json:"Id"`
		Summary string `json:"Summary"`
		URL     string `json:"URL"`
	} `json:"Ability_1"`
	Ability2Details struct {
		Description struct {
			ItemDescription struct {
				Cooldown    string `json:"cooldown"`
				Cost        string `json:"cost"`
				Description string `json:"description"`
				Menuitems   []struct {
					Description string `json:"description"`
					Value       string `json:"value"`
				} `json:"menuitems"`
				Rankitems []struct {
					Description string `json:"description"`
					Value       string `json:"value"`
				} `json:"rankitems"`
				SecondaryDescription string `json:"secondaryDescription"`
			} `json:"itemDescription"`
		} `json:"Description"`
		ID      int64  `json:"Id"`
		Summary string `json:"Summary"`
		URL     string `json:"URL"`
	} `json:"Ability_2"`
	Ability3Details struct {
		Description struct {
			ItemDescription struct {
				Cooldown    string `json:"cooldown"`
				Cost        string `json:"cost"`
				Description string `json:"description"`
				Menuitems   []struct {
					Description string `json:"description"`
					Value       string `json:"value"`
				} `json:"menuitems"`
				Rankitems []struct {
					Description string `json:"description"`
					Value       string `json:"value"`
				} `json:"rankitems"`
				SecondaryDescription string `json:"secondaryDescription"`
			} `json:"itemDescription"`
		} `json:"Description"`
		ID      int64  `json:"Id"`
		Summary string `json:"Summary"`
		URL     string `json:"URL"`
	} `json:"Ability_3"`
	Ability4Details struct {
		Description struct {
			ItemDescription struct {
				Cooldown    string `json:"cooldown"`
				Cost        string `json:"cost"`
				Description string `json:"description"`
				Menuitems   []struct {
					Description string `json:"description"`
					Value       string `json:"value"`
				} `json:"menuitems"`
				Rankitems []struct {
					Description string `json:"description"`
					Value       string `json:"value"`
				} `json:"rankitems"`
				SecondaryDescription string `json:"secondaryDescription"`
			} `json:"itemDescription"`
		} `json:"Description"`
		ID      int64  `json:"Id"`
		Summary string `json:"Summary"`
		URL     string `json:"URL"`
	} `json:"Ability_4"`
	Ability5Details struct {
		Description struct {
			ItemDescription struct {
				Cooldown    string `json:"cooldown"`
				Cost        string `json:"cost"`
				Description string `json:"description"`
				Menuitems   []struct {
					Description string `json:"description"`
					Value       string `json:"value"`
				} `json:"menuitems"`
				Rankitems []struct {
					Description string `json:"description"`
					Value       string `json:"value"`
				} `json:"rankitems"`
				SecondaryDescription string `json:"secondaryDescription"`
			} `json:"itemDescription"`
		} `json:"Description"`
		ID      int64  `json:"Id"`
		Summary string `json:"Summary"`
		URL     string `json:"URL"`
	} `json:"Ability_5"`
	AttackSpeed                int64   `json:"AttackSpeed"`
	AttackSpeedPerLevel        float64 `json:"AttackSpeedPerLevel"`
	Cons                       string  `json:"Cons"`
	HP5PerLevel                float64 `json:"HP5PerLevel"`
	Health                     int64   `json:"Health"`
	HealthPerFive              int64   `json:"HealthPerFive"`
	HealthPerLevel             int64   `json:"HealthPerLevel"`
	Lore                       string  `json:"Lore"`
	MP5PerLevel                float64 `json:"MP5PerLevel"`
	MagicProtection            int64   `json:"MagicProtection"`
	MagicProtectionPerLevel    int64   `json:"MagicProtectionPerLevel"`
	MagicalPower               int64   `json:"MagicalPower"`
	MagicalPowerPerLevel       float64 `json:"MagicalPowerPerLevel"`
	Mana                       int64   `json:"Mana"`
	ManaPerFive                float64 `json:"ManaPerFive"`
	ManaPerLevel               int64   `json:"ManaPerLevel"`
	Name                       string  `json:"Name"`
	OnFreeRotation             string  `json:"OnFreeRotation"`
	Pantheon                   string  `json:"Pantheon"`
	PhysicalPower              int64   `json:"PhysicalPower"`
	PhysicalPowerPerLevel      int64   `json:"PhysicalPowerPerLevel"`
	PhysicalProtection         int64   `json:"PhysicalProtection"`
	PhysicalProtectionPerLevel float64 `json:"PhysicalProtectionPerLevel"`
	Pros                       string  `json:"Pros"`
	Roles                      string  `json:"Roles"`
	Speed                      int64   `json:"Speed"`
	Title                      string  `json:"Title"`
	Type                       string  `json:"Type"`
	AbilityDescription1        struct {
		ItemDescription struct {
			Cooldown    string `json:"cooldown"`
			Cost        string `json:"cost"`
			Description string `json:"description"`
			Menuitems   []struct {
				Description string `json:"description"`
				Value       string `json:"value"`
			} `json:"menuitems"`
			Rankitems []struct {
				Description string `json:"description"`
				Value       string `json:"value"`
			} `json:"rankitems"`
			SecondaryDescription string `json:"secondaryDescription"`
		} `json:"itemDescription"`
	} `json:"abilityDescription1"`
	AbilityDescription2 struct {
		ItemDescription struct {
			Cooldown    string `json:"cooldown"`
			Cost        string `json:"cost"`
			Description string `json:"description"`
			Menuitems   []struct {
				Description string `json:"description"`
				Value       string `json:"value"`
			} `json:"menuitems"`
			Rankitems []struct {
				Description string `json:"description"`
				Value       string `json:"value"`
			} `json:"rankitems"`
			SecondaryDescription string `json:"secondaryDescription"`
		} `json:"itemDescription"`
	} `json:"abilityDescription2"`
	AbilityDescription3 struct {
		ItemDescription struct {
			Cooldown    string `json:"cooldown"`
			Cost        string `json:"cost"`
			Description string `json:"description"`
			Menuitems   []struct {
				Description string `json:"description"`
				Value       string `json:"value"`
			} `json:"menuitems"`
			Rankitems []struct {
				Description string `json:"description"`
				Value       string `json:"value"`
			} `json:"rankitems"`
			SecondaryDescription string `json:"secondaryDescription"`
		} `json:"itemDescription"`
	} `json:"abilityDescription3"`
	AbilityDescription4 struct {
		ItemDescription struct {
			Cooldown    string `json:"cooldown"`
			Cost        string `json:"cost"`
			Description string `json:"description"`
			Menuitems   []struct {
				Description string `json:"description"`
				Value       string `json:"value"`
			} `json:"menuitems"`
			Rankitems []struct {
				Description string `json:"description"`
				Value       string `json:"value"`
			} `json:"rankitems"`
			SecondaryDescription string `json:"secondaryDescription"`
		} `json:"itemDescription"`
	} `json:"abilityDescription4"`
	AbilityDescription5 struct {
		ItemDescription struct {
			Cooldown    string `json:"cooldown"`
			Cost        string `json:"cost"`
			Description string `json:"description"`
			Menuitems   []struct {
				Description string `json:"description"`
				Value       string `json:"value"`
			} `json:"menuitems"`
			Rankitems []struct {
				Description string `json:"description"`
				Value       string `json:"value"`
			} `json:"rankitems"`
			SecondaryDescription string `json:"secondaryDescription"`
		} `json:"itemDescription"`
	} `json:"abilityDescription5"`
	BasicAttack struct {
		ItemDescription struct {
			Cooldown    string `json:"cooldown"`
			Cost        string `json:"cost"`
			Description string `json:"description"`
			Menuitems   []struct {
				Description string `json:"description"`
				Value       string `json:"value"`
			} `json:"menuitems"`
			Rankitems            []interface{} `json:"rankitems"`
			SecondaryDescription string        `json:"secondaryDescription"`
		} `json:"itemDescription"`
	} `json:"basicAttack"`
	GodAbility1URL string      `json:"godAbility1_URL"`
	GodAbility2URL string      `json:"godAbility2_URL"`
	GodAbility3URL string      `json:"godAbility3_URL"`
	GodAbility4URL string      `json:"godAbility4_URL"`
	GodAbility5URL string      `json:"godAbility5_URL"`
	GodCardURL     string      `json:"godCard_URL"`
	GodIconURL     string      `json:"godIcon_URL"`
	ID             int64       `json:"id"`
	LatestGod      string      `json:"latestGod"`
	RetMsg         interface{} `json:"ret_msg"`
}
