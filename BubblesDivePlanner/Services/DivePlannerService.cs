using BubblesDivePlanner.Controllers;
using BubblesDivePlanner.Presenters;

namespace BubblesDivePlannerTests.Services
{
    public class DivePlannerService : IDivePlannerService
    {
        private readonly IDiveController diveController;

        public DivePlannerService(IDiveController diveController)
        {
            this.diveController = diveController;
        }

        public void Run(IPresenter presenter)
        {
            diveController.SetupDivePlan();

            do
            {
                var divePlan = diveController.SetupDiveStep();
                divePlan = diveController.RunDiveProfile(divePlan);
                diveController.PrintDiveResults(divePlan.DiveModel.DiveProfile);
            } while (presenter.GetConfirmation("Continue?"));
        }
    }
}