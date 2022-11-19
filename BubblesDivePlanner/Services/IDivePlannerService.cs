using BubblesDivePlanner.Presenters;

namespace BubblesDivePlannerTests.Services
{
    public interface IDivePlannerService
    {
        void Run(IPresenter presenter);
    }
}