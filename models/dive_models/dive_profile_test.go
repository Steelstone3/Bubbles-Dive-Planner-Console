package divemodels

import (
	"testing"

	"github.com/stretchr/testify/assert"
)

func TestConstructDiveProfile(t *testing.T) {
	// Given
	compartmentCount := uint(9)
	diveProfile := DiveProfile{
		NitrogenTissuePressures:   make([]float64, compartmentCount),
		HeliumTissuePressures:     make([]float64, compartmentCount),
		TotalTissuePressures:      make([]float64, compartmentCount),
		ToleratedAmbientPressures: make([]float64, compartmentCount),
		AValues:                   make([]float64, compartmentCount),
		BValues:                   make([]float64, compartmentCount),
		MaximumSurfacePressures:   make([]float64, compartmentCount),
		CompartmentLoads:          make([]float64, compartmentCount),
		PressurisedGasMixture: PressurisedGasMixture{
			Oxygen:   0,
			Helium:   0,
			Nitrogen: 0,
		},
	}

	// When
	actualDiveProfile := ConstructDiveProfile(compartmentCount)

	// Then
	assert.Equal(t, diveProfile, actualDiveProfile)
}
