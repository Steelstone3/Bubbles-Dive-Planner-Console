package divestage

import "github.com/Steelstone3/Bubbles-Dive-Planner-Console/presenters"

type GasMixture struct {
	Oxygen   uint
	Helium   uint
	Nitrogen uint
}

func (gasMixture *GasMixture) appendGasMixture(cylinderString string) string {
	cylinderString = presenters.UintStringReplace(cylinderString, "{Oxygen}", gasMixture.Oxygen)
	cylinderString = presenters.UintStringReplace(cylinderString, "{Nitrogen}", gasMixture.Nitrogen)
	cylinderString = presenters.UintStringReplace(cylinderString, "{Helium}", gasMixture.Helium)

	return cylinderString
}

func constructGasMixture() GasMixture {
	gasMixture := GasMixture{}
	gasMixture.Oxygen = presenters.GetUintFromConsoleInRange("\nEnter oxygen:", 5, 100)
	gasMixture.Helium = presenters.GetUintFromConsoleInRange("Enter helium:", 0, 100-gasMixture.Oxygen)
	gasMixture.Nitrogen = 100 - gasMixture.Oxygen - gasMixture.Helium

	return gasMixture
}
