using System.Collections.Generic;
using BubblesDivePlanner.Models.Cylinders;
using BubblesDivePlanner.Models.DiveModels;

namespace BubblesDivePlanner.Models
{
    public class DivePlan : IDivePlan
    {
        public DivePlan(IDiveModel diveModel, List<ICylinder> cylinders, IDiveStep diveStep, ICylinder selectedCylinder)
        {
            DiveModel = diveModel;
            Cylinders = cylinders;
            DiveStep = diveStep;
            SelectedCylinder = selectedCylinder;
        }

        public IDiveModel DiveModel { get; }
        public List<ICylinder> Cylinders { get; }
        public IDiveStep DiveStep { get; }
        public ICylinder SelectedCylinder { get; }
    }
}