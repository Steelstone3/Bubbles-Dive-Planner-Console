using System.Collections.Generic;
using BubblesDivePlanner.Models.Cylinders;
using BubblesDivePlanner.Models.DiveModels;

namespace BubblesDivePlanner.Models
{
    public interface IDivePlan
    {
        IDiveModel DiveModel { get; }
        IList<ICylinder> Cylinders { get; }
        IDiveStep DiveStep { get; }
        ICylinder SelectedCylinder { get; }
        void UpdateDiveStep(IDiveStep diveStep);
        void UpdateSelectedCylinder(ICylinder cylinder);
        string Serialise();
        void Deserialise(string expectedDivePlanJson);
    }
}