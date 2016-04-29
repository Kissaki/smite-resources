package gods

import "strings"

type Gods []God

func (gods Gods) Len() int               { return len(gods) }
func (gods Gods) Swap(i int, j int)      { gods[i], gods[j] = gods[j], gods[i] }
func (gods Gods) Less(i int, j int) bool { return strings.Compare(gods[i].Name, gods[j].Name) < 0 }

type God struct {
	Ability1                   Ability `json:"Ability_1"`
	Ability2                   Ability `json:"Ability_2"`
	Ability3                   Ability `json:"Ability_3"`
	Ability4                   Ability `json:"Ability_4"`
	Ability5                   Ability `json:"Ability_5"`
	AttackSpeed                float32 `json:"AttackSpeed"`
	AttackSpeedPerLevel        float32 `json:"AttackSpeedPerLevel"`
	Cons                       string  `json:"Cons"`
	HP5PerLevel                float32 `json:"HP5PerLevel"`
	Health                     int     `json:"Health"`
	HealthPerFive              int     `json:"HealthPerFive"`
	HealthPerLevel             int     `json:"HealthPerLevel"`
	Lore                       string  `json:"Lore"`
	MP5PerLevel                float32 `json:"MP5PerLevel"`
	MagicProtection            int     `json:"MagicProtection"`
	MagicProtectionPerLevel    float32 `json:"MagicProtectionPerLevel"`
	MagicalPower               int     `json:"MagicalPower"`
	MagicalPowerPerLevel       float32 `json:"MagicalPowerPerLevel"`
	Mana                       int     `json:"Mana"`
	ManaPerFive                float32 `json:"ManaPerFive"`
	ManaPerLevel               int     `json:"ManaPerLevel"`
	Name                       string  `json:"Name"`
	OnFreeRotation             string  `json:"OnFreeRotation"`
	Pantheon                   string  `json:"Pantheon"`
	PhysicalPower              int     `json:"PhysicalPower"`
	PhysicalPowerPerLevel      float32 `json:"PhysicalPowerPerLevel"`
	PhysicalProtection         int     `json:"PhysicalProtection"`
	PhysicalProtectionPerLevel float64 `json:"PhysicalProtectionPerLevel"`
	Pros                       string  `json:"Pros"`
	Roles                      string  `json:"Roles"`
	Speed                      int     `json:"Speed"`
	Title                      string  `json:"Title"`
	Type                       string  `json:"Type"`
	BasicAttack                struct {
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
	GodCardURL string      `json:"godCard_URL"`
	GodIconURL string      `json:"godIcon_URL"`
	ID         int         `json:"id"`
	LatestGod  string      `json:"latestGod"`
	RetMsg     interface{} `json:"ret_msg"`
}
