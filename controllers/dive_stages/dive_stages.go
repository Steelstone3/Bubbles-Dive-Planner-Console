package divestages

import (
	divemodels "github.com/Steelstone3/Bubbles-Dive-Planner-Console/models/dive_models"
	divestage "github.com/Steelstone3/Bubbles-Dive-Planner-Console/models/dive_stage"
)

func RunDiveStages(diveModel divemodels.DiveModel, diveStep divestage.DiveStep, selectedCylinder divestage.Cylinder) divemodels.DiveProfile {
	diveModel.DiveProfile = calculateAmbientPressure(diveModel.DiveProfile, diveStep, selectedCylinder.GasMixture)

	for compartment := uint(0); compartment < diveModel.CompartmentCount; compartment++ {
		diveModel.DiveProfile = updateDiveProfile(compartment, diveModel, diveStep)
	}

	return diveModel.DiveProfile
}

func updateDiveProfile(compartment uint, diveModel divemodels.DiveModel, diveStep divestage.DiveStep) divemodels.DiveProfile {
	diveModel.DiveProfile.NitrogenTissuePressures[compartment] = calculateNitrogenTissuePressure(compartment, diveModel, diveStep)
	diveModel.DiveProfile.HeliumTissuePressures[compartment] = calculateHeliumTissuePressure(compartment, diveModel, diveStep)
	diveModel.DiveProfile.TotalTissuePressures[compartment] = calculateTotalTissuePressure(compartment, diveModel.DiveProfile)
	diveModel.DiveProfile.AValues[compartment] = calculateAValue(compartment, diveModel)
	diveModel.DiveProfile.BValues[compartment] = calculateBValue(compartment, diveModel)
	diveModel.DiveProfile.ToleratedAmbientPressures[compartment] = calculateToleratedAmbientPressure(compartment, diveModel.DiveProfile)
	diveModel.DiveProfile.MaximumSurfacePressures[compartment] = calculateMaximumSurfacePressure(compartment, diveModel.DiveProfile)
	diveModel.DiveProfile.CompartmentLoads[compartment] = calculateCompartmentLoad(compartment, diveModel.DiveProfile)

	return diveModel.DiveProfile
}
