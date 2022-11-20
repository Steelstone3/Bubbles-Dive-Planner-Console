using System.Collections.Generic;
using BubblesDivePlanner.Controllers;
using BubblesDivePlanner.Controllers.Json;
using BubblesDivePlanner.Models;
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
            var divePlans = new List<IDivePlan>();
            var divePlan = fileController.LoadFile();
            diveController.SetupDivePlan(divePlan);

            do
            {
                divePlan = diveController.SetupDiveStep();
                divePlan = diveController.RunDiveProfile(divePlan);
                diveController.PrintDiveResults(divePlan);
                divePlans.Add(divePlan);
            } while (presenter.GetConfirmation("Continue?"));

            // put divePlans in here
            fileController.SaveFile(divePlan);
        }
    }
}