package divestages

import (
	"testing"

	divemodels "github.com/Steelstone3/Bubbles-Dive-Planner-Console/models/dive_models"
	"github.com/stretchr/testify/assert"
)

func TestCalculateCompartmentLoad(t *testing.T) {
	// Given
	compartmentCount := uint(16)
	diveProfile := divemodels.DiveProfile{
		CompartmentLoads: make([]float64, compartmentCount),
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
		MaximumSurfacePressures: []float64{
			3.356,
			2.640,
			2.342,
			2.122,
			1.978,
			1.828,
			1.719,
			1.637,
			1.577,
			1.521,
			1.482,
			1.450,
			1.415,
			1.400,
			1.380,
			1.356,
		},
	}

	// When
	for compartment := uint(0); compartment < compartmentCount; compartment++ {
		diveProfile.CompartmentLoads[compartment] = calculateCompartmentLoad(compartment, diveProfile)
	}

	// Then
	assert.InDelta(t, 119.249, diveProfile.CompartmentLoads[0], 0.01)
	assert.InDelta(t, 111.326, diveProfile.CompartmentLoads[1], 0.01)
	assert.InDelta(t, 94.962, diveProfile.CompartmentLoads[2], 0.01)
	assert.InDelta(t, 78.746, diveProfile.CompartmentLoads[3], 0.01)
	assert.InDelta(t, 62.336, diveProfile.CompartmentLoads[4], 0.01)
	assert.InDelta(t, 49.945, diveProfile.CompartmentLoads[5], 0.01)
	assert.InDelta(t, 38.860, diveProfile.CompartmentLoads[6], 0.01)
	assert.InDelta(t, 29.505, diveProfile.CompartmentLoads[7], 0.01)
	assert.InDelta(t, 22.067, diveProfile.CompartmentLoads[8], 0.01)
	assert.InDelta(t, 17.291, diveProfile.CompartmentLoads[9], 0.01)
	assert.InDelta(t, 13.968, diveProfile.CompartmentLoads[10], 0.01)
	assert.InDelta(t, 11.172, diveProfile.CompartmentLoads[11], 0.01)
	assert.InDelta(t, 9.046, diveProfile.CompartmentLoads[12], 0.01)
	assert.InDelta(t, 7.214, diveProfile.CompartmentLoads[13], 0.01)
	assert.InDelta(t, 5.725, diveProfile.CompartmentLoads[14], 0.01)
	assert.InDelta(t, 4.572, diveProfile.CompartmentLoads[15], 0.01)
}
