package divestages

import (
	divemodels "github.com/Steelstone3/Bubbles-Dive-Planner-Console/models/dive_models"
	divestage "github.com/Steelstone3/Bubbles-Dive-Planner-Console/models/dive_stage"
)

func calculateAmbientPressure(diveProfile divemodels.DiveProfile, diveStep divestage.DiveStep, gasMixture divestage.GasMixture) divemodels.DiveProfile {
	ambientPressure := 1.0 + float64(diveStep.Depth)/10.0

	diveProfile.PressurisedGasMixture.Nitrogen = float64(gasMixture.Nitrogen) / 100.0 * ambientPressure
	diveProfile.PressurisedGasMixture.Oxygen = float64(gasMixture.Oxygen) / 100.0 * ambientPressure
	diveProfile.PressurisedGasMixture.Helium = float64(gasMixture.Helium) / 100.0 * ambientPressure

	return diveProfile
}
