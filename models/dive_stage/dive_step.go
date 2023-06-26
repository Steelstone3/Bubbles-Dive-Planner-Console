package divestage

import "github.com/Steelstone3/Bubbles-Dive-Planner-Console/presenters"

type DiveStep struct {
	Depth uint
	Time  uint
}

func ConstructDiveStep() DiveStep {
	return DiveStep{
		Depth: presenters.GetUintFromConsoleInRange("\nEnter depth:", 1, 100),
		Time:  presenters.GetUintFromConsoleInRange("Enter time:", 1, 60),
	}
}
