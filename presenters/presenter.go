package presenters

import (
	"bufio"
	"fmt"
	"os"
	"strconv"
	"strings"
)

func GetUintFromConsole(prompt string) uint {
	text := getStringFromConsole(prompt)
	number, err := strconv.ParseUint(text, 10, 64)
	if err != nil {
		return 0
	}
	return uint(number)
}

func GetUintFromConsoleInRange(prompt string, minimum uint, maximum uint) uint {
	for {
		text := getStringFromConsole(prompt)
		number, err := strconv.ParseUint(text, 10, 64)
		if err != nil || uint(number) < minimum || uint(number) > maximum {
			errorMessage := "Please enter a number between {Minimum} and {Maximum}\n"
			errorMessage = UintStringReplace(errorMessage, "{Minimum}", minimum)
			errorMessage = UintStringReplace(errorMessage, "{Maximum}", maximum)

			Print(errorMessage)
			continue
		}
		return uint(number)
	}
}

func GetConfirmationFromConsole(question string) bool {
	input := getStringFromConsole(question)

	if input == "y" || input == "Y" || input == "Yes" || input == "yes" {
		return true
	} else {
		return false
	}
}

func GetValidStringFromConsole(prompt string) string {
	for {
		text := getStringFromConsole(prompt)
		if text != "" {
			return text
		}
		Print("Input cannot be null or empty. Please try again.")
	}
}

func Float64StringReplace(outputString string, key string, value float64) string {
	return strings.Replace(outputString, key, fmt.Sprintf("%f", value), 1)
}

func UintStringReplace(outputString string, key string, value uint) string {
	return strings.Replace(outputString, key, fmt.Sprintf("%d", value), 1)
}

func StringReplace(outputString string, key string, value string) string {
	return strings.Replace(outputString, key, value, 1)
}

func Print(message string) {
	fmt.Print(message)
}

func getStringFromConsole(prompt string) string {
	reader := bufio.NewReader(os.Stdin)
	Print(prompt)
	text, _ := reader.ReadString('\n')
	text = strings.TrimSpace(text)
	return text
}
