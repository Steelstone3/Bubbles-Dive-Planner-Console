using System.Collections.Generic;
using BubblesDivePlanner.Controllers;
using BubblesDivePlanner.Models;
using BubblesDivePlanner.Models.Cylinders;
using BubblesDivePlanner.Models.DiveModels;
using BubblesDivePlanner.Presenters;

namespace BubblesDivePlannerTests.Services
{
    public class DiveService : IDiveService
    {
        private readonly IDiveStepPresenter diveStepPresenter;
        private readonly IDiveSetupPresenter diveSetupPresenter;
        private readonly IDiveController diveController;
        private IDiveModel diveModel;
        private List<ICylinder> cylinders;

        public DiveService(IDiveStepPresenter diveStepPresenter, IDiveSetupPresenter diveSetupPresenter, IDiveController diveController)
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
    }
}