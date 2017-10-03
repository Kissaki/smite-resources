package main

type PlayerStatus []struct {
	Match                 int64       `json:"Match"`
	PersonalStatusMessage interface{} `json:"personal_status_message"`
	RetMsg                interface{} `json:"ret_msg"`
	Status                int64       `json:"status"`
	StatusString          string      `json:"status_string"`
}
