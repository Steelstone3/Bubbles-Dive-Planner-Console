using BubblesDivePlanner.Controllers;
using BubblesDivePlanner.Controllers.Json;
using BubblesDivePlanner.Presenters;
using BubblesDivePlannerTests.Services;

namespace BubblesDivePlanner
{
    static class Program
    {
        static void Main()
        {
            IPresenter presenter = new Presenter();
            IDivePresenter divePresenter = new DivePresenter(presenter);
            IFilePresenter filePresenter = new FilePresenter(presenter);
            IDiveStagesController diveStagesController = new DiveStagesController();
            IDiveController diveController = new DiveController(divePresenter, diveStagesController);
            IJsonController jsonController = new JsonController();
            IFileController fileController = new FileController(filePresenter, jsonController, new());
            IDecompressionController decompressionController = new DecompressionController(divePresenter, diveController, fileController);
            IDivePlannerService divePlannerService = new DivePlannerService(diveController, decompressionController, fileController);

            divePlannerService.Run(divePresenter);
        }
    }
}
