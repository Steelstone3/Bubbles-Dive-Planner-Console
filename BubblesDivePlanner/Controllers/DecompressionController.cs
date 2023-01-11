using System.Collections.Generic;
using BubblesDivePlanner.Controllers.Json;
using BubblesDivePlanner.Models;
using BubblesDivePlanner.Models.Cylinders;
using BubblesDivePlanner.Presenters;

namespace BubblesDivePlanner.Controllers
{
    public class DecompressionController : IDecompressionController
    {
        private readonly IDivePresenter divePresenter;
        private readonly IDiveController diveController;
        private readonly IFileController fileController;

        public DecompressionController(IDivePresenter divePresenter, IDiveController diveController, IFileController fileController)
        {
            this.divePresenter = divePresenter;
            this.diveController = diveController;
            this.fileController = fileController;
        }

        public void RunDecompression(IDivePlan divePlan)
        {
            if (divePresenter.ConfirmDecompression(divePlan.DiveModel.DiveProfile.DepthCeiling))
            {
                var selectedCylinder = divePresenter.SelectCylinder(divePlan.Cylinders);

                while (divePlan.DiveModel.DiveProfile.DepthCeiling > 0.0)
                {
                    var decoDivePlan = RunDecompressionDiveStep(divePlan, selectedCylinder);
                    divePresenter.PrintDiveResult(decoDivePlan);
                    fileController.AddDivePlan(decoDivePlan);
                }
            }
        }

        private IDivePlan RunDecompressionDiveStep(IDivePlan divePlan, ICylinder selectedCylinder)
        {
            divePlan = new DivePlan
            (
                divePlan.DiveModel,
                divePlan.Cylinders,
                NextDiveStep(divePlan.DiveModel.DiveProfile.DepthCeiling),
                selectedCylinder
            );

            return diveController.RunDiveProfile(divePlan);
        }

        private static IDiveStep NextDiveStep(double depthCeiling)
        {
            var mod = depthCeiling % 3;
            var depth = (byte)(depthCeiling + (3 - mod));

            return new DiveStep(depth, 1);
        }
    }
}