package divestage

import (
	"testing"

	"github.com/stretchr/testify/assert"
)

func TestVerifyCylinderIsWithinLimit(t *testing.T) {
	// Given
	const initialPressurisedVolume uint = 2400
	const volume uint = 12
	const pressure uint = 200
	const surfaceAirConsumptionRate uint = 12
	cylinder := Cylinder{
		Volume:                    volume,
		Pressure:                  pressure,
		SurfaceAirConsumptionRate: surfaceAirConsumptionRate,
	}

	// When
	cylinder = verifyCylinder(cylinder.Volume, cylinder.Pressure, cylinder.SurfaceAirConsumptionRate)

	// Then
	assert.Equal(t, volume, cylinder.Volume)
	assert.Equal(t, pressure, cylinder.Pressure)
	assert.Equal(t, surfaceAirConsumptionRate, cylinder.SurfaceAirConsumptionRate)
	assert.Equal(t, initialPressurisedVolume, cylinder.InitialPressurisedVolume)
}

func TestVerifyCylinderIsOverLimit(t *testing.T) {
	// Given
	const initialPressurisedVolume uint = 9000
	const volume uint = 30
	const pressure uint = 300
	const surfaceAirConsumptionRate uint = 30
	cylinder := Cylinder{
		Volume:                    31,
		Pressure:                  301,
		SurfaceAirConsumptionRate: 31,
	}

	// When
	cylinder = verifyCylinder(cylinder.Volume, cylinder.Pressure, cylinder.SurfaceAirConsumptionRate)

	// Then
	assert.Equal(t, volume, cylinder.Volume)
	assert.Equal(t, pressure, cylinder.Pressure)
	assert.Equal(t, surfaceAirConsumptionRate, cylinder.SurfaceAirConsumptionRate)
	assert.Equal(t, initialPressurisedVolume, cylinder.InitialPressurisedVolume)
}

func TestVerifyCylinderIsUnderLimit(t *testing.T) {
	// Given
	const initialPressurisedVolume uint = 150
	const volume uint = 3
	const pressure uint = 50
	const surfaceAirConsumptionRate uint = 6
	cylinder := Cylinder{
		Volume:                    2,
		Pressure:                  49,
		SurfaceAirConsumptionRate: 5,
	}

	// When
	cylinder = verifyCylinder(cylinder.Volume, cylinder.Pressure, cylinder.SurfaceAirConsumptionRate)

	// Then
	assert.Equal(t, volume, cylinder.Volume)
	assert.Equal(t, pressure, cylinder.Pressure)
	assert.Equal(t, surfaceAirConsumptionRate, cylinder.SurfaceAirConsumptionRate)
	assert.Equal(t, initialPressurisedVolume, cylinder.InitialPressurisedVolume)
}
