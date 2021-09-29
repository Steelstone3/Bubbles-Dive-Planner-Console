package divestage

import (
	"testing"

	"github.com/stretchr/testify/assert"
)

func TestVerifyGasMixtureIsWithinLimit(t *testing.T) {
	// Given
	const oxygen uint = 21
	const helium uint = 0
	const nitrogen uint = 79
	gasMixture := GasMixture{
		Oxygen:   oxygen,
		Helium:   helium,
		Nitrogen: nitrogen,
	}

	// When
	gasMixture = verifyGasMixture(gasMixture.Oxygen, gasMixture.Helium)

	// Then
	assert.Equal(t, oxygen, gasMixture.Oxygen)
	assert.Equal(t, helium, gasMixture.Helium)
	assert.Equal(t, nitrogen, gasMixture.Nitrogen)
}

func TestVerifyGasMixtureIsOverLimit1(t *testing.T) {
	// Given
	const oxygen uint = 100
	const helium uint = 0
	const nitrogen uint = 0
	gasMixture := GasMixture{
		Oxygen:   101,
		Helium:   20,
		Nitrogen: nitrogen,
	}

	// When
	gasMixture = verifyGasMixture(gasMixture.Oxygen, gasMixture.Helium)

	// Then
	assert.Equal(t, oxygen, gasMixture.Oxygen)
	assert.Equal(t, helium, gasMixture.Helium)
	assert.Equal(t, nitrogen, gasMixture.Nitrogen)
}

func TestVerifyGasMixtureIsOverLimit2(t *testing.T) {
	// Given
	const oxygen uint = 100
	const helium uint = 0
	const nitrogen uint = 0
	gasMixture := GasMixture{
		Oxygen:   101,
		Helium:   101,
		Nitrogen: nitrogen,
	}

	// When
	gasMixture = verifyGasMixture(gasMixture.Oxygen, gasMixture.Helium)

	// Then
	assert.Equal(t, oxygen, gasMixture.Oxygen)
	assert.Equal(t, helium, gasMixture.Helium)
	assert.Equal(t, nitrogen, gasMixture.Nitrogen)
}

func TestVerifyGasMixtureIsOverLimit3(t *testing.T) {
	// Given
	const oxygen uint = 51
	const helium uint = 49
	const nitrogen uint = 0
	gasMixture := GasMixture{
		Oxygen:   51,
		Helium:   51,
		Nitrogen: nitrogen,
	}

	// When
	gasMixture = verifyGasMixture(gasMixture.Oxygen, gasMixture.Helium)

	// Then
	assert.Equal(t, oxygen, gasMixture.Oxygen)
	assert.Equal(t, helium, gasMixture.Helium)
	assert.Equal(t, nitrogen, gasMixture.Nitrogen)
}

func TestVerifyGasMixtureIsUnderLimit(t *testing.T) {
	// Given
	const oxygen uint = 5
	const helium uint = 10
	const nitrogen uint = 85
	gasMixture := GasMixture{
		Oxygen:   1,
		Helium:   10,
		Nitrogen: nitrogen,
	}

	// When
	gasMixture = verifyGasMixture(gasMixture.Oxygen, gasMixture.Helium)

	// Then
	assert.Equal(t, oxygen, gasMixture.Oxygen)
	assert.Equal(t, helium, gasMixture.Helium)
	assert.Equal(t, nitrogen, gasMixture.Nitrogen)
}
