using BubblesDivePlanner.Models;
using BubblesDivePlanner.Models.DiveModels;

namespace BubblesDivePlannerTests.Services
{
    public interface IDivePlannerService
    {
        void SetupDivePlan();
        IDivePlan SetupDiveStep();
        IDivePlan RunDiveProfile(IDivePlan divePlan);
        void PrintDiveResults(IDiveProfile diveProfile);
    }
}