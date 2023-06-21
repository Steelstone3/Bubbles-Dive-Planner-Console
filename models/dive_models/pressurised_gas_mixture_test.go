package divemodels

import (
	"testing"

	"github.com/stretchr/testify/assert"
)

func TestConstructPressurisedGasMixture(t *testing.T) {
	// Given
	pressurisedGasMixture := PressurisedGasMixture{
		Oxygen:   0,
		Helium:   0,
		Nitrogen: 0,
	}

	// When
	actualPressurisedGasMixture := ConstructPressurisedGasMixture()

	// Then
	assert.Equal(t, pressurisedGasMixture, actualPressurisedGasMixture)
}
