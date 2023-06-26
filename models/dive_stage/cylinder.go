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
		index = presenters.GetUintFromConsoleInRange("\nSelect cylinder:", 0, uint(len(cylinders))-1)
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
	cylinder := Cylinder{}
	cylinder.Name = presenters.GetValidStringFromConsole("\nEnter cylinder name:")
	cylinder.Volume = presenters.GetUintFromConsoleInRange("Enter volume:", 3, 30)
	cylinder.Pressure = presenters.GetUintFromConsoleInRange("Enter pressure:", 50, 300)
	cylinder.SurfaceAirConsumptionRate = presenters.GetUintFromConsoleInRange("Enter SAC rate:", 6, 30)
	cylinder.InitialPressurisedVolume = cylinder.Volume * cylinder.Pressure
	cylinder.GasMixture = constructGasMixture()

	return cylinder
}
