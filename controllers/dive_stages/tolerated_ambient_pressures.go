package divestages

import divemodels "github.com/Steelstone3/Bubbles-Dive-Planner-Console/models/dive_models"

func calculateToleratedAmbientPressure(compartment uint, diveProfile divemodels.DiveProfile) float64 {
	return (diveProfile.TotalTissuePressures[compartment] - diveProfile.AValues[compartment]) * diveProfile.BValues[compartment]
}
