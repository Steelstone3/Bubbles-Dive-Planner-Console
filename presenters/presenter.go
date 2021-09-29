package presenters

import (
	"bufio"
	"fmt"
	"os"
	"strconv"
	"strings"
)

func GetUintFromConsole(prompt string) uint {
	text := GetStringFromConsole(prompt)
	num, err := strconv.ParseUint(text, 10, 64)
	if err != nil {
		return 0
	}
	return uint(num)
}

func GetConfirmationFromConsole(question string) bool {
	input := GetStringFromConsole(question)

	if input == "y" || input == "Y" || input == "Yes" || input == "yes" {
		return true
	} else {
		return false
	}
}

func GetStringFromConsole(prompt string) string {
	reader := bufio.NewReader(os.Stdin)
	Print(prompt)
	text, _ := reader.ReadString('\n')
	text = strings.TrimSpace(text)
	return text
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
