package divemodels

import (
	"math"

	"github.com/Steelstone3/Bubbles-Dive-Planner-Console/presenters"
)

type DiveModel struct {
	name              string
	CompartmentCount  uint
	NitrogenHalfTimes []float64
	HeliumHalfTimes   []float64
	NitrogenAValues   []float64
	NitrogenBValues   []float64
	HeliumAValues     []float64
	HeliumBValues     []float64
	DiveProfile       DiveProfile
}

// TODO Use this in a selection menu
func constructUsnDiveModel() DiveModel {
	const compartmentCount = 9

	return DiveModel{
		name:             "USN Revision 6",
		CompartmentCount: compartmentCount,
		NitrogenHalfTimes: []float64{
			5.0,
			10.0,
			20.0,
			40.0,
			80.0,
			120.0,
			160.0,
			200.0,
			240.0,
		},
		HeliumHalfTimes: []float64{
			5.0,
			10.0,
			20.0,
			40.0,
			80.0,
			120.0,
			160.0,
			200.0,
			240.0,
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
			0.870,
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
			1.0,
			1.0,
			1.0,
		},
		DiveProfile: ConstructDiveProfile(compartmentCount),
	}
}

func constructZhl16DiveModel() DiveModel {
	const compartmentCount = 16

	return DiveModel{
		name:             "Zhl16",
		CompartmentCount: compartmentCount,
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
		DiveProfile: ConstructDiveProfile(compartmentCount),
	}
}

func (diveModel *DiveModel) displayDiveModel() {
	diveModelString := "\n{Name}"

	diveModelString = presenters.StringReplace(diveModelString, "{Name}", diveModel.name)

	presenters.Print(diveModelString)
}

func selectDiveModel() DiveModel {
	var diveModels = []DiveModel{constructZhl16DiveModel(), constructUsnDiveModel()}

	for _, diveModel := range diveModels {
		diveModel.displayDiveModel()
	}

	index := uint(math.MaxUint64)

	for index >= uint(len(diveModels)) {
		index = presenters.GetUintFromConsole("\nSelect dive model:")
	}

	return diveModels[index]
}
