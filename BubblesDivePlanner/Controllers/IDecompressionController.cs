using System.Collections.Generic;
using BubblesDivePlanner.Models;

namespace BubblesDivePlanner.Controllers
{
    public interface IDecompressionController
    {
        List<IDivePlan> RunDecompression(IDivePlan divePlan);
    }
}