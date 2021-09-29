package divestages

import (
	"math"

	diveProfile "github.com/Steelstone3/Bubbles-Dive-Planner-Console/models/dive_models"
	divestage "github.com/Steelstone3/Bubbles-Dive-Planner-Console/models/dive_stage"
)

func calculateTotalTissuePressure(compartment uint, diveProfile diveProfile.DiveProfile) float64 {
	return diveProfile.HeliumTissuePressures[compartment] + diveProfile.NitrogenTissuePressures[compartment]
}

func calculateNitrogenTissuePressure(compartment uint, diveModel diveProfile.DiveModel, diveStep divestage.DiveStep) float64 {
	return diveModel.DiveProfile.NitrogenTissuePressures[compartment] + ((diveModel.DiveProfile.PressurisedGasMixture.Nitrogen - diveModel.DiveProfile.NitrogenTissuePressures[compartment]) * (1.0 - math.Pow(2.0, -float64(float64(diveStep.Time)/diveModel.NitrogenHalfTimes[compartment]))))
}

func calculateHeliumTissuePressure(compartment uint, diveModel diveProfile.DiveModel, diveStep divestage.DiveStep) float64 {
	return diveModel.DiveProfile.HeliumTissuePressures[compartment] + ((diveModel.DiveProfile.PressurisedGasMixture.Helium - diveModel.DiveProfile.HeliumTissuePressures[compartment]) * (1.0 - math.Pow(2.0, -float64(float64(diveStep.Time)/diveModel.HeliumHalfTimes[compartment]))))
}
