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

        [Fact]
        public void CalculateNextDiveStep()
        {
            // Given
            DivePlan divePlan = new(TestFixture.ExpectedDiveModel, TestFixture.FixtureCylinders(), TestFixture.FixtureDiveStep, TestFixture.FixtureSelectedCylinder);
            divePresenter.Setup(dp => dp.ConfirmDecompression()).Returns(true);
            divePresenter.Setup(dp => dp.SelectCylinder(divePlan.Cylinders)).Returns(TestFixture.FixtureSelectedCylinder);
            DiveController diveController = new(divePresenter.Object, new DiveStagesController());
            decompressionController = new DecompressionController(divePresenter.Object, diveController);

            // When
            var divePlans = decompressionController.RunDecompression(divePlan);

            // Then
            Assert.NotNull(divePlans);
            Assert.NotEmpty(divePlans);
            Assert.Equal(6, divePlans[0].DiveStep.Depth);
            Assert.Equal(1, divePlans[0].DiveStep.Time);
            Assert.Equivalent(new Cylinder("Air", 12, 200, 12, new GasMixture(21, 0), 2352, 12), divePlans[0].SelectedCylinder);
            Assert.Equal(3, divePlans[1].DiveStep.Depth);
            Assert.Equal(1, divePlans[1].DiveStep.Time);
            Assert.Equivalent(new Cylinder("Air", 12, 200, 12, new GasMixture(21, 0), 2352, 12), divePlans[1].SelectedCylinder);
            Assert.Equal(3, divePlans[2].DiveStep.Depth);
            Assert.Equal(1, divePlans[2].DiveStep.Time);
            Assert.Equivalent(new Cylinder("Air", 12, 200, 12, new GasMixture(21, 0), 2352, 12), divePlans[2].SelectedCylinder);
            Assert.Equal(3, divePlans[3].DiveStep.Depth);
            Assert.Equal(1, divePlans[3].DiveStep.Time);
            Assert.Equivalent(new Cylinder("Air", 12, 200, 12, new GasMixture(21, 0), 2352, 12), divePlans[3].SelectedCylinder);
        }
    }
}