using BubblesDivePlanner.Models;
using BubblesDivePlanner.Models.Cylinders;

namespace BubblesDivePlanner.Presenters
{
    public interface IDiveSetup
    {
        void WelcomeMessage();
        DiveStep CreateDiveStep();
        Cylinder CreateCylinder();
    }
}