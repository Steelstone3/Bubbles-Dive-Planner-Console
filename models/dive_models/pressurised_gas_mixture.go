package divemodels

type PressurisedGasMixture struct {
	Oxygen   float64
	Helium   float64
	Nitrogen float64
}

func ConstructPressurisedGasMixture() PressurisedGasMixture {
	return PressurisedGasMixture{
		Oxygen:   0,
		Helium:   0,
		Nitrogen: 0,
	}
}
