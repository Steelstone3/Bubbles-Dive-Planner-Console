package divemodels

import (
	"github.com/Steelstone3/Bubbles-Dive-Planner-Console/presenters"
)

type DiveProfile struct {
	NitrogenTissuePressures   []float64
	HeliumTissuePressures     []float64
	TotalTissuePressures      []float64
	ToleratedAmbientPressures []float64
	AValues                   []float64
	BValues                   []float64
	MaximumSurfacePressures   []float64
	CompartmentLoads          []float64
	PressurisedGasMixture     PressurisedGasMixture
}

func ConstructDiveProfile(compartmentCount uint) DiveProfile {
	return DiveProfile{
		NitrogenTissuePressures:   make([]float64, compartmentCount),
		HeliumTissuePressures:     make([]float64, compartmentCount),
		TotalTissuePressures:      make([]float64, compartmentCount),
		ToleratedAmbientPressures: make([]float64, compartmentCount),
		AValues:                   make([]float64, compartmentCount),
		BValues:                   make([]float64, compartmentCount),
		MaximumSurfacePressures:   make([]float64, compartmentCount),
		CompartmentLoads:          make([]float64, compartmentCount),
		PressurisedGasMixture:     ConstructPressurisedGasMixture(),
	}
}

func (d *DiveProfile) DisplayDiveProfile() {
	presenters.Print("\n")

	for compartment := 0; compartment < len(d.CompartmentLoads); compartment++ {
		diveProfileDisplayString := "| tTP {TotalTissuePressure} | TAP {ToleratedAmbientPressure} | MSP {MaximumSurfacePressure} | CLp {CompartmentLoad} |"
		diveProfileDisplayString = presenters.Float64StringReplace(diveProfileDisplayString, "{TotalTissuePressure}", d.TotalTissuePressures[compartment])
		diveProfileDisplayString = presenters.Float64StringReplace(diveProfileDisplayString, "{ToleratedAmbientPressure}", d.ToleratedAmbientPressures[compartment])
		diveProfileDisplayString = presenters.Float64StringReplace(diveProfileDisplayString, "{MaximumSurfacePressure}", d.MaximumSurfacePressures[compartment])
		diveProfileDisplayString = presenters.Float64StringReplace(diveProfileDisplayString, "{CompartmentLoad}", d.CompartmentLoads[compartment])
		presenters.Print(diveProfileDisplayString)
		presenters.Print("\n")
	}
}
