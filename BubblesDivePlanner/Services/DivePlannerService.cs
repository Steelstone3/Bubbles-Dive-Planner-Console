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
                diveController.PrintDiveResults(divePlan.DiveModel);
                // diveController.RunGasManagement(divePlan.SelectedCylinder, divePlan.DiveStep);
                // diveController.PrintCylinder(divePlan.SelectedCylinder);
            } while (presenter.GetConfirmation("Continue?"));

            fileController.SaveFile(divePlan);
        }
    }
}