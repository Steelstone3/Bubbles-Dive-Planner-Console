package divestage

import (
	"math"

	"github.com/Steelstone3/Bubbles-Dive-Planner-Console/presenters"
)

type Cylinder struct {
	Name                      string
	InitialPressurisedVolume  uint
	Volume                    uint
	Pressure                  uint
	SurfaceAirConsumptionRate uint
	GasMixture                GasMixture
}

func SelectCylinder(cylinders []Cylinder) Cylinder {
	presenters.Print("\n")

	for _, cylinder := range cylinders {
		cylinder.displayCylinder()
	}

	index := uint(math.MaxUint64)

	for index >= uint(len(cylinders)) {
		index = presenters.GetUintFromConsole("\nSelect cylinder:")
	}

	return cylinders[index]
}

func (cylinder *Cylinder) displayCylinder() {
	cylinderString := "\n| Name: {Name} | Volume: {Volume} | O2: {Oxygen} N: {Nitrogen} He: {Helium} |"

	cylinderString = presenters.StringReplace(cylinderString, "{Name}", cylinder.Name)
	cylinderString = presenters.UintStringReplace(cylinderString, "{Volume}", cylinder.InitialPressurisedVolume)
	cylinderString = cylinder.GasMixture.appendGasMixture(cylinderString)

	presenters.Print(cylinderString)
}

func ConstructCylinders() []Cylinder {
	cylinders := []Cylinder{}
	cylinders = append(cylinders, constructCylinder())

	for presenters.GetConfirmationFromConsole("\nCreate new cylinder (Yes/yes/Y/y or No/no/N/n)?") {
		cylinders = append(cylinders, constructCylinder())
	}

	return cylinders
}

func constructCylinder() Cylinder {
	name := presenters.GetStringFromConsole("\nEnter cylinder name:")
	volume := presenters.GetUintFromConsole("Enter volume:")
	pressure := presenters.GetUintFromConsole("Enter pressure:")
	surfaceAirConsumptionRate := presenters.GetUintFromConsole("Enter SAC rate:")
	cylinder := verifyCylinder(volume, pressure, surfaceAirConsumptionRate)
	cylinder.Name = name
	cylinder.GasMixture = constructGasMixture()

	return cylinder
}

func verifyCylinder(volume uint, pressure uint, surfaceAirConsumptionRate uint) Cylinder {
	if volume > 30 {
		volume = 30
	} else if volume < 3 {
		volume = 3
	}

	if pressure > 300 {
		pressure = 300
	} else if pressure < 50 {
		pressure = 50
	}

	if surfaceAirConsumptionRate > 30 {
		surfaceAirConsumptionRate = 30
	} else if surfaceAirConsumptionRate < 6 {
		surfaceAirConsumptionRate = 6
	}

	return Cylinder{
		InitialPressurisedVolume:  volume * pressure,
		Volume:                    volume,
		Pressure:                  pressure,
		SurfaceAirConsumptionRate: surfaceAirConsumptionRate,
	}
}
