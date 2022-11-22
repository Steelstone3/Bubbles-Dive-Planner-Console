using BubblesDivePlanner.Controllers;
using BubblesDivePlanner.Controllers.Json;
using BubblesDivePlanner.Presenters;

namespace BubblesDivePlannerTests.Services
{
    public class DivePlannerService : IDivePlannerService
    {
        private readonly IDiveController diveController;
        private readonly IFileController fileController;

        public DivePlannerService(IDiveController diveController, IFileController fileController)
        {
            this.diveController = diveController;
            this.fileController = fileController;
        }

        public void Run(IPresenter presenter)
        {
            var divePlan = fileController.LoadFile();
            diveController.SetupDivePlan(divePlan);

            do
            {
                divePlan = diveController.SetupDiveStep();
                divePlan = diveController.RunDiveProfile(divePlan);
                diveController.PrintDiveResults(divePlan);
                fileController.AddDivePlan(divePlan);
            } while (presenter.GetConfirmation("Continue?"));

            fileController.SaveFile();
        }
    }
}