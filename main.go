package main

import (
	divestages "github.com/Steelstone3/Bubbles-Dive-Planner-Console/controllers/dive_stages"
	divemodels "github.com/Steelstone3/Bubbles-Dive-Planner-Console/models/dive_models"
)

func main() {
	diveStage := divemodels.SetupDiveStage()

	for {
		diveStage.SetupDiveStep()
		diveStage.DiveModel.DiveProfile = divestages.RunDiveStages(diveStage.DiveModel, diveStage.DiveStep, diveStage.SelectedCylinder)
		diveStage.DiveModel.DiveProfile.DisplayDiveProfile()
	}
}
