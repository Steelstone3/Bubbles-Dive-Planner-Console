using BubblesDivePlanner.Models;
using BubblesDivePlanner.Models.Cylinders;
using BubblesDivePlanner.Models.DiveModels;

namespace BubblesDivePlanner.Controllers.DiveStages
{
    public class AmbientPressureCommand : IDiveStageCommand
    {
        private readonly IDiveProfile diveProfile;
        private readonly IGasMixture gasMixture;
        private readonly IDiveStep diveStep;

        public AmbientPressureCommand(IDiveProfile diveProfile, IGasMixture gasMixtureModel, IDiveStep diveStepModel)
        {
            this.diveProfile = diveProfile;
            gasMixture = gasMixtureModel;
            diveStep = diveStepModel;
        }

        public void RunDiveStage()
        {
            var pressureAmbient = CalculateAmbientPressure();
            CalculateAdjustedGasPressures(pressureAmbient);
        }

        private double CalculateAmbientPressure()
        {
            return 1.0 + diveStep.Depth / 10.0;
        }

        private void CalculateAdjustedGasPressures(double pressureAmbient)
        {
            var pressureOxygen = gasMixture.Oxygen / 100 * pressureAmbient;
            var pressureHelium = gasMixture.Helium / 100 * pressureAmbient;
            var pressureNitrogen = gasMixture.Nitrogen / 100 * pressureAmbient;
            diveProfile.UpdateGasMixtureUnderPressure(pressureOxygen, pressureHelium, pressureNitrogen);
        }
    }
}