using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BubblesDivePlanner.Controllers;
using BubblesDivePlanner.Models;
using BubblesDivePlanner.Models.Cylinders;
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
            divePlan.Setup(dp => dp.DiveModel).Returns(TestFixture.FixtureDiveModel(null));
            divePlan.Setup(dp => dp.DiveStep).Returns(TestFixture.FixtureDiveStep);
            divePlan.Setup(dp => dp.Cylinders).Returns(TestFixture.FixtureCylinders());
            divePresenter.Setup(dp => dp.ConfirmDecompression()).Returns(true);
            divePresenter.Setup(dp => dp.SelectCylinder(divePlan.Object.Cylinders)).Returns(TestFixture.FixtureSelectedCylinder);
            diveController.Setup(dc => dc.RunDiveProfile(divePlan.Object));

            decompressionController = new DecompressionController(divePresenter.Object, diveController.Object);

            // When
            decompressionController.RunDecompression(divePlan.Object);

            // Then
            divePresenter.VerifyAll();
        }

        [Theory(Skip="Not implemented yet")]
        [InlineData(12, 12)]
        [InlineData(11, 12)]
        [InlineData(5, 6)]
        [InlineData(6.1, 9)]
        [InlineData(10.6, 12)]
        public void CalculateNextDiveStep(double depthCeiling, byte depth)
        {
            // Given
            var diveStep = new DiveStep(depth, 1);
            decompressionController = new DecompressionController(divePresenter.Object, diveController.Object);
            
            // When
            var result = decompressionController.NextDiveStep(depthCeiling);

            // Then
            Assert.Equal(diveStep.Depth, result.Depth);
            Assert.Equal(diveStep.Time, result.Time);
        }
    }
}