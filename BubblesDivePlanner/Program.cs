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
            var diveStagesController = new DiveStagesController();
            var diveController = new DiveController(diveStepPresenter, diveSetupPresenter, diveStagesController);

            diveController.SetupDivePlan();

            do
            {
                var divePlan = diveController.SetupDiveStep();
                var updatedDivePlan = diveController.RunDiveProfile(divePlan);
                diveController.PrintDiveResults(updatedDivePlan.DiveModel.DiveProfile);
            } while (presenter.GetConfirmation("Continue?"));
        }
    }
}
