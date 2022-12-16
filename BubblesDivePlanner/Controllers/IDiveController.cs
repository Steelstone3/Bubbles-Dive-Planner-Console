using BubblesDivePlanner.Models;

namespace BubblesDivePlanner.Controllers
{
    public interface IDiveController
    {
        void SetupDivePlan(IDivePlan divePlan);
        IDivePlan SetupDiveStep(byte depthCeiling);
        IDivePlan RunDiveProfile(IDivePlan divePlan);
        void PrintDiveResults(IDivePlan divePlan);
    }
}