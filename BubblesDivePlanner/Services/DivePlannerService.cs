using BubblesDivePlanner.Controllers;

namespace BubblesDivePlannerTests.Services
{
    public class DivePlannerService : IDivePlannerService
    {
        private readonly IDiveController diveController;

        public DivePlannerService(IDiveController diveController)
        {
            this.diveController = diveController;
        }

        public void Run()
        {
            diveController.SetupDivePlan();
            var divePlan = diveController.SetupDiveStep();
            divePlan = diveController.RunDiveProfile(divePlan);
            diveController.PrintDiveResults(divePlan.DiveModel.DiveProfile);
        }
    }
}