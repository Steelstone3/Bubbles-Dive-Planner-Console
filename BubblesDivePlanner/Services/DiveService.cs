using System.Collections.Generic;
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
        private IDiveModel diveModel;
        private List<ICylinder> cylinders;
        private IDiveStep diveStep;
        private ICylinder cylinder;

        public DiveService(IDiveStepPresenter diveStepPresenter, IDiveSetupPresenter diveSetupPresenter)
        {
            this.diveStepPresenter = diveStepPresenter;
            this.diveSetupPresenter = diveSetupPresenter;
        }

        public void SetupDivePlan()
        {
            diveSetupPresenter.WelcomeMessage();
            diveModel = diveSetupPresenter.SelectDiveModel();
            cylinders = diveSetupPresenter.CreateCylinders();
        }

        public void SetupDiveStep()
        {
            diveStep = diveStepPresenter.CreateDiveStep();
            cylinder = diveStepPresenter.SelectCylinder(cylinders);
        }
    }
}