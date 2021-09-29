package divestage

import "github.com/Steelstone3/Bubbles-Dive-Planner-Console/presenters"

type DiveStep struct {
	Depth uint
	Time  uint
}

func ConstructDiveStep() DiveStep {
	depth := presenters.GetUintFromConsole("\nEnter depth:")
	time := presenters.GetUintFromConsole("Enter time:")

	return verifyDiveStep(depth, time)
}

func verifyDiveStep(depth uint, time uint) DiveStep {
	if depth > 100 {
		depth = 100
	} else if depth == 0 {
		depth = 1
	}

	if time > 60 {
		time = 60
	} else if time == 0 {
		time = 1
	}

	return DiveStep{
		Depth: depth,
		Time:  time,
	}
}
