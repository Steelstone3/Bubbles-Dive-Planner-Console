package divestages

import divemodels "github.com/Steelstone3/Bubbles-Dive-Planner-Console/models/dive_models"

func calculateMaximumSurfacePressure(compartment uint, diveProfile divemodels.DiveProfile) float64 {
	return (1.0 / diveProfile.BValues[compartment]) + diveProfile.AValues[compartment]
}
