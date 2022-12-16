using System.Collections.Generic;
using BubblesDivePlanner.Models;
using BubblesDivePlanner.Models.Cylinders;

namespace BubblesDivePlanner.Presenters
{
    public interface IDiveStepPresenter
    {
        IDiveStep CreateDiveStep(byte depthCeiling, byte maximumOperatingDepth);
        ICylinder SelectCylinder(List<ICylinder> cylinders);
    }
}