package main

import (
	"apidata"
	"flag"
	"io/ioutil"
	"log"
)

var developerID = flag.Int("devid", 0, "developer ID given by Hi-Rez")
var authKey = flag.String("authkey", "", "authentication key given by Hi-Rez")
var outFolder = flag.String("out", "data", "Output directory path")
var pullGeneric = flag.Bool("generic", false, "Generic data; reults: gods.json, items.json")
var pullPlayer = flag.String("player", "", "Player data; results: player-<name>-[..].json")
var pullPlayerID = flag.String("playerid", "", "Player data; results: player-<id>-[..].json")

func pull(s apidata.Session, method string, outFile string, params string) {
	result := s.CallParamed(method, params)
	ioutil.WriteFile(*outFolder+"/"+outFile, []byte(result), 0666)
}

func main() {
	flag.Parse()

	s := apidata.NewSession(apidata.EndpointPC, *developerID, *authKey, apidata.CreateFormatJson())
	log.Println(s.Ping())
	s.Connect()
	log.Println(s.TestSession())

	if *pullGeneric {
		pull(s, apidata.Gods, "gods.json", "/"+apidata.LangEN)
		pull(s, apidata.Items, "items.json", "/"+apidata.LangEN)
	}

	if *pullPlayer != "" {
		playerID := *pullPlayer
		pull(s, apidata.Player, "player-"+playerID+"-info.json", "/"+playerID)
		pull(s, apidata.PlayerStatus, "player-"+playerID+"-status.json", "/"+playerID)
		pull(s, apidata.PlayerFriends, "player-"+playerID+"-friends.json", "/"+playerID)
		pull(s, apidata.PlayerGodRanks, "player-"+playerID+"-godranks.json", "/"+playerID)
		pull(s, apidata.PlayerMatchHistory, "player-"+playerID+"-matchhistory.json", "/"+playerID)
	}

	if *pullPlayerID != "" {
		playerID := *pullPlayerID
		pull(s, apidata.PlayerAchievements, "player-"+playerID+"-achievements.json", "/"+playerID)
	}
}
