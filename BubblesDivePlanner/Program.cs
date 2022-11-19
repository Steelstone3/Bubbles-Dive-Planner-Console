using BubblesDivePlanner.Controllers;
using BubblesDivePlanner.Presenters;
using BubblesDivePlannerTests.Services;

namespace BubblesDivePlanner
{
    class Program
    {
        static void Main(string[] args)
        {
            IPresenter presenter = new Presenter();
            IDiveSetupPresenter diveSetupPresenter = new DiveSetupPresenter(presenter);
            IDiveStepPresenter diveStepPresenter = new DiveStepPresenter(presenter);
            IDiveStagesController diveStagesController = new DiveStagesController();
            IDiveController diveController = new DiveController(diveStepPresenter, diveSetupPresenter, diveStagesController);
            IDivePlannerService divePlannerService = new DivePlannerService(diveController);
            
            divePlannerService.Run(presenter);
        }
    }
}
