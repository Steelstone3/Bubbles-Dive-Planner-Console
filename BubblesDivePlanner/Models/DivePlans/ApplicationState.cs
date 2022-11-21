using System.Collections.Generic;
using BubblesDivePlanner.Models.Cylinders;
using BubblesDivePlanner.Models.DiveModels;

namespace BubblesDivePlanner.Models.DivePlans
{
    public class ApplicationState : IApplicationState
    {
        public ApplicationState(List<IDiveProfile> allDiveProfiles, List<List<ICylinder>> allCylinders, List<IDiveStep> allDiveSteps, List<ICylinder> allSelectedCylinders)
        {
            AllDiveProfiles = allDiveProfiles ?? new List<IDiveProfile>();
            AllCylinders = allCylinders ?? new List<List<ICylinder>>();
            AllDiveSteps = allDiveSteps ?? new List<IDiveStep>();
            AllSelectedCylinders = allSelectedCylinders ?? new List<ICylinder>();
        }

        public List<IDiveProfile> AllDiveProfiles { get; }
        public List<List<ICylinder>> AllCylinders { get; }
        public List<IDiveStep> AllDiveSteps { get; }
        public List<ICylinder> AllSelectedCylinders { get; }

        public void UpdateDivePlans(IDivePlan divePlan)
        {
            AllDiveProfiles.Add(divePlan.DiveModel.DiveProfile);
            AllCylinders.Add(divePlan.Cylinders);
            AllSelectedCylinders.Add(divePlan.SelectedCylinder);
            AllDiveSteps.Add(divePlan.DiveStep);
        }
    }
}