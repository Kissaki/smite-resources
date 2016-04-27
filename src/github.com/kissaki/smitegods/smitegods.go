package main

import (
	"bufio"
	"encoding/json"
	"flag"
	"fmt"
	"github.com/kissaki/smitegods/apidata"
	"github.com/kissaki/smitegods/apidata/gods"
	"html/template"
	"io/ioutil"
	"log"
	"net/http"
	"os"
)

const (
	FILE_GODS = "gods.json"
)

var UpdateGodData = flag.Bool("updategoddata", false, "Specifies whether the God data should be updated form the SMITE API. This requires specifying the developer ID and authentication key.")
var DeveloperID = flag.Int("devid", 0, "developer ID given by Hi-Rez")
var AuthKey = flag.String("authkey", "", "authentication key given by Hi-Rez")
var OutFile = flag.String("target", "smitegods.html", "Filename that the HTML god overview is written to.")

// Default Request Handler
func defaultHandler(w http.ResponseWriter, r *http.Request) {
	fmt.Fprintf(w, "<h1>Hello %s!</h1>", r.URL.Path[1:])
}

func APITestAndGodsDownload() {
	s := apidata.NewSession(apidata.EndpointPC, *DeveloperID, *AuthKey, apidata.CreateFormatJson())
	log.Println(s.Ping())
	s.Connect()
	log.Println(s.TestSession())
	s.GetGods()
}

func httpListen() {
	http.HandleFunc("/", defaultHandler)
	http.ListenAndServe(":8080", nil)
}

func parseGods() (gods.Gods, error) {
	godsJSON, err := ioutil.ReadFile(FILE_GODS)
	if err != nil {
		return nil, err
	}
	var gods gods.Gods
	err = json.Unmarshal(godsJSON, &gods)
	if err != nil {
		return nil, err
	}
	return gods, nil
}

type pantheonGodList map[string]roleGodMap
type roleGodMap map[string]godsByID
type godsByID map[int]gods.God

type set map[string]struct{}
type Pantheons set
type Roles set

type TemplateData struct {
	Pantheons       Pantheons
	Roles           Roles
	PantheonGodList pantheonGodList
}

func LogToFile() {
	f, err := os.OpenFile("log.log", os.O_RDWR|os.O_CREATE|os.O_APPEND, 0666)
	if err != nil {
		log.Fatalf("error opening file: %v", err)
	}
	defer f.Close()
	log.SetOutput(f)
}

func Dump(filename string, data string) {
	f, err := os.OpenFile("log.log", os.O_RDWR|os.O_CREATE|os.O_APPEND, 0666)
	if err != nil {
		log.Fatalf("error opening file: %v", err)
	}
	defer f.Close()
	ioutil.WriteFile(filename, []byte(data), 0666)
}

func initFlags() error {
	flag.Parse()

	if *UpdateGodData && (*DeveloperID == 0 || *AuthKey == "") {
		fmt.Println("invalid or unspecified parameters")
		flag.Usage()
		fmt.Println("#" + flag.Arg(0))
		log.Fatalln("FATAL: invalid or unspecified parameters")
	}

	return nil
}

func main() {
	//	apidata.LogToFile()

	if initFlags() != nil {
		os.Exit(1)
	}

	if *UpdateGodData {
		APITestAndGodsDownload()
	}

	godlist, err := parseGods()
	if err != nil {
		log.Fatalln(err)
	}

	roles := make(Roles)
	pantheons := make(Pantheons)
	godMap := make(pantheonGodList)
	for _, god := range godlist {
		roles[god.Roles] = struct{}{}
		pantheons[god.Pantheon] = struct{}{}

		if godMap[god.Pantheon] == nil {
			godMap[god.Pantheon] = make(roleGodMap)
		}
		roleMap := godMap[god.Pantheon]

		if roleMap[god.Roles] == nil {
			roleMap[god.Roles] = make(godsByID)
		}
		roleGods := roleMap[god.Roles]

		roleGods[god.ID] = god
	}

	tmpl, err := template.New("gods.html").ParseGlob("templates/*.html")
	if err != nil {
		log.Fatalln("Failed to parse template files: ", err)
	}
	file, err := os.Create(*OutFile)
	if err != nil {
		log.Fatalln("Failed to create output file: ", err)
	}
	defer file.Close()

	writer := bufio.NewWriter(file)
	defer writer.Flush()

	err = tmpl.Execute(writer, TemplateData{pantheons, roles, godMap})
	if err != nil {
		log.Fatalln("Failed to execute HTML template with data: ", err)
	}

	log.Println("DONE")

	//	for _, god := range gods {
	//		fmt.Println(god.Name, god.Pantheon, god.Roles, god.Type, god.OnFreeRotation, god.GodIconURL, god.GodCardURL)
	//	}
	//	httpListen()
}
