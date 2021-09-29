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
	oxygen := presenters.GetUintFromConsole("\nEnter oxygen:")
	helium := presenters.GetUintFromConsole("Enter helium:")

	return verifyGasMixture(oxygen, helium)
}

func verifyGasMixture(oxygen uint, helium uint) GasMixture {
	if oxygen > 100 {
		oxygen = 100
		helium = 0
	} else if oxygen+helium > 100 {
		helium = 100 - oxygen
	} else if oxygen < 5 {
		oxygen = 5
	}

	return GasMixture{
		Oxygen:   oxygen,
		Helium:   helium,
		Nitrogen: 100 - oxygen - helium,
	}
}
