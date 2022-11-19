using BubblesDivePlanner.Controllers;
using BubblesDivePlanner.Presenters;
using BubblesDivePlannerTests.Services;

namespace BubblesDivePlanner
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO extract to some sort of runner service
            var presenter = new Presenter();
            var diveSetupPresenter = new DiveSetupPresenter(presenter);
            var diveStepPresenter = new DiveStepPresenter(presenter);
            var diveController = new DiveStagesController();
            var diveService = new DivePlannerService(diveStepPresenter, diveSetupPresenter, diveController);

            diveService.SetupDivePlan();

            do
            {
                var divePlan = diveService.SetupDiveStep();
                var updatedDivePlan = diveService.RunDiveProfile(divePlan);
                diveService.PrintDiveResults(updatedDivePlan.DiveModel.DiveProfile);
            } while (presenter.GetConfirmation("Continue?"));
        }
    }
}
