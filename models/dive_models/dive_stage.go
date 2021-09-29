package divemodels

import (
	divestage "github.com/Steelstone3/Bubbles-Dive-Planner-Console/models/dive_stage"
)

type DiveStage struct {
	DiveModel        DiveModel
	DiveStep         divestage.DiveStep
	SelectedCylinder divestage.Cylinder
	Cylinders        []divestage.Cylinder
}

func SetupDiveStage() DiveStage {
	return DiveStage{
		DiveModel: selectDiveModel(),
		Cylinders: divestage.ConstructCylinders(),
	}
}

func (diveStage *DiveStage) SetupDiveStep() {
	diveStage.DiveStep = divestage.ConstructDiveStep()
	diveStage.SelectedCylinder = divestage.SelectCylinder(diveStage.Cylinders)
}
