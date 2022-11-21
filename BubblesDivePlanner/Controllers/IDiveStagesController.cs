using BubblesDivePlanner.Models.DivePlans;

namespace BubblesDivePlanner.Controllers
{
    public interface IDiveStagesController
    {
        IDivePlan Run(IDivePlan divePlan);
    }
}