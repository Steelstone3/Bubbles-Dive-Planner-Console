using BubblesDivePlanner.Controllers;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.Controllers
{
    public class DecompressionControllerShould
    {
        private readonly Mock<IDiveController> diveController = new();
        private IDecompressionController decompressionController;

        [Fact(Skip="To implement next. Need to ask the question do you want to decompress? Then select a cylinder providing the dive plan whereby it will run a decompression algorithm")]
        public void RunADecompressionDiveProfile()
        {
            // Given
            // diveController.Object
            decompressionController = new DecompressionController();

            // When
        
            // Then
        }
    }
}