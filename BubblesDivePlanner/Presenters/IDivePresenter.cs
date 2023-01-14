using System.Collections.Generic;
using BubblesDivePlanner.Models;
using BubblesDivePlanner.Models.Cylinders;
using BubblesDivePlanner.Models.DiveModels;

namespace BubblesDivePlanner.Presenters
{
    public interface IDivePresenter
    {
        void WelcomeMessage();
        void DisplayResultOption();
        IDiveModel SelectDiveModel();
        List<ICylinder> CreateCylinders(string diveModelName);
        IDiveStep CreateDiveStep(byte depthCeiling, byte maximumOperatingDepth);
        ICylinder SelectCylinder(List<ICylinder> cylinders);
        void PrintDiveResult(IDivePlan divePlan);
        bool ConfirmDecompression(double depthCeiling);
        bool ConfirmContinueWithDive();
        void DisplayHelp();
    }
}