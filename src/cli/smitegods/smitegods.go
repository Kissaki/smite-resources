package main

import (
	"bufio"
	"flag"
	"fmt"
	"hash/adler32"
	"html/template"
	"io/ioutil"
	"log"
	"net/http"
	"os"
	"sort"
	"strings"

	"../../apidata"
	"../../apidata/gods"
	"../common/godhandling"
)

const (
	fileGods      = "data/gods.json"
)

var updateGodData = flag.Bool("updategoddata", false, "Specifies whether the God data should be updated form the SMITE API. This requires specifying the developer ID and authentication key.")
var developerID = flag.Int("devid", 0, "developer ID given by Hi-Rez")
var authKey = flag.String("authkey", "", "authentication key given by Hi-Rez")
var outFile = flag.String("target", "smitegods.html", "Filename that the HTML god overview is written to.")

// Default Request Handler
func defaultHandler(w http.ResponseWriter, r *http.Request) {
	fmt.Fprintf(w, "<h1>Hello %s!</h1>", r.URL.Path[1:])
}

func APITestAndGodsDownload() {
	s := apidata.NewSession(apidata.EndpointPC, *developerID, *authKey, apidata.CreateFormatJson())
	log.Println(s.Ping())
	s.Connect()
	log.Println(s.TestSession())

	godsNew := s.GetGods()

	godsOld, err := ioutil.ReadFile(fileGods)
	if err != nil || adler32.Checksum(godsOld) != adler32.Checksum([]byte(godsNew)) {
		fmt.Println("Writing new god data to gods.json")
		ioutil.WriteFile(fileGods, []byte(godsNew), 0666)
	} else {
		fmt.Println("Pulled god data is not different from the stored data.")
	}
}

func httpListen() {
	http.HandleFunc("/", defaultHandler)
	http.ListenAndServe(":8080", nil)
}

// TemplateData contains the data used in the HTML templates to generate the resulting HTML
type TemplateData struct {
	Pantheons []string
	Roles     []string
	// Pantheon => Role => GodID => God
	//FIXME: Order gods
	Assoc                map[string]map[string]gods.Gods
	ResourceQualifierCSS string
	ResourceQualifierJS  string
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
	if err != nil {
		panic("Failed to get CSS file information")
	}
	resourceQualifierCSS := fCSS.ModTime().UTC().Format("20060102150405")
	fJS, err := os.Stat("smitegods.js")
	if err != nil {
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

	if *updateGodData && (*developerID == 0 || *authKey == "") {
		fmt.Println("invalid or unspecified parameters")
		flag.Usage()
		fmt.Println("#" + flag.Arg(0))
		log.Fatalln("FATAL: invalid or unspecified parameters")
	}

	return nil
}

func ToCSSClass(str string) string {
	return strings.ReplaceAll(strings.TrimSpace(strings.ToLower(str)), " ", "")
}

func main() {
	if initFlags() != nil {
		os.Exit(1)
	}

	if *updateGodData {
		APITestAndGodsDownload()
	}

	godlist, err := godhandling.ParseGods(fileGods)
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
	file, err := os.Create(*outFile)
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
}
