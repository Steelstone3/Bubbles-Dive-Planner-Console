using System.Collections.Generic;
using BubblesDivePlanner.Models;

namespace BubblesDivePlanner.Controllers
{
    public interface IDiveController
    {
        void SetupDivePlan(IDivePlan divePlan);
        IDivePlan SetupDiveStep(double depthCeiling);
        IDivePlan RunDiveProfile(IDivePlan divePlan);
        void PrintDiveResult(IDivePlan divePlan);
    }
}