using BubblesDivePlanner.Models;
using BubblesDivePlanner.Models.DiveModels;

namespace BubblesDivePlannerTests.Services
{
    public interface IDiveService
    {
        void SetupDivePlan();
        IDivePlan SetupDiveStep();
        IDivePlan RunDiveProfile(IDivePlan divePlan);
        void PrintDiveResults(IDiveProfile diveProfile);
    }
}