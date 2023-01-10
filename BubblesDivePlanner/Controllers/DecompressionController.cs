using System;
using BubblesDivePlanner.Models;
using BubblesDivePlanner.Presenters;

namespace BubblesDivePlanner.Controllers
{
    public class DecompressionController : IDecompressionController
    {
        private readonly IDivePresenter divePresenter;
        private readonly IDiveController diveController;

        public DecompressionController(IDivePresenter divePresenter, IDiveController diveController)
        {
            this.divePresenter = divePresenter;
            this.diveController = diveController;
        }

        public void RunDecompression(IDivePlan divePlan)
        {
            if (divePresenter.ConfirmDecompression())
            {
                var selectedCylinder = divePresenter.SelectCylinder(divePlan.Cylinders);

                if (divePlan.DiveModel.DiveProfile.DepthCeiling > 0.0)
                {
                    NextDiveStep(divePlan.DiveModel.DiveProfile.DepthCeiling);
                }

                divePlan = new DivePlan
                (
                    divePlan.DiveModel,
                    divePlan.Cylinders,
                    new DiveStep(0, 0),
                    selectedCylinder
                );

                diveController.RunDiveProfile(divePlan);
            }
        }

        public IDiveStep NextDiveStep(double depthCeiling)
        {
            var mod = depthCeiling % 3;
            var depth = (byte)(depthCeiling + (3-mod));

            return new DiveStep(depth,1);
        }
    }
}