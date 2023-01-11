using BubblesDivePlanner.Controllers;
using BubblesDivePlanner.Controllers.Json;
using BubblesDivePlanner.Models;
using BubblesDivePlanner.Models.Cylinders;
using BubblesDivePlanner.Presenters;
using Castle.Components.DictionaryAdapter.Xml;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.Controllers
{
    public class DecompressionControllerShould
    {
        private readonly Mock<IDivePlan> divePlan = new();
        private readonly Mock<IDivePresenter> divePresenter = new();
        private readonly Mock<IDiveController> diveController = new();
        private readonly Mock<IFileController> fileController = new();
        private IDecompressionController decompressionController;

        [Fact]
        public void RunADecompressionDiveProfile()
        {
            // Given
            divePlan.Setup(dp => dp.DiveModel).Returns(TestFixture.FixtureDiveModel(null));
            divePlan.Setup(dp => dp.DiveStep).Returns(TestFixture.FixtureDiveStep);
            divePlan.Setup(dp => dp.Cylinders).Returns(TestFixture.FixtureCylinders());
            divePresenter.Setup(dp => dp.ConfirmDecompression(0.0)).Returns(true);
            divePresenter.Setup(dp => dp.SelectCylinder(divePlan.Object.Cylinders)).Returns(TestFixture.FixtureSelectedCylinder);
            diveController.Setup(dc => dc.RunDiveProfile(divePlan.Object));
            decompressionController = new DecompressionController(divePresenter.Object, diveController.Object, fileController.Object);

            // When
            decompressionController.RunDecompression(divePlan.Object);

            // Then
            divePresenter.VerifyAll();
        }

        [Fact]
        public void HaveADepthCeilingLessThanZero()
        {
            // Given
            DivePlan divePlan = new(TestFixture.ExpectedDiveModel, TestFixture.FixtureCylinders(), TestFixture.FixtureDiveStep, TestFixture.FixtureSelectedCylinder);
            divePresenter.Setup(dp => dp.ConfirmDecompression(divePlan.DiveModel.DiveProfile.DepthCeiling)).Returns(true);
            divePresenter.Setup(dp => dp.SelectCylinder(divePlan.Cylinders)).Returns(TestFixture.FixtureSelectedCylinder);
            DiveController diveController = new(divePresenter.Object, new DiveStagesController());
            decompressionController = new DecompressionController(divePresenter.Object, diveController, fileController.Object);

            // When
            decompressionController.RunDecompression(divePlan);

            // Then
            Assert.Equal(-0.27, divePlan.DiveModel.DiveProfile.DepthCeiling);
        }
    }
}