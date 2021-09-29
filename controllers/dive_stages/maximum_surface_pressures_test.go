package divestages

import (
	"testing"

	divemodels "github.com/Steelstone3/Bubbles-Dive-Planner-Console/models/dive_models"
	"github.com/stretchr/testify/assert"
)

func TestCalculateMaximumSurfacePressure(t *testing.T) {
	// Given
	compartmentCount := uint(16)
	diveProfile := divemodels.DiveProfile{
		MaximumSurfacePressures: make([]float64, compartmentCount),
		AValues: []float64{
			1.328,
			1.070,
			0.930,
			0.822,
			0.728,
			0.625,
			0.555,
			0.503,
			0.466,
			0.427,
			0.399,
			0.376,
			0.349,
			0.341,
			0.326,
			0.309,
		},
		BValues: []float64{
			0.493,
			0.637,
			0.708,
			0.769,
			0.800,
			0.831,
			0.859,
			0.882,
			0.900,
			0.914,
			0.923,
			0.931,
			0.938,
			0.944,
			0.949,
			0.955,
		},
	}

	// When
	for compartment := uint(0); compartment < compartmentCount; compartment++ {
		diveProfile.MaximumSurfacePressures[compartment] = calculateMaximumSurfacePressure(compartment, diveProfile)
	}

	// Then
	assert.InDelta(t, 3.356, diveProfile.MaximumSurfacePressures[0], 0.01)
	assert.InDelta(t, 2.640, diveProfile.MaximumSurfacePressures[1], 0.01)
	assert.InDelta(t, 2.342, diveProfile.MaximumSurfacePressures[2], 0.01)
	assert.InDelta(t, 2.122, diveProfile.MaximumSurfacePressures[3], 0.01)
	assert.InDelta(t, 1.978, diveProfile.MaximumSurfacePressures[4], 0.01)
	assert.InDelta(t, 1.828, diveProfile.MaximumSurfacePressures[5], 0.01)
	assert.InDelta(t, 1.719, diveProfile.MaximumSurfacePressures[6], 0.01)
	assert.InDelta(t, 1.637, diveProfile.MaximumSurfacePressures[7], 0.01)
	assert.InDelta(t, 1.577, diveProfile.MaximumSurfacePressures[8], 0.01)
	assert.InDelta(t, 1.521, diveProfile.MaximumSurfacePressures[9], 0.01)
	assert.InDelta(t, 1.482, diveProfile.MaximumSurfacePressures[10], 0.01)
	assert.InDelta(t, 1.450, diveProfile.MaximumSurfacePressures[11], 0.01)
	assert.InDelta(t, 1.415, diveProfile.MaximumSurfacePressures[12], 0.01)
	assert.InDelta(t, 1.400, diveProfile.MaximumSurfacePressures[13], 0.01)
	assert.InDelta(t, 1.380, diveProfile.MaximumSurfacePressures[14], 0.01)
	assert.InDelta(t, 1.356, diveProfile.MaximumSurfacePressures[15], 0.01)
}
