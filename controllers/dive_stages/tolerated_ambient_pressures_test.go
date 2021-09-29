package divestages

import (
	"testing"

	divemodels "github.com/Steelstone3/Bubbles-Dive-Planner-Console/models/dive_models"
	"github.com/stretchr/testify/assert"
)

func TestCalculateToleratedAmbientPressure(t *testing.T) {
	// Given
	compartmentCount := uint(16)
	diveProfile := divemodels.DiveProfile{
		ToleratedAmbientPressures: make([]float64, compartmentCount),
		TotalTissuePressures: []float64{
			4.002,
			2.939,
			2.224,
			1.671,
			1.233,
			0.913,
			0.668,
			0.483,
			0.348,
			0.263,
			0.207,
			0.162,
			0.128,
			0.101,
			0.079,
			0.062,
		},
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
		diveProfile.ToleratedAmbientPressures[compartment] = calculateToleratedAmbientPressure(compartment, diveProfile)
	}

	// Then
	assert.InDelta(t, 1.318, diveProfile.ToleratedAmbientPressures[0], 0.01)
	assert.InDelta(t, 1.191, diveProfile.ToleratedAmbientPressures[1], 0.01)
	assert.InDelta(t, 0.916, diveProfile.ToleratedAmbientPressures[2], 0.01)
	assert.InDelta(t, 0.653, diveProfile.ToleratedAmbientPressures[3], 0.01)
	assert.InDelta(t, 0.404, diveProfile.ToleratedAmbientPressures[4], 0.01)
	assert.InDelta(t, 0.239, diveProfile.ToleratedAmbientPressures[5], 0.01)
	assert.InDelta(t, 0.097, diveProfile.ToleratedAmbientPressures[6], 0.01)
	assert.InDelta(t, -0.018, diveProfile.ToleratedAmbientPressures[7], 0.01)
	assert.InDelta(t, -0.106, diveProfile.ToleratedAmbientPressures[8], 0.01)
	assert.InDelta(t, -0.150, diveProfile.ToleratedAmbientPressures[9], 0.01)
	assert.InDelta(t, -0.177, diveProfile.ToleratedAmbientPressures[10], 0.01)
	assert.InDelta(t, -0.199, diveProfile.ToleratedAmbientPressures[11], 0.01)
	assert.InDelta(t, -0.207, diveProfile.ToleratedAmbientPressures[12], 0.01)
	assert.InDelta(t, -0.227, diveProfile.ToleratedAmbientPressures[13], 0.01)
	assert.InDelta(t, -0.234, diveProfile.ToleratedAmbientPressures[14], 0.01)
	assert.InDelta(t, -0.236, diveProfile.ToleratedAmbientPressures[15], 0.01)
}
