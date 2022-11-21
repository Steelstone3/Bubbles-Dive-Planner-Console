using BubblesDivePlanner.Models.DivePlans;

namespace BubblesDivePlanner.Controllers.Json
{
    public interface IFileController
    {
        IDivePlan LoadFile();
        void SaveFile(IDivePlan divePlan);
    }
}