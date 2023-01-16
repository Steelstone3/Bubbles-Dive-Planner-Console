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
        private readonly IDivePresenter divePresenter;
        private readonly IDiveStagesController diveStagesController;
        private IDiveModel diveModel;
        private List<ICylinder> cylinders;

        public DiveController(IDivePresenter divePresenter, IDiveStagesController diveStagesController)
        {
            this.divePresenter = divePresenter;
            this.diveStagesController = diveStagesController;
        }

        public void SetupDivePlan(IDivePlan divePlan)
        {
            divePresenter.WelcomeMessage();
            divePresenter.DisplayHelp();
            divePresenter.DisplayResultOption();

            if (divePlan == null)
            {
                diveModel = divePresenter.SelectDiveModel();
                cylinders = divePresenter.CreateCylinders(diveModel.Name);
            }
            else if (divePlan.DiveModel == null || divePlan.Cylinders == null)
            {
                diveModel = divePresenter.SelectDiveModel();
                cylinders = divePresenter.CreateCylinders(diveModel.Name);
            }
            else
            {
                diveModel = divePlan.DiveModel;
                cylinders = divePlan.Cylinders;
            }
        }

        public IDivePlan SetupDiveStep(double depthCeiling)
        {
            var maximumDepth = (byte)100;
            var selectedCylinder = divePresenter.SelectCylinder(cylinders);

            if (selectedCylinder != null)
            {
                maximumDepth = selectedCylinder.GasMixture.MaximumOperatingDepth < 100 ?
                   (byte)Math.Floor(selectedCylinder.GasMixture.MaximumOperatingDepth) : (byte)100;
            }

            return new DivePlan(diveModel, cylinders, divePresenter.CreateDiveStep(depthCeiling, maximumDepth), selectedCylinder);
        }

        public IDivePlan RunDiveProfile(IDivePlan divePlan)
        {
            return diveStagesController.Run(divePlan);
        }

        public void PrintDiveResult(IDivePlan divePlan)
        {
            if (divePlan == null)
            {
                return;
            }

            divePresenter.PrintDiveResult(divePlan);
        }
    }
}