using BubblesDivePlanner.Models;

namespace BubblesDivePlanner.Controllers.Json
{
    public interface IFileController
    {
        IDivePlan LoadFile();
        void SaveFile();
    }
}