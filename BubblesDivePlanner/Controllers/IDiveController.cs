using BubblesDivePlanner.Models;
using BubblesDivePlanner.Models.Cylinders;
using BubblesDivePlanner.Models.DiveModels;

namespace BubblesDivePlanner.Controllers
{
    public interface IDiveController
    {
        void SetupDivePlan();
        IDivePlan SetupDiveStep();
        IDivePlan RunDiveProfile(IDivePlan divePlan);
        void PrintDiveResults(IDiveModel diveModel);
        void RunGasManagement(ICylinder selectedCylinder, IDiveStep diveStep);
        void PrintCylinder(ICylinder selectedCylinder);
    }
}