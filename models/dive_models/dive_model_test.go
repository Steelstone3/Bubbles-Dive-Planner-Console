package divemodels

import (
	"testing"

	"github.com/stretchr/testify/assert"
)

func TestConstructZhl16DiveModel(t *testing.T) {
	// Given
	diveModel := DiveModel{
		name:             "Zhl16",
		CompartmentCount: uint(16),
		NitrogenHalfTimes: []float64{
			4,
			8,
			12.5,
			18.5,
			27,
			38.3,
			54.3,
			77,
			109,
			146,
			187,
			239,
			305,
			390,
			498,
			635,
		},
		HeliumHalfTimes: []float64{
			1.51,
			3.02,
			4.72,
			6.99,
			10.21,
			14.48,
			20.53,
			29.11,
			41.2,
			55.19,
			70.69,
			90.34,
			115.29,
			147.42,
			188.24,
			240.03,
		},
		NitrogenAValues: []float64{
			1.2559,
			1,
			0.8618,
			0.7562,
			0.6667,
			0.56,
			0.4947,
			0.45,
			0.4187,
			0.3798,
			0.3497,
			0.3223,
			0.285,
			0.2737,
			0.2523,
			0.2327,
		},
		NitrogenBValues: []float64{
			0.505,
			0.6514,
			0.7222,
			0.7825,
			0.8126,
			0.8434,
			0.8693,
			0.891,
			0.9092,
			0.9222,
			0.9319,
			0.9403,
			0.9477,
			0.9544,
			0.9602,
			0.9653,
		},
		HeliumAValues: []float64{
			1.7424,
			1.383,
			1.1919,
			1.0458,
			0.922,
			0.8205,
			0.7305,
			0.6502,
			0.595,
			0.5545,
			0.5333,
			0.5189,
			0.5181,
			0.5176,
			0.5172,
			0.5119,
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
		DiveProfile: DiveProfile{
			NitrogenTissuePressures:   make([]float64, 16),
			HeliumTissuePressures:     make([]float64, 16),
			TotalTissuePressures:      make([]float64, 16),
			ToleratedAmbientPressures: make([]float64, 16),
			AValues:                   make([]float64, 16),
			BValues:                   make([]float64, 16),
			MaximumSurfacePressures:   make([]float64, 16),
			CompartmentLoads:          make([]float64, 16),
			PressurisedGasMixture: PressurisedGasMixture{
				Oxygen:   0,
				Helium:   0,
				Nitrogen: 0,
			},
		},
	}

	// When
	actualDiveModel := constructZhl16DiveModel()

	// Then
	assert.Equal(t, diveModel, actualDiveModel)
}

func TestConstructUsnDiveModel(t *testing.T) {
	// Given
	diveModel := DiveModel{
		name:             "USN Revision 6",
		CompartmentCount: uint(9),
		NitrogenHalfTimes: []float64{
			5,
			10,
			20,
			40,
			80,
			120,
			160,
			200,
			240,
		},
		HeliumHalfTimes: []float64{
			5,
			10,
			20,
			40,
			80,
			120,
			160,
			200,
			240,
		},
		NitrogenAValues: []float64{
			1.37,
			1.08,
			0.69,
			0.3,
			0.34,
			0.38,
			0.4,
			0.45,
			0.42,
		},
		NitrogenBValues: []float64{
			0.555,
			0.625,
			0.666,
			0.714,
			0.769,
			0.833,
			0.87,
			0.909,
			0.909,
		},
		HeliumAValues: []float64{
			1.12,
			0.85,
			0.71,
			0.63,
			0.5,
			0.44,
			0.54,
			0.61,
			0.61,
		},
		HeliumBValues: []float64{
			0.67,
			0.714,
			0.769,
			0.83,
			0.83,
			0.91,
			1,
			1,
			1,
		},
		DiveProfile: DiveProfile{
			NitrogenTissuePressures:   make([]float64, 9),
			HeliumTissuePressures:     make([]float64, 9),
			TotalTissuePressures:      make([]float64, 9),
			ToleratedAmbientPressures: make([]float64, 9),
			AValues:                   make([]float64, 9),
			BValues:                   make([]float64, 9),
			MaximumSurfacePressures:   make([]float64, 9),
			CompartmentLoads:          make([]float64, 9),
			PressurisedGasMixture: PressurisedGasMixture{
				Oxygen:   0,
				Helium:   0,
				Nitrogen: 0,
			},
		},
	}

	// When
	actualDiveModel := constructUsnDiveModel()

	// Then
	assert.Equal(t, diveModel, actualDiveModel)
}
