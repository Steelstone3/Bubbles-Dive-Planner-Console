using BubblesDivePlanner.Controllers;
using BubblesDivePlanner.Controllers.Json;
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
            IFileController fileController = new FileController();
            IDivePlannerService divePlannerService = new DivePlannerService(diveController, fileController);

            divePlannerService.Run(presenter);
        }
    }
}
