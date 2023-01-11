using System.Collections.Generic;
using BubblesDivePlanner.Models;

namespace BubblesDivePlanner.Controllers
{
    public interface IDecompressionController
    {
        void RunDecompression(IDivePlan divePlan);
    }
}