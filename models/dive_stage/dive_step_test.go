package divestage

import (
	"testing"

	"github.com/stretchr/testify/assert"
)

func TestVerifyDiveStepIsOverLimit(t *testing.T) {
	// Given
	const depth uint = 100
	const time uint = 60
	diveStep := DiveStep{
		Depth: 101,
		Time:  61,
	}

	// When
	diveStep = verifyDiveStep(diveStep.Depth, diveStep.Time)

	// Then
	assert.Equal(t, depth, diveStep.Depth)
	assert.Equal(t, time, diveStep.Time)
}

func TestVerifyDiveStepIsUnderLimit(t *testing.T) {
	// Given
	const depth uint = 1
	const time uint = 1
	diveStep := DiveStep{
		Depth: 0,
		Time:  0,
	}

	// When
	diveStep = verifyDiveStep(diveStep.Depth, diveStep.Time)

	// Then
	assert.Equal(t, depth, diveStep.Depth)
	assert.Equal(t, time, diveStep.Time)
}

func TestVerifyDiveStepIsWithinLimit1(t *testing.T) {
	// Given
	const depth uint = 100
	const time uint = 60
	diveStep := DiveStep{
		Depth: 100,
		Time:  60,
	}

	// When
	diveStep = verifyDiveStep(diveStep.Depth, diveStep.Time)

	// Then
	assert.Equal(t, depth, diveStep.Depth)
	assert.Equal(t, time, diveStep.Time)
}

func TestVerifyDiveStepIsWithinLimit2(t *testing.T) {
	// Given
	const depth uint = 1
	const time uint = 1
	diveStep := DiveStep{
		Depth: 1,
		Time:  1,
	}

	// When
	diveStep = verifyDiveStep(diveStep.Depth, diveStep.Time)

	// Then
	assert.Equal(t, depth, diveStep.Depth)
	assert.Equal(t, time, diveStep.Time)
}
