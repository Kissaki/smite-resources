package main

import (
	"flag"
	"io/ioutil"
	"log"
	"strconv"

	"../../apidata"
	"../common/godhandling"
)

var developerID = flag.Int("devid", 0, "developer ID given by Hi-Rez")
var authKey = flag.String("authkey", "", "authentication key given by Hi-Rez")
var outFolder = flag.String("out", "data", "Output directory path")
var pullGeneric = flag.Bool("generic", false, "Generic data; reults: gods.json, items.json")
var pullPlayer = flag.String("player", "", "Player data; results: player-<name>-[..].json")
var pullPlayerID = flag.String("playerid", "", "Player data; results: player-<id>-[..].json")
var pullGodSkins = flag.Int("godskin", 0, "God skin data for a god by god ID")
var pullAllGodSkins = flag.Bool("godskins", false, "God skin data for all gods")

func pull(s apidata.Session, method string, outFile string, params string) {
	result := s.CallParamed(method, params)
	ioutil.WriteFile(*outFolder+"/"+outFile, []byte(result), 0666)
}

func pullGodSkinsData(s apidata.Session, godID int) {
	godIDStr := strconv.Itoa(godID)
	pull(s, apidata.GodSkins, "godskins-"+godIDStr+".json", "/"+godIDStr+"/"+apidata.LangEN)
}

func main() {
	flag.Parse()

	s := apidata.NewSession(apidata.EndpointPC, *developerID, *authKey, apidata.CreateFormatJson())
	log.Println(s.Ping())
	s.Connect()
	log.Println(s.TestSession())

	if *pullGeneric {
		pull(s, apidata.Gods, "gods.json", "/"+apidata.LangEN)
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

	if *pullGodSkins != 0 {
		godID := *pullGodSkins
		pullGodSkinsData(s, godID)
	}

	if *pullAllGodSkins {
		godsFilePath := *outFolder + "/gods.json"
		godlist, err := godhandling.ParseGods(godsFilePath)
		if err != nil {
			log.Fatalln(err)
		}
		for _, god := range godlist {
			godID := god.ID
			pullGodSkinsData(s, godID)
		}
	}
}
