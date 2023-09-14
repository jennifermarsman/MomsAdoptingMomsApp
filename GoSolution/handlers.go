package main

import (
	"bufio"
	"database/sql"
	"encoding/json"
	"errors"
	"fmt"
	"net/http"
	"os"
	"strings"

	"github.com/lib/pq"
	_ "github.com/lib/pq"
	"golang.org/x/crypto/bcrypt"
)

// Create a struct that models the structure of a user, both in the request body, and in the DB
type Credentials struct {
	Password string `json:"password", db:"password"`
	Username string `json:"username", db:"username"`
}

func Signup(w http.ResponseWriter, r *http.Request) {
	// Parse and decode the request body into a new `Credentials` instance
	creds := &Credentials{}
	err := json.NewDecoder(r.Body).Decode(creds)
	if err != nil {
		// If there is something wrong with the request body, return a 400 status
		w.WriteHeader(http.StatusBadRequest)
		return
	}
	// Salt and hash the password using the bcrypt algorithm
	// The second argument is the cost of hashing, which we arbitrarily set as 8 (this value can be more or less, depending on the computing power you wish to utilize)
	hashedPassword, err := bcrypt.GenerateFromPassword([]byte(creds.Password), 8)

	// Next, insert the username, along with the hashed password into the database
	if _, err = db.Query("insert into users values ($1, $2)", creds.Username, string(hashedPassword)); err != nil {
		// If there is any issue with inserting into the database, return a 500 error
		w.WriteHeader(http.StatusInternalServerError)
		return
	}
	// We reach this point if the credentials we correctly stored in the database, and the default status of 200 is sent back
}

func Signin(w http.ResponseWriter, r *http.Request) {
	// Parse and decode the request body into a new `Credentials` instance
	creds := &Credentials{}
	err := json.NewDecoder(r.Body).Decode(creds)
	if err != nil {
		// If there is something wrong with the request body, return a 400 status
		w.WriteHeader(http.StatusBadRequest)
		return
	}
	// Get the existing entry present in the database for the given username
	result := db.QueryRow("select password from users where username=$1", creds.Username)
	if err != nil {
		// If there is an issue with the database, return a 500 error
		w.WriteHeader(http.StatusInternalServerError)
		return
	}
	// We create another instance of `Credentials` to store the credentials we get from the database
	storedCreds := &Credentials{}
	// Store the obtained password in `storedCreds`
	err = result.Scan(&storedCreds.Password)
	if err != nil {
		// If an entry with the username does not exist, send an "Unauthorized"(401) status
		if err == sql.ErrNoRows {
			w.WriteHeader(http.StatusUnauthorized)
			return
		}
		// If the error is of any other type, send a 500 status
		w.WriteHeader(http.StatusInternalServerError)
		return
	}

	// Compare the stored hashed password, with the hashed version of the password that was received
	if err = bcrypt.CompareHashAndPassword([]byte(storedCreds.Password), []byte(creds.Password)); err != nil {
		// If the two passwords don't match, return a 401 status
		w.WriteHeader(http.StatusUnauthorized)
	}

	// If we reach this point, that means the users password was correct, and that they are authorized
	// The default 200 status is sent

	AnswerQuestionsIfEmpty(w, creds.Username)
	DetermineWorkflow(creds.Username)
}

func AnswerQuestionsIfEmpty(w http.ResponseWriter, username string) {
	// Check if questions in database have been answered
	// If not, send a question to the user
	rows, err := db.Query("select * from questionnaire where username=$1", username)
	if err != nil {
		// If there is an issue with the database, return a 500 error
		fmt.Println("Error checking questions: ", err.Error())
		return
	}
	if rows == nil || !rows.Next() {
		fmt.Println("No questions have been answered. Answer them now!")
		qrows, err := db.Query("select * from questions")
		if err != nil {
			fmt.Println("Error getting questions: ", err.Error())
		}

		for qrows.Next() {
			fmt.Println("@@@@@ Next question")
			var qno int
			var question string
			err = qrows.Scan(&qno, &question)
			if err != nil {
				fmt.Println("Error scanning questions: ", err.Error())
			}
			fmt.Println("Question: ", question)
			var answer string
			for {
				scanner := bufio.NewScanner(os.Stdin)
				scanner.Scan()
				answer = scanner.Text()
				if answer != "" {
					break
				}
			}
			// Save to db
			_, err = db.Query("insert into questionnaire (username_qno, qno, username, answer) values ($1, $2, $3, $4)", username+string(qno), qno, username, answer)
			if err != nil {
				fmt.Println("Error saving answers: ", err.Error())
			}
			fmt.Println("@@@@ Saved answer")
		}
		return
	}
}

func DetermineWorkflow(username string) {
	fmt.Println("Determining workflow for user: ", username)
	workflow := []string{}
	if username == "" {
		fmt.Println("Username is empty")
		return
	}

	// Check if the user has children in foster care
	answer, err := ReadAnswerToQuestion(username, 4)
	if err != nil {
		fmt.Println("Error reading question 4: ", err.Error())
		return
	}
	if strings.EqualFold(answer, "no") {
		fmt.Println("User has no children in foster care")
		return
	}

	// Check if the user is on substance abuse
	answer, err = ReadAnswerToQuestion(username, 7)
	if err != nil {
		fmt.Println("Error reading question 4: ", err.Error())
		return
	}

	if strings.EqualFold(answer, "yes") {
		workflow = append(workflow, "StepA")
		fmt.Println("User is on substance abuse. Need StepA")
	}

	// Check if the user has transportation
	answer, err = ReadAnswerToQuestion(username, 7)
	if err != nil {
		fmt.Println("Error reading question 4: ", err.Error())
		return
	}

	answer = strings.ToLower(answer)
	if strings.Contains(answer, "friend") || (!strings.Contains(answer, "car") && !strings.Contains(answer, "truck")) {
		workflow = append(workflow, "StepB")
		fmt.Println("User does not have car/truck transportation. Need StepB")
	}

	// Everyone needs StepC
	workflow = append(workflow, "StepC")
	fmt.Println("User needs StepC")

	// Everyone needs StepD
	workflow = append(workflow, "StepD")
	fmt.Println("User needs StepD")

	// Create workflow for user
	_, err = db.Query("insert into workflow (username, steps) values ($1, $2)", username, pq.Array(workflow))
	if err != nil {
		fmt.Println("Error saving workflow: ", err.Error())
	}
}

func ReadAnswerToQuestion(username string, qno int) (string, error) {
	qRows, err := db.Query("select answer from questionnaire where username=$1 and qno=$2", username, qno)
	if err != nil {
		fmt.Println("Error getting questions: ", err.Error())
	}

	if qRows == nil || !qRows.Next() {
		fmt.Println("Erro reading questionnaire")
		return "", errors.New("Error reading questionnaire")
	}

	var answer string

	if qRows.Scan(&answer) != nil {
		fmt.Println("Error scanning questionnaire")
		return "", errors.New("Error scanning questionnaire")
	}
	return answer, nil
}
