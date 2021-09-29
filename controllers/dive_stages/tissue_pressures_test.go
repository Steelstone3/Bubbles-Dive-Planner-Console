package divestages

import (
	"testing"

	divemodels "github.com/Steelstone3/Bubbles-Dive-Planner-Console/models/dive_models"
	divestage "github.com/Steelstone3/Bubbles-Dive-Planner-Console/models/dive_stage"
	"github.com/stretchr/testify/assert"
)

func TestCalculateTotalTissuePressure(t *testing.T) {
	// Given
	compartmentCount := uint(16)
	diveProfile := divemodels.DiveProfile{
		NitrogenTissuePressures: []float64{
			3.408,
			2.399,
			1.762,
			1.294,
			0.937,
			0.685,
			0.496,
			0.356,
			0.255,
			0.192,
			0.151,
			0.118,
			0.093,
			0.073,
			0.057,
			0.045,
		},
		HeliumTissuePressures: []float64{
			0.594,
			0.540,
			0.462,
			0.377,
			0.296,
			0.228,
			0.172,
			0.127,
			0.093,
			0.071,
			0.056,
			0.044,
			0.035,
			0.028,
			0.022,
			0.017,
		},
	}

	// When
	for compartment := uint(0); compartment < compartmentCount; compartment++ {
		diveProfile.NitrogenTissuePressures[compartment] = calculateTotalTissuePressure(compartment, diveProfile)
	}

	// Then
	assert.InDelta(t, 4.002, diveProfile.NitrogenTissuePressures[0], 0.01)
	assert.InDelta(t, 2.939, diveProfile.NitrogenTissuePressures[1], 0.01)
	assert.InDelta(t, 2.224, diveProfile.NitrogenTissuePressures[2], 0.01)
	assert.InDelta(t, 1.671, diveProfile.NitrogenTissuePressures[3], 0.01)
	assert.InDelta(t, 1.233, diveProfile.NitrogenTissuePressures[4], 0.01)
	assert.InDelta(t, 0.913, diveProfile.NitrogenTissuePressures[5], 0.01)
	assert.InDelta(t, 0.668, diveProfile.NitrogenTissuePressures[6], 0.01)
	assert.InDelta(t, 0.483, diveProfile.NitrogenTissuePressures[7], 0.01)
	assert.InDelta(t, 0.348, diveProfile.NitrogenTissuePressures[8], 0.01)
	assert.InDelta(t, 0.263, diveProfile.NitrogenTissuePressures[9], 0.01)
	assert.InDelta(t, 0.207, diveProfile.NitrogenTissuePressures[10], 0.01)
	assert.InDelta(t, 0.162, diveProfile.NitrogenTissuePressures[11], 0.01)
	assert.InDelta(t, 0.128, diveProfile.NitrogenTissuePressures[12], 0.01)
	assert.InDelta(t, 0.101, diveProfile.NitrogenTissuePressures[13], 0.01)
	assert.InDelta(t, 0.079, diveProfile.NitrogenTissuePressures[14], 0.01)
	assert.InDelta(t, 0.062, diveProfile.NitrogenTissuePressures[15], 0.01)
}

