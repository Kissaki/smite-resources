package types

type PlayerInfo []struct {
	AvatarURL             string `json:"Avatar_URL"`
	CreatedDatetime       string `json:"Created_Datetime"`
	ID                    int64  `json:"Id"`
	LastLoginDatetime     string `json:"Last_Login_Datetime"`
	Leaves                int64  `json:"Leaves"`
	Level                 int64  `json:"Level"`
	Losses                int64  `json:"Losses"`
	MasteryLevel          int64  `json:"MasteryLevel"`
	Name                  string `json:"Name"`
	PersonalStatusMessage string `json:"Personal_Status_Message"`
	RankStatConquest      int64  `json:"Rank_Stat_Conquest"`
	RankStatDuel          int64  `json:"Rank_Stat_Duel"`
	RankStatJoust         int64  `json:"Rank_Stat_Joust"`
	RankedConquest        struct {
		Leaves           int64       `json:"Leaves"`
		Losses           int64       `json:"Losses"`
		Name             string      `json:"Name"`
		Points           int64       `json:"Points"`
		PrevRank         int64       `json:"PrevRank"`
		Rank             int64       `json:"Rank"`
		RankStatConquest interface{} `json:"Rank_Stat_Conquest"`
		RankStatDuel     interface{} `json:"Rank_Stat_Duel"`
		RankStatJoust    interface{} `json:"Rank_Stat_Joust"`
		Season           int64       `json:"Season"`
		Tier             int64       `json:"Tier"`
		Trend            int64       `json:"Trend"`
		Wins             int64       `json:"Wins"`
		PlayerID         interface{} `json:"player_id"`
		RetMsg           interface{} `json:"ret_msg"`
	} `json:"RankedConquest"`
	RankedDuel struct {
		Leaves           int64       `json:"Leaves"`
		Losses           int64       `json:"Losses"`
		Name             string      `json:"Name"`
		Points           int64       `json:"Points"`
		PrevRank         int64       `json:"PrevRank"`
		Rank             int64       `json:"Rank"`
		RankStatConquest interface{} `json:"Rank_Stat_Conquest"`
		RankStatDuel     interface{} `json:"Rank_Stat_Duel"`
		RankStatJoust    interface{} `json:"Rank_Stat_Joust"`
		Season           int64       `json:"Season"`
		Tier             int64       `json:"Tier"`
		Trend            int64       `json:"Trend"`
		Wins             int64       `json:"Wins"`
		PlayerID         interface{} `json:"player_id"`
		RetMsg           interface{} `json:"ret_msg"`
	} `json:"RankedDuel"`
	RankedJoust struct {
		Leaves           int64       `json:"Leaves"`
		Losses           int64       `json:"Losses"`
		Name             string      `json:"Name"`
		Points           int64       `json:"Points"`
		PrevRank         int64       `json:"PrevRank"`
		Rank             int64       `json:"Rank"`
		RankStatConquest interface{} `json:"Rank_Stat_Conquest"`
		RankStatDuel     interface{} `json:"Rank_Stat_Duel"`
		RankStatJoust    interface{} `json:"Rank_Stat_Joust"`
		Season           int64       `json:"Season"`
		Tier             int64       `json:"Tier"`
		Trend            int64       `json:"Trend"`
		Wins             int64       `json:"Wins"`
		PlayerID         interface{} `json:"player_id"`
		RetMsg           interface{} `json:"ret_msg"`
	} `json:"RankedJoust"`
	Region            string      `json:"Region"`
	TeamID            int64       `json:"TeamId"`
	TeamName          string      `json:"Team_Name"`
	TierConquest      int64       `json:"Tier_Conquest"`
	TierDuel          int64       `json:"Tier_Duel"`
	TierJoust         int64       `json:"Tier_Joust"`
	TotalAchievements int64       `json:"Total_Achievements"`
	TotalWorshippers  int64       `json:"Total_Worshippers"`
	Wins              int64       `json:"Wins"`
	RetMsg            interface{} `json:"ret_msg"`
}
