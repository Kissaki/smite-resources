package main

import (
	"apidata"
	"flag"
	"io/ioutil"
	"log"
)

var developerID = flag.Int("devid", 0, "developer ID given by Hi-Rez")
var authKey = flag.String("authkey", "", "authentication key given by Hi-Rez")

func pull(s apidata.Session, method string, outFile string, params string) {
	result := s.CallParamed(method, params)
	ioutil.WriteFile(outFile, []byte(result), 0666)
}

func main() {
	flag.Parse()

	s := apidata.NewSession(apidata.EndpointPC, *developerID, *authKey, apidata.CreateFormatJson())
	log.Println(s.Ping())
	s.Connect()
	log.Println(s.TestSession())

	pull(s, apidata.Gods, "gods.json", "/"+apidata.LangEN)
	pull(s, apidata.Items, "items.json", "/"+apidata.LangEN)
}
