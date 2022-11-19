using BubblesDivePlanner.Models;

namespace BubblesDivePlannerTests.Services
{
    public interface IDiveService
    {
        void SetupDivePlan();
        IDivePlan SetupDiveStep();
        void RunDiveProfile(IDivePlan divePlan);
    }
}