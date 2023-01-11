using System.Collections.Generic;
using BubblesDivePlanner.Models;
using BubblesDivePlanner.Models.Cylinders;
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

        public List<IDivePlan> RunDecompression(IDivePlan divePlan)
        {
            if (divePresenter.ConfirmDecompression(divePlan.DiveModel.DiveProfile.DepthCeiling))
            {
                List<IDivePlan> divePlans = new();

                var selectedCylinder = divePresenter.SelectCylinder(divePlan.Cylinders);

                while (divePlan.DiveModel.DiveProfile.DepthCeiling > 0.0)
                {
                    divePlans.Add(RunDecompressionDiveStep(divePlan, selectedCylinder));
                }

                return divePlans;
            }

            return null;
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