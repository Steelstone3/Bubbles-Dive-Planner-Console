package divestages

import divemodels "github.com/Steelstone3/Bubbles-Dive-Planner-Console/models/dive_models"

func calculateCompartmentLoad(compartment uint, diveProfile divemodels.DiveProfile) float64 {
	return diveProfile.TotalTissuePressures[compartment] / diveProfile.MaximumSurfacePressures[compartment] * 100.0
}
