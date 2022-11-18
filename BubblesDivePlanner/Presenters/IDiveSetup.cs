using BubblesDivePlanner.Models;
using BubblesDivePlanner.Models.Cylinders;

namespace BubblesDivePlanner.Presenters
{
    public interface IDiveSetup
    {
        DiveStep CreateDiveStep();
        Cylinder CreateCylinder();
    }
}