package main

type PlayerGodRanks []struct {
	Assists     int64       `json:"Assists"`
	Deaths      int64       `json:"Deaths"`
	Kills       int64       `json:"Kills"`
	Losses      int64       `json:"Losses"`
	MinionKills int64       `json:"MinionKills"`
	Rank        int64       `json:"Rank"`
	Wins        int64       `json:"Wins"`
	Worshippers int64       `json:"Worshippers"`
	God         string      `json:"god"`
	GodID       string      `json:"god_id"`
	PlayerID    string      `json:"player_id"`
	RetMsg      interface{} `json:"ret_msg"`
}
