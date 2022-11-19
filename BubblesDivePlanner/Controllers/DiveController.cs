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
        private readonly IDiveStagesController diveController;
        private IDiveModel diveModel;
        private List<ICylinder> cylinders;

        public DiveController(IDiveStepPresenter diveStepPresenter, IDiveSetupPresenter diveSetupPresenter, IDiveStagesController diveController)
        {
            this.diveStepPresenter = diveStepPresenter;
            this.diveSetupPresenter = diveSetupPresenter;
            this.diveController = diveController;
        }

        public void SetupDivePlan()
        {
            diveSetupPresenter.WelcomeMessage();
            diveModel = diveSetupPresenter.SelectDiveModel();
            cylinders = diveSetupPresenter.CreateCylinders();
        }

        public IDivePlan SetupDiveStep()
        {
            return new DivePlan(diveModel, cylinders, diveStepPresenter.CreateDiveStep(), diveStepPresenter.SelectCylinder(cylinders));
        }

        public IDivePlan RunDiveProfile(IDivePlan divePlan)
        {
            return diveController.Run(divePlan);
        }

        public void PrintDiveResults(IDiveProfile diveProfile)
        {
            diveSetupPresenter.PrintDiveResults(diveProfile);
        }

        public void RunGasManagement(ICylinder selectedCylinder, IDiveStep diveStep)
        {
            selectedCylinder.UpdateCylinderGasConsumption(diveStep);
        }

        public void PrintCylinder(ICylinder selectedCylinder)
        {
            diveSetupPresenter.PrintCylinder(selectedCylinder);
        }
    }
}