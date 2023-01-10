using BubblesDivePlanner.Controllers;
using BubblesDivePlanner.Models;
using BubblesDivePlanner.Presenters;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.Controllers
{
    public class DecompressionControllerShould
    {
        private readonly Mock<IDivePlan> divePlan = new();
        private readonly Mock<IDivePresenter> divePresenter = new();
        private readonly Mock<IDiveController> diveController = new();
        private IDecompressionController decompressionController;

        [Fact]
        public void RunADecompressionDiveProfile()
        {
            // Given
            divePresenter.Setup(dp => dp.ConfirmDecompression()).Returns(true);
            decompressionController = new DecompressionController(divePresenter.Object, diveController.Object);

            // When
            decompressionController.RunDecompression();

            // Then
            divePresenter.VerifyAll();
        }
    }
}