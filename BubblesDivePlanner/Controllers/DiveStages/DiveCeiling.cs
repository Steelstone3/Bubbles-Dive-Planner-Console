using System;
using System.Linq;
using BubblesDivePlanner.Models.DiveModels;

namespace BubblesDivePlanner.Controllers.DiveStages
{
    public class DiveCeiling : IDiveStageCommand
    {
        private IDiveProfile diveProfile;

        public DiveCeiling(IDiveProfile diveProfile)
        {
            this.diveProfile = diveProfile;
        }

        public void RunDiveStage()
        {
            diveProfile.DepthCeiling = Math.Round((diveProfile.ToleratedAmbientPressures.Max() - 1.0) * 10.0, 2);
        }
    }
}