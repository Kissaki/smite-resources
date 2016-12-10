package main

import (
	"bufio"
	"encoding/json"
	"flag"
	"fmt"
	"apidata"
	"apidata/gods"
	"html/template"
	"io/ioutil"
	"log"
	"net/http"
	"os"
	"sort"
	"strings"
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

type TemplateData struct {
	Pantheons []string
	Roles     []string
	// Pantheon => Role => GodID => God
	//FIXME: Order gods
	Assoc             map[string]map[string]gods.Gods
	ResourceQualifierCSS string
	ResourceQualifierJS string
}

type IntSet map[int]struct{}
type StringSet map[string]struct{}

func CreateTemplateData(godsList gods.Gods) TemplateData {
	rolesUnique := make(StringSet)
	godData := make(map[string]map[string]gods.Gods)
	// Unique check
	GodIds := make(IntSet)
	for _, god := range godsList {
		_, ok := GodIds[god.ID]
		if ok {
			log.Fatalf("God with ID %d exists more than once; %s", god.ID, god.Name)
		}

		godRole := strings.TrimSpace(god.Roles)
		if len(godRole) == 0 {
			log.Println("WARN: God with no role: ", god.Name)
			continue
		}

		GodIds[god.ID] = struct{}{}

		godPantheon := strings.TrimSpace(god.Pantheon)
		if godData[godPantheon] == nil {
			godData[godPantheon] = make(map[string]gods.Gods)
		}

		rolesUnique[godRole] = struct{}{}
		godData[godPantheon][godRole] = append(godData[godPantheon][godRole], god)
	}

	// Now we have unique pantheons, roles, god IDs, and can get gods by ID

	var pantheons []string
	for pantheon, _ := range godData {
		pantheons = append(pantheons, pantheon)
	}
	sort.Strings(pantheons)

	var roles []string
	for role, _ := range rolesUnique {
		roles = append(roles, role)
	}
	sort.Strings(roles)

	for _, data1 := range godData {
		for _, gods := range data1 {
			sort.Sort(gods)
		}
	}

	fCSS, err := os.Stat("smitegods.css")
	if (err != nil) {
		panic("Failed to get CSS file information")
	}
	resourceQualifierCSS := fCSS.ModTime().UTC().Format("20060102150405")
	fJS, err := os.Stat("smitegods.js")
	if (err != nil) {
		panic("Failed to get JS file information")
	} 
	resourceQualifierJS := fJS.ModTime().UTC().Format("20060102150405")

	return TemplateData{pantheons, roles, godData, resourceQualifierCSS, resourceQualifierJS}
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

func ToCSSClass(str string) string {
	return strings.TrimSpace(strings.ToLower(str))
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

	funcMap := template.FuncMap{
		"ToCSSClass": ToCSSClass,
	}

	tmpl, err := template.New("gods.html").Funcs(funcMap).ParseGlob("templates/*.html")
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

	err = tmpl.Execute(writer, CreateTemplateData(godlist))
	if err != nil {
		log.Fatalln("Failed to execute HTML template with data: ", err)
	}

	log.Println("DONE")

	//	for _, god := range gods {
	//		fmt.Println(god.Name, god.Pantheon, god.Roles, god.Type, god.OnFreeRotation, god.GodIconURL, god.GodCardURL)
	//	}
	//	httpListen()
}
