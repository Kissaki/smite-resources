package types

type Items []struct {
	ChildItemID     int64  `json:"ChildItemId"`
	DeviceName      string `json:"DeviceName"`
	IconID          int64  `json:"IconId"`
	ItemDescription struct {
		Description string `json:"Description"`
		Menuitems   []struct {
			Description string `json:"Description"`
			Value       string `json:"Value"`
		} `json:"Menuitems"`
		SecondaryDescription string `json:"SecondaryDescription"`
	} `json:"ItemDescription"`
	ItemID       int64       `json:"ItemId"`
	ItemTier     int64       `json:"ItemTier"`
	Price        int64       `json:"Price"`
	RootItemID   int64       `json:"RootItemId"`
	ShortDesc    string      `json:"ShortDesc"`
	StartingItem bool        `json:"StartingItem"`
	Type         string      `json:"Type"`
	ItemIconURL  string      `json:"itemIcon_URL"`
	RetMsg       interface{} `json:"ret_msg"`
}
