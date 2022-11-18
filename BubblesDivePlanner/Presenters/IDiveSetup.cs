using System.Collections.Generic;
using BubblesDivePlanner.Models;
using BubblesDivePlanner.Models.Cylinders;

namespace BubblesDivePlanner.Presenters
{
    public interface IDiveSetup
    {
        IDiveStep CreateDiveStep();
        ICylinder SelectCylinder(List<ICylinder> cylinders);
    }
}