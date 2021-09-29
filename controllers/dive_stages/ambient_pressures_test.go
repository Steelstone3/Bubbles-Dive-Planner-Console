package divestages

import (
	"testing"

	divemodels "github.com/Steelstone3/Bubbles-Dive-Planner-Console/models/dive_models"
	divestage "github.com/Steelstone3/Bubbles-Dive-Planner-Console/models/dive_stage"
	"github.com/stretchr/testify/assert"
)

func TestCalculateAmbientPressure(t *testing.T) {
	// Given
	delta := 0.01
	expectedPressurisedGasMixture := divemodels.PressurisedGasMixture{
		Oxygen:   1.26,
		Helium:   0.6,
		Nitrogen: 4.14,
	}
	diveStep := divestage.DiveStep{
		Depth: 50,
		Time:  10,
	}
	gasMixture := divestage.GasMixture{
		Oxygen:   21,
		Helium:   10,
		Nitrogen: 69,
	}
	diveProfile := divemodels.DiveProfile{
		PressurisedGasMixture: divemodels.PressurisedGasMixture{
			Oxygen:   0,
			Helium:   0,
			Nitrogen: 0,
		},
	}

	// When
	diveProfile = calculateAmbientPressure(diveProfile, diveStep, gasMixture)

	// Then
	assert.InDelta(t, expectedPressurisedGasMixture.Oxygen, diveProfile.PressurisedGasMixture.Oxygen, delta)
	assert.InDelta(t, expectedPressurisedGasMixture.Helium, diveProfile.PressurisedGasMixture.Helium, delta)
	assert.InDelta(t, expectedPressurisedGasMixture.Nitrogen, diveProfile.PressurisedGasMixture.Nitrogen, delta)
}
