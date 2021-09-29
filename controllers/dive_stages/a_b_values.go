package divestages

import (
	divemodels "github.com/Steelstone3/Bubbles-Dive-Planner-Console/models/dive_models"
)

func calculateAValue(compartment uint, diveModel divemodels.DiveModel) float64 {
	return (diveModel.NitrogenAValues[compartment]*diveModel.DiveProfile.NitrogenTissuePressures[compartment] + diveModel.HeliumAValues[compartment]*diveModel.DiveProfile.HeliumTissuePressures[compartment]) / diveModel.DiveProfile.TotalTissuePressures[compartment]
}

func calculateBValue(compartment uint, diveModel divemodels.DiveModel) float64 {
	return (diveModel.NitrogenBValues[compartment]*diveModel.DiveProfile.NitrogenTissuePressures[compartment] + diveModel.HeliumBValues[compartment]*diveModel.DiveProfile.HeliumTissuePressures[compartment]) / diveModel.DiveProfile.TotalTissuePressures[compartment]
}
