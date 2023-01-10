using BubblesDivePlanner.Models;

namespace BubblesDivePlanner.Controllers
{
    public interface IDecompressionController
    {
        IDiveStep NextDiveStep(double depthCeiling);
        void RunDecompression(IDivePlan divePlan);
    }
}