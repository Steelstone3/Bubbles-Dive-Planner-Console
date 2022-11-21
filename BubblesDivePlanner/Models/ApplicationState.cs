// using System.Collections.Generic;
// using BubblesDivePlanner.Models.Cylinders;
// using BubblesDivePlanner.Models.DiveModels;

// namespace BubblesDivePlanner.Models
// {
//     public class ApplicationState : IApplicationState
//     {
//         public List<IDiveProfile> AllDiveProfiles => throw new System.NotImplementedException();
//         public List<List<ICylinder>> AllCylinders => throw new System.NotImplementedException();
//         public List<IDiveStep> AllDiveSteps => throw new System.NotImplementedException();
//         public List<ICylinder> AllSelectedCylinders => throw new System.NotImplementedException();

//         public void UpdateApplicationState(IDivePlan divePlan)
//         {
//             AllDiveProfiles.Add(divePlan.DiveModel.DiveProfile);
//             AllCylinders.Add(divePlan.Cylinders);
//             AllSelectedCylinders.Add(divePlan.SelectedCylinder);
//             AllDiveSteps.Add(divePlan.DiveStep);
//         }
//     }
// }