func TestCalculateNitrogenTissuePressure(t *testing.T) {
	// Given
	compartmentCount := uint(16)
	diveProfile := divemodels.DiveProfile{
		NitrogenTissuePressures: make([]float64, compartmentCount),
		PressurisedGasMixture: divemodels.PressurisedGasMixture{
			Oxygen:   1.26,
			Helium:   0.6,
			Nitrogen: 4.14,
		},
	}
	diveModel := divemodels.DiveModel{
		NitrogenHalfTimes: []float64{
			4.0,
			8.0,
			12.5,
			18.5,
			27.0,
			38.3,
			54.3,
			77.0,
			109.0,
			146.0,
			187.0,
			239.0,
			305.0,
			390.0,
			498.0,
			635.0,
		},
		DiveProfile: diveProfile,
	}
	diveStep := divestage.DiveStep{
		Depth: 50,
		Time:  10,
	}

	// When
	for compartment := uint(0); compartment < compartmentCount; compartment++ {
		diveModel.DiveProfile.NitrogenTissuePressures[compartment] = calculateNitrogenTissuePressure(compartment, diveModel, diveStep)
	}

	// Then
	assert.InDelta(t, 3.408, diveModel.DiveProfile.NitrogenTissuePressures[0], 0.01)
	assert.InDelta(t, 2.399, diveModel.DiveProfile.NitrogenTissuePressures[1], 0.01)
	assert.InDelta(t, 1.762, diveModel.DiveProfile.NitrogenTissuePressures[2], 0.01)
	assert.InDelta(t, 1.294, diveModel.DiveProfile.NitrogenTissuePressures[3], 0.01)
	assert.InDelta(t, 0.937, diveModel.DiveProfile.NitrogenTissuePressures[4], 0.01)
	assert.InDelta(t, 0.685, diveModel.DiveProfile.NitrogenTissuePressures[5], 0.01)
	assert.InDelta(t, 0.496, diveModel.DiveProfile.NitrogenTissuePressures[6], 0.01)
	assert.InDelta(t, 0.356, diveModel.DiveProfile.NitrogenTissuePressures[7], 0.01)
	assert.InDelta(t, 0.255, diveModel.DiveProfile.NitrogenTissuePressures[8], 0.01)
	assert.InDelta(t, 0.192, diveModel.DiveProfile.NitrogenTissuePressures[9], 0.01)
	assert.InDelta(t, 0.151, diveModel.DiveProfile.NitrogenTissuePressures[10], 0.01)
	assert.InDelta(t, 0.118, diveModel.DiveProfile.NitrogenTissuePressures[11], 0.01)
	assert.InDelta(t, 0.093, diveModel.DiveProfile.NitrogenTissuePressures[12], 0.01)
	assert.InDelta(t, 0.073, diveModel.DiveProfile.NitrogenTissuePressures[13], 0.01)
	assert.InDelta(t, 0.057, diveModel.DiveProfile.NitrogenTissuePressures[14], 0.01)
	assert.InDelta(t, 0.045, diveModel.DiveProfile.NitrogenTissuePressures[15], 0.01)
}

func TestCalculateHeliumTissuePressure(t *testing.T) {
	// Given
	compartmentCount := uint(16)
	diveProfile := divemodels.DiveProfile{
		HeliumTissuePressures: make([]float64, compartmentCount),
		PressurisedGasMixture: divemodels.PressurisedGasMixture{
			Oxygen:   1.26,
			Helium:   0.6,
			Nitrogen: 4.14,
		},
	}
	diveModel := divemodels.DiveModel{
		HeliumHalfTimes: []float64{
			1.51,
			3.02,
			4.72,
			6.99,
			10.21,
			14.48,
			20.53,
			29.11,
			41.20,
			55.19,
			70.69,
			90.34,
			115.29,
			147.42,
			188.24,
			240.03,
		},
		DiveProfile: diveProfile,
	}
	diveStep := divestage.DiveStep{
		Depth: 50,
		Time:  10,
	}

	// When
	for compartment := uint(0); compartment < compartmentCount; compartment++ {
		diveModel.DiveProfile.HeliumTissuePressures[compartment] = calculateHeliumTissuePressure(compartment, diveModel, diveStep)
	}

	// Then
	assert.InDelta(t, 0.594, diveModel.DiveProfile.HeliumTissuePressures[0], 0.01)
	assert.InDelta(t, 0.540, diveModel.DiveProfile.HeliumTissuePressures[1], 0.01)
	assert.InDelta(t, 0.462, diveModel.DiveProfile.HeliumTissuePressures[2], 0.01)
	assert.InDelta(t, 0.377, diveModel.DiveProfile.HeliumTissuePressures[3], 0.01)
	assert.InDelta(t, 0.296, diveModel.DiveProfile.HeliumTissuePressures[4], 0.01)
	assert.InDelta(t, 0.228, diveModel.DiveProfile.HeliumTissuePressures[5], 0.01)
	assert.InDelta(t, 0.172, diveModel.DiveProfile.HeliumTissuePressures[6], 0.01)
	assert.InDelta(t, 0.127, diveModel.DiveProfile.HeliumTissuePressures[7], 0.01)
	assert.InDelta(t, 0.093, diveModel.DiveProfile.HeliumTissuePressures[8], 0.01)
	assert.InDelta(t, 0.071, diveModel.DiveProfile.HeliumTissuePressures[9], 0.01)
	assert.InDelta(t, 0.056, diveModel.DiveProfile.HeliumTissuePressures[10], 0.01)
	assert.InDelta(t, 0.044, diveModel.DiveProfile.HeliumTissuePressures[11], 0.01)
	assert.InDelta(t, 0.035, diveModel.DiveProfile.HeliumTissuePressures[12], 0.01)
	assert.InDelta(t, 0.028, diveModel.DiveProfile.HeliumTissuePressures[13], 0.01)
	assert.InDelta(t, 0.022, diveModel.DiveProfile.HeliumTissuePressures[14], 0.01)
	assert.InDelta(t, 0.017, diveModel.DiveProfile.HeliumTissuePressures[15], 0.01)
}
