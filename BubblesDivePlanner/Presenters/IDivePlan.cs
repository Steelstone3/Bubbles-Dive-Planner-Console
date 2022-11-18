using System.Collections.Generic;
using BubblesDivePlanner.Models.Cylinders;
using BubblesDivePlanner.Models.DiveModels;

namespace BubblesDivePlanner.Presenters
{
    public interface IDivePlan
    {
        void WelcomeMessage();
        IDiveModel SelectDiveModel();
        List<ICylinder> CreateCylinders();
    }
}