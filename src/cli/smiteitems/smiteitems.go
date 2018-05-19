package main

import (
	"bufio"
	"encoding/json"
	"flag"
	"html/template"
	"io/ioutil"
	"log"
	"os"

	"../../apidata/types"
)

const (
	fileItems = "data/items.json"
)

var generate = flag.Bool("generate", false, "Generate HTML file")
var outFile = flag.String("outfile", "smiteitems.html", "Generated output file")

func parseItems() (types.Items, error) {
	itemsJSON, err := ioutil.ReadFile(fileItems)
	if err != nil {
		return nil, err
	}

	var items types.Items
	err = json.Unmarshal(itemsJSON, &items)
	if err != nil {
		return nil, err
	}

	return items, nil
}

// TemplateData contains the data used in the HTML templates to generate the resulting HTML
type TemplateData struct {
	ResourceQualifierCSS string
	ResourceQualifierJS  string
	Items                []types.Items
}

func main() {
	flag.Parse()

	if *generate {
		items, err := parseItems()
		if err != nil {
			log.Fatalln("Failed to parse items", err)
		}

		// log.Println(len(items))
		// for _, i := range items {
		// 	log.Println(i.DeviceName)
		// }
		// .ItemId .RootItemId, .ItemTier, .ChildItemId

		tmpl, err := template.New("items.html").ParseFiles("templates/items.html", "templates/item.html")
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

		err = tmpl.Execute(writer, items)
		if err != nil {
			log.Fatalln("Failed to execute HTML template with data: ", err)
		}

	}
}
