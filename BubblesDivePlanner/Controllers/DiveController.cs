using BubblesDivePlanner.Controllers.DiveStages;
using BubblesDivePlanner.Models;

namespace BubblesDivePlanner.Controllers
{
    public class DiveController : IDiveController
    {
        public IDivePlan Run(IDivePlan divePlan)
        {
            var diveStageCommands = CreateDiveStageCommands(divePlan);

            foreach (var diveStage in diveStageCommands)
            {
                diveStage.RunDiveStage();
            }

            return divePlan;
        }

        private IDiveStageCommand[] CreateDiveStageCommands(IDivePlan divePlan)
        {
            var diveProfile = divePlan.DiveModel.DiveProfile;
            var diveStep = divePlan.DiveStep;
            var gasMixture = divePlan.SelectedCylinder.GasMixture;

            return new IDiveStageCommand[]
            {
                new AmbientPressure(diveProfile, gasMixture, diveStep),

            };
        }
    }
}