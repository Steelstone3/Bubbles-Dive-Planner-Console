using BubblesDivePlanner.Controllers.Json;
using BubblesDivePlanner.Models;
using BubblesDivePlanner.Presenters;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.Controllers.Json
{
    public class FileControllerShould
    {
        // TODO consider using stubbing the json controller to test the functionality all the way down
        // TODO smell assigning divePlans with a setter consider putting the divePlans into save directly to be passed down to the json controller
        private readonly Mock<IJsonController> jsonController = new();
        private readonly Mock<IPresenter> presenter = new();
        private IFileController fileController;

        [Fact]
        public void SaveFile()
        {
            // Given
            presenter.Setup(p => p.GetConfirmation("Save File?")).Returns(false);
            fileController = new FileController(presenter.Object, jsonController.Object);

            // When
            fileController.SaveFile();

            // Then
            presenter.VerifyAll();
        }

        [Fact]
        public void LoadFile()
        {
            // Given
            presenter.Setup(p => p.GetConfirmation("Load File?")).Returns(false);
            fileController = new FileController(presenter.Object, jsonController.Object);

            // When
            fileController.LoadFile();

            // Then
            presenter.VerifyAll();
        }

        [Fact]
        public void AcceptanceTest()
        {
            // Given
            var expectedDivePlan = new DivePlan(TestFixture.FixtureDiveModel, TestFixture.FixtureCylinders(), TestFixture.FixtureDiveStep, TestFixture.FixtureSelectedCylinder);
            presenter.Setup(p => p.GetConfirmation("Save File?")).Returns(true);
            presenter.Setup(p => p.GetConfirmation("Load File?")).Returns(true);
            fileController = new FileController(presenter.Object, new JsonController());

            // When
            fileController.AddDivePlan(expectedDivePlan);
            fileController.AddDivePlan(expectedDivePlan);
            fileController.SaveFile();
            var divePlan = fileController.LoadFile();

            // Then
            presenter.VerifyAll();
            Assert.Equal(expectedDivePlan.DiveModel.DiveProfile.OxygenPressureAtDepth, divePlan.DiveModel.DiveProfile.OxygenPressureAtDepth);
            Assert.Equal(expectedDivePlan.DiveModel.DiveProfile.HeliumPressureAtDepth, divePlan.DiveModel.DiveProfile.HeliumPressureAtDepth);
            Assert.Equal(expectedDivePlan.DiveModel.DiveProfile.NitrogenPressureAtDepth, divePlan.DiveModel.DiveProfile.NitrogenPressureAtDepth);
            Assert.Equal(expectedDivePlan.DiveModel.DiveProfile.NitrogenTissuePressures, divePlan.DiveModel.DiveProfile.NitrogenTissuePressures);
            Assert.Equal(expectedDivePlan.DiveModel.DiveProfile.HeliumTissuePressures, divePlan.DiveModel.DiveProfile.HeliumTissuePressures);
            Assert.Equal(expectedDivePlan.DiveModel.DiveProfile.TotalTissuePressures, divePlan.DiveModel.DiveProfile.TotalTissuePressures);
            Assert.Equal(expectedDivePlan.DiveModel.DiveProfile.MaxSurfacePressures, divePlan.DiveModel.DiveProfile.MaxSurfacePressures);
            Assert.Equal(expectedDivePlan.DiveModel.DiveProfile.ToleratedAmbientPressures, divePlan.DiveModel.DiveProfile.ToleratedAmbientPressures);
            Assert.Equal(expectedDivePlan.DiveModel.DiveProfile.CompartmentLoads, divePlan.DiveModel.DiveProfile.CompartmentLoads);

            for (int i = 0; i < expectedDivePlan.Cylinders.Count; i++)
            {
                Assert.Equal(expectedDivePlan.Cylinders[i].CylinderPressure, divePlan.Cylinders[i].CylinderPressure);
                Assert.Equal(expectedDivePlan.Cylinders[i].CylinderVolume, divePlan.Cylinders[i].CylinderVolume);
                Assert.Equal(expectedDivePlan.Cylinders[i].InitialPressurisedVolume, divePlan.Cylinders[i].InitialPressurisedVolume);
                Assert.Equal(expectedDivePlan.Cylinders[i].RemainingGas, divePlan.Cylinders[i].RemainingGas);
                Assert.Equal(expectedDivePlan.Cylinders[i].SurfaceAirConsumptionRate, divePlan.Cylinders[i].SurfaceAirConsumptionRate);
                Assert.Equal(expectedDivePlan.Cylinders[i].UsedGas, divePlan.Cylinders[i].UsedGas);
            }
        }
    }
}