package godhandling

import (
	"encoding/json"
	"io/ioutil"

	"../../../apidata/gods"
)

// ParseGods parses the god data in fileGods and returns typed god data
func ParseGods(fileGods string) (gods.Gods, error) {
	godsJSON, err := ioutil.ReadFile(fileGods)
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
