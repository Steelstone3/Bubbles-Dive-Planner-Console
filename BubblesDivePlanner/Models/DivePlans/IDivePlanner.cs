using System.Collections.Generic;
using BubblesDivePlanner.Models.Cylinders;
using BubblesDivePlanner.Models.DiveModels;

namespace BubblesDivePlanner.Models.DivePlans
{
    public interface IDivePlanner
    {
        List<IDiveProfile> AllDiveProfiles { get; }
        List<List<ICylinder>> AllCylinders { get; }
        List<IDiveStep> AllDiveSteps { get; }
        List<ICylinder> AllSelectedCylinders { get; }
        void UpdateDivePlans(IDivePlan divePlan);
    }
}