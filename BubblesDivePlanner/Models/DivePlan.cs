using System.Collections.Generic;
using System.Linq;
using BubblesDivePlanner.Controllers.Json;
using BubblesDivePlanner.Models.Cylinders;
using BubblesDivePlanner.Models.DiveModels;
using Newtonsoft.Json;

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

        public IDiveModel DiveModel { get; private set; }
        public List<ICylinder> Cylinders { get; private set; }
        public IDiveStep DiveStep { get; private set; }
        public ICylinder SelectedCylinder { get; private set; }
    }
}