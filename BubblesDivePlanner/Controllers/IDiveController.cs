using BubblesDivePlanner.Models;
using BubblesDivePlanner.Models.DiveModels;

namespace BubblesDivePlanner.Controllers
{
    public interface IDiveController
    {
        void SetupDivePlan();
        IDivePlan SetupDiveStep();
        IDivePlan RunDiveProfile(IDivePlan divePlan);
        void PrintDiveResults(IDiveProfile diveProfile);
    }
}