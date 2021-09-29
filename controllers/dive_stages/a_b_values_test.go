package divestages

import (
	"testing"

	divemodels "github.com/Steelstone3/Bubbles-Dive-Planner-Console/models/dive_models"
	"github.com/stretchr/testify/assert"
)

func TestCalculateAValues(t *testing.T) {
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
		AValues: make([]float64, compartmentCount),
		BValues: make([]float64, compartmentCount),
	}
	diveModel := divemodels.DiveModel{
		NitrogenAValues: []float64{
			1.2559,
			1.0000,
			0.8618,
			0.7562,
			0.6667,
			0.5600,
			0.4947,
			0.4500,
			0.4187,
			0.3798,
			0.3497,
			0.3223,
			0.2850,
			0.2737,
			0.2523,
			0.2327,
		},
		HeliumAValues: []float64{
			1.7424,
			1.3830,
			1.1919,
			1.0458,
			0.9220,
			0.8205,
			0.7305,
			0.6502,
			0.5950,
			0.5545,
			0.5333,
			0.5189,
			0.5181,
			0.5176,
			0.5172,
			0.5119,
		},
		DiveProfile: diveProfile,
	}

	// When
	for compartment := uint(0); compartment < compartmentCount; compartment++ {
		diveModel.DiveProfile.AValues[compartment] = calculateAValue(compartment, diveModel)
	}

	// Then
	assert.InDelta(t, 1.328, diveModel.DiveProfile.AValues[0], 0.01)
	assert.InDelta(t, 1.070, diveModel.DiveProfile.AValues[1], 0.01)
	assert.InDelta(t, 0.930, diveModel.DiveProfile.AValues[2], 0.01)
	assert.InDelta(t, 0.822, diveModel.DiveProfile.AValues[3], 0.01)
	assert.InDelta(t, 0.728, diveModel.DiveProfile.AValues[4], 0.01)
	assert.InDelta(t, 0.625, diveModel.DiveProfile.AValues[5], 0.01)
	assert.InDelta(t, 0.555, diveModel.DiveProfile.AValues[6], 0.01)
	assert.InDelta(t, 0.503, diveModel.DiveProfile.AValues[7], 0.01)
	assert.InDelta(t, 0.466, diveModel.DiveProfile.AValues[8], 0.01)
	assert.InDelta(t, 0.427, diveModel.DiveProfile.AValues[9], 0.01)
	assert.InDelta(t, 0.399, diveModel.DiveProfile.AValues[10], 0.01)
	assert.InDelta(t, 0.376, diveModel.DiveProfile.AValues[11], 0.01)
	assert.InDelta(t, 0.349, diveModel.DiveProfile.AValues[12], 0.01)
	assert.InDelta(t, 0.341, diveModel.DiveProfile.AValues[13], 0.01)
	assert.InDelta(t, 0.326, diveModel.DiveProfile.AValues[14], 0.01)
	assert.InDelta(t, 0.309, diveModel.DiveProfile.AValues[15], 0.01)
}

func TestCalculateBValues(t *testing.T) {
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
		AValues: make([]float64, compartmentCount),
		BValues: make([]float64, compartmentCount),
	}
	diveModel := divemodels.DiveModel{
		NitrogenBValues: []float64{
			0.5050,
			0.6514,
			0.7222,
			0.7825,
			0.8126,
			0.8434,
			0.8693,
			0.8910,
			0.9092,
			0.9222,
			0.9319,
			0.9403,
			0.9477,
			0.9544,
			0.9602,
			0.9653,
		},
		HeliumBValues: []float64{
			0.4245,
			0.5747,
			0.6527,
			0.7223,
			0.7582,
			0.7957,
			0.8279,
			0.8553,
			0.8757,
			0.8903,
			0.8997,
			0.9073,
			0.9122,
			0.9171,
			0.9217,
			0.9267,
		},
		DiveProfile: diveProfile,
	}

	// When
	for compartment := uint(0); compartment < compartmentCount; compartment++ {
		diveModel.DiveProfile.BValues[compartment] = calculateBValue(compartment, diveModel)
	}

	// Then
	assert.InDelta(t, 0.493, diveModel.DiveProfile.BValues[0], 0.01)
	assert.InDelta(t, 0.637, diveModel.DiveProfile.BValues[1], 0.01)
	assert.InDelta(t, 0.708, diveModel.DiveProfile.BValues[2], 0.01)
	assert.InDelta(t, 0.769, diveModel.DiveProfile.BValues[3], 0.01)
	assert.InDelta(t, 0.800, diveModel.DiveProfile.BValues[4], 0.01)
	assert.InDelta(t, 0.831, diveModel.DiveProfile.BValues[5], 0.01)
	assert.InDelta(t, 0.859, diveModel.DiveProfile.BValues[6], 0.01)
	assert.InDelta(t, 0.882, diveModel.DiveProfile.BValues[7], 0.01)
	assert.InDelta(t, 0.900, diveModel.DiveProfile.BValues[8], 0.01)
	assert.InDelta(t, 0.914, diveModel.DiveProfile.BValues[9], 0.01)
	assert.InDelta(t, 0.923, diveModel.DiveProfile.BValues[10], 0.01)
	assert.InDelta(t, 0.931, diveModel.DiveProfile.BValues[11], 0.01)
	assert.InDelta(t, 0.938, diveModel.DiveProfile.BValues[12], 0.01)
	assert.InDelta(t, 0.944, diveModel.DiveProfile.BValues[13], 0.01)
	assert.InDelta(t, 0.949, diveModel.DiveProfile.BValues[14], 0.01)
	assert.InDelta(t, 0.955, diveModel.DiveProfile.BValues[15], 0.01)
}
