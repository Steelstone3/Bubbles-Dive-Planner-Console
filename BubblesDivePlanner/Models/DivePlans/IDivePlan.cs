using System.Collections.Generic;
using BubblesDivePlanner.Models.Cylinders;
using BubblesDivePlanner.Models.DiveModels;

namespace BubblesDivePlanner.Models.DivePlans
{
    public interface IDivePlan
    {
        IDivePlanner DivePlanner { get; }
        IDiveModel DiveModel { get; }
        List<ICylinder> Cylinders { get; }
        IDiveStep DiveStep { get; }
        ICylinder SelectedCylinder { get; }
        string Serialise();
        void Deserialise(string expectedDivePlanJson);
    }
}