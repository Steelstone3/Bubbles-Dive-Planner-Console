using System;
using System.Collections.Generic;
using BubblesDivePlanner.Models;
using BubblesDivePlanner.Models.Cylinders;
using BubblesDivePlanner.Models.DiveModels;
using BubblesDivePlanner.Presenters;

namespace BubblesDivePlanner.Controllers
{
    public class DiveController : IDiveController
    {
        private readonly IDiveStepPresenter diveStepPresenter;
        private readonly IDiveSetupPresenter diveSetupPresenter;
        private readonly IDiveStagesController diveStagesController;
        private IDiveModel diveModel;
        private List<ICylinder> cylinders;

        public DiveController(IDiveStepPresenter diveStepPresenter, IDiveSetupPresenter diveSetupPresenter, IDiveStagesController diveStagesController)
        {
            this.diveStepPresenter = diveStepPresenter;
            this.diveSetupPresenter = diveSetupPresenter;
            this.diveStagesController = diveStagesController;
        }

        public void SetupDivePlan(IDivePlan divePlan)
        {
            diveSetupPresenter.WelcomeMessage();

            if (divePlan == null)
            {
                diveModel = diveSetupPresenter.SelectDiveModel();
                cylinders = diveSetupPresenter.CreateCylinders(diveModel.Name);
            }
            else if (divePlan.DiveModel == null || divePlan.Cylinders == null)
            {
                diveModel = diveSetupPresenter.SelectDiveModel();
                cylinders = diveSetupPresenter.CreateCylinders(diveModel.Name);
            }
            else
            {
                diveModel = divePlan.DiveModel;
                cylinders = divePlan.Cylinders;
            }
        }

        public IDivePlan SetupDiveStep()
        {
            var maximumDepth = (byte)100;
            var selectedCylinder = diveStepPresenter.SelectCylinder(cylinders);

            if (selectedCylinder != null)
            {
                maximumDepth = selectedCylinder.GasMixture.MaximumOperatingDepth < 100 ?
                   (byte)Math.Floor(selectedCylinder.GasMixture.MaximumOperatingDepth) : (byte)100;
            }

            return new DivePlan(diveModel, cylinders, diveStepPresenter.CreateDiveStep(maximumDepth), selectedCylinder);
        }

        public IDivePlan RunDiveProfile(IDivePlan divePlan)
        {
            return diveStagesController.Run(divePlan);
        }

        public void PrintDiveResults(IDivePlan divePlan)
        {
            diveSetupPresenter.PrintDiveResults(divePlan);
        }
    }
}