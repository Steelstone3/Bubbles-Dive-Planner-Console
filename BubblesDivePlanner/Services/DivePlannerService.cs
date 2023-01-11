using System;
using System.Collections.Generic;
using System.Linq;
using BubblesDivePlanner.Controllers;
using BubblesDivePlanner.Controllers.Json;
using BubblesDivePlanner.Models;
using BubblesDivePlanner.Presenters;

namespace BubblesDivePlannerTests.Services
{
    public class DivePlannerService : IDivePlannerService
    {
        private readonly IDiveController diveController;
        private readonly IDecompressionController decompressionController;
        private readonly IFileController fileController;

        public DivePlannerService(IDiveController diveController, IDecompressionController decompressionController, IFileController fileController)
        {
            this.diveController = diveController;
            this.decompressionController = decompressionController;
            this.fileController = fileController;
        }

        public void Run(IDivePresenter divePresenter)
        {
            var divePlan = fileController.LoadFile();
            diveController.SetupDivePlan(divePlan);

            do
            {
                var depthCeiling = divePlan?.DiveModel?.DiveProfile != null ? divePlan.DiveModel.DiveProfile.DepthCeiling : 0.0;
                divePlan = diveController.SetupDiveStep((byte)Math.Ceiling(depthCeiling));
                divePlan = diveController.RunDiveProfile(divePlan);
                diveController.PrintDiveResult(divePlan);
                var divePlans = decompressionController.RunDecompression(divePlan);
                diveController.PrintDecompressionResults(divePlans);
                fileController.AddDivePlan(divePlan);
                fileController.AddDivePlans(divePlans);
            } while (divePresenter.ConfirmContinueWithDive());

            fileController.SaveFile();
        }
    }
}