package apidata

import (
	"crypto/md5"
	"encoding/hex"
	"encoding/json"
	"fmt"
	"io"
	"io/ioutil"
	"log"
	"net/http"
	"strings"
	"time"
)

const (
	EndpointPC   = "http://api.smitegame.com/smiteapi.svc"
	EndpointXbox = "http://api.xbox.smitegame.com/smiteapi.svc"
	EndpointPS4  = "http://api.ps4.smitegame.com/smiteapi.svc"
	TimeFormat   = "20060102150405"
)

const (
	LANG_EN   = "1"
	LANG_DE   = "2"
	LANG_FR   = "3"
	LANG_ES   = "7"
	LANG_ESLA = "9"
	LANG_PT   = "10"
	LANG_RU   = "11"
	LANG_PL   = "12"
	LANG_TR   = "13"
)

type Format struct {
	PathData string
}

func CreateFormatJson() Format {
	return Format{"json"}
}
func CreateFormatXml() Format {
	return Format{"xml"}
}

func (f *Format) ToString() string {
	return f.PathData
}

const pathSessionCreate = "/createsession"
const pathPing = "/ping"

type SessionReturn struct {
	// "Approved"
	// ret_msg
	Message   string `json:"ret_msg"`
	SessionID string `json:"session_id"`
	Timestamp string `json:"timestamp"`
}

type Session struct {
	Endpoint  string
	DevID     int
	AuthKey   string
	Format    Format
	sessionID string
}

func NewSession(Endpoint string, developerID int, authKey string, Format Format) Session {
	return Session{Endpoint, developerID, authKey, Format, ""}
}

func (s *Session) getFormattedTime() string {
	return time.Now().UTC().Format(TimeFormat)
}

func (s *Session) hash(methodname string, timeUtc string) string {
	// DevID + methodname + AuthKey + timestampUTC
	fieldConcat := fmt.Sprintf("%d%s%s%s", s.DevID, methodname, s.AuthKey, timeUtc)
	md5 := md5.Sum([]byte(fieldConcat))
	return strings.ToLower(hex.EncodeToString(md5[:]))
}

// {developerId}/{signature}/{timestamp}
func (s *Session) createPathPartNoSession(methodname string) string {
	timeUtc := s.getFormattedTime()
	return fmt.Sprintf("%d/%s/%s", s.DevID, s.hash(methodname, timeUtc), timeUtc)
}

// {developerId}/{signature}/{session}/{timestamp}
func (s *Session) createPathPart(methodname string) string {
	if s.sessionID == "" {
		log.Fatal("Method ", methodname, " called without valid session")
	}
	timeUtc := s.getFormattedTime()
	return fmt.Sprintf("%d/%s/%s/%s", s.DevID, s.hash(methodname, timeUtc), s.sessionID, timeUtc)
}

func (s *Session) call(methodname string) string {
	url := fmt.Sprintf("%s/%s%s", s.Endpoint, methodname, s.Format.ToString())
	return s.remoteCallToString(url)
}

func (s *Session) call2ToString(methodname string, part string) string {
	url := fmt.Sprintf("%s/%s%s/%s", s.Endpoint, methodname, s.Format.ToString(), part)
	return s.remoteCallToString(url)
}

func (s *Session) call2ToStringParamed(methodname string, part string, params string) string {
	url := fmt.Sprintf("%s/%s%s/%s%s", s.Endpoint, methodname, s.Format.ToString(), part, params)
	return s.remoteCallToString(url)
}

func (s *Session) call2(methodname string, part string) io.ReadCloser {
	url := fmt.Sprintf("%s/%s%s/%s", s.Endpoint, methodname, s.Format.ToString(), part)
	return s.remoteCall(url)
}

func (s *Session) remoteCallToString(path string) string {
	body := s.remoteCall(path)
	defer body.Close()
	str, _ := ioutil.ReadAll(body)
	return fmt.Sprintf("%s", str)
}

func (s *Session) remoteCall(path string) io.ReadCloser {
	log.Println("Request to", path)
	resp, err := http.Get(path)
	if err != nil {
		log.Fatal(err)
	}
	if resp.StatusCode != 200 {
		log.Fatal("Response code != OK: ", resp.Status)
	}
	return resp.Body
}

func (s *Session) Ping() string {
	return s.call("ping")
}

// /createsession[ResponseFormat]/{developerId}/{signature}/{timestamp}
// A required step to Authenticate the developerId/signature for further API use.
func (s *Session) Connect() {
	method := "createsession"
	body := s.call2(method, s.createPathPartNoSession(method))

	defer body.Close()

	str, _ := ioutil.ReadAll(body)
	log.Println("resp: ", fmt.Sprintf("%s", str))

	//	decoder := json.NewDecoder(body)
	var data SessionReturn
	//	err := decoder.Decode(&data)
	err := json.Unmarshal([]byte(str), &data)
	if err != nil && err != io.EOF {
		log.Fatal("Failed to parse sessioncreate response: ", err)
	}
	if data.Message != "Approved" {
		log.Fatal("Failed to create session. Server returned error: ", data.Message)
	}
	s.sessionID = data.SessionID
	log.Println("Created session ", s.sessionID)
}

// /testsession[ResponseFormat]/{developerId}/{signature}/{session}/{timestamp}
// A means of validating that a session is established.
func (s *Session) TestSession() string {
	method := "testsession"
	return s.call2ToString(method, s.createPathPart(method))
}

func (s *Session) GetGods() string {
	method := "getgods"
	return s.call2ToStringParamed(method, s.createPathPart(method), "/"+LANG_EN)
}
