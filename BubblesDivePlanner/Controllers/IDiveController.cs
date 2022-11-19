using BubblesDivePlanner.Models;

namespace BubblesDivePlanner.Controllers
{
    public interface IDiveController
    {
        IDivePlan Run(IDivePlan divePlan);
    }
}