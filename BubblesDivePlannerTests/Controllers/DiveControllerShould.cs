using BubblesDivePlanner.Controllers;
using BubblesDivePlanner.Models;
using BubblesDivePlanner.Models.Cylinders;
using BubblesDivePlanner.Models.DiveModels;
using BubblesDivePlanner.Models.DiveModels.Types;
using BubblesDivePlanner.Presenters;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.Controllers
{
    public class DiveControllerShould
    {
        private readonly Mock<IDivePresenter> divePresenter = new();
        private readonly Mock<IDiveStagesController> diveStagesController = new();
        private readonly Mock<IDivePlan> divePlan = new();
        private IDiveController diveController;

        [Fact]
        public void SetupADivePlan()
        {
            // Given
            var diveModel = new Zhl12Buhlmann(null);
            divePresenter.Setup(dp => dp.WelcomeMessage());
            divePresenter.Setup(dp => dp.SelectDiveModel()).Returns(diveModel);
            divePresenter.Setup(dp => dp.CreateCylinders(diveModel.Name));
            diveController = new DiveController(divePresenter.Object, diveStagesController.Object);

            // When
            diveController.SetupDivePlan(null);

            // Then
            divePresenter.VerifyAll();
        }

        [Fact]
        public void LoadFromFileDivePlan()
        {
            // Given
            divePlan.Setup(dp => dp.DiveModel).Returns(TestFixture.FixtureDiveModel(null));
            divePlan.Setup(dp => dp.Cylinders).Returns(TestFixture.FixtureCylinders());
            divePresenter.Setup(dp => dp.WelcomeMessage());
            divePresenter.Setup(dp => dp.SelectDiveModel());
            divePresenter.Setup(dp => dp.CreateCylinders(DiveModelNames.ZHL16_B.ToString()));
            diveController = new DiveController(divePresenter.Object, diveStagesController.Object);

            // When
            diveController.SetupDivePlan(divePlan.Object);

            // Then
            divePresenter.Verify(dsp => dsp.WelcomeMessage(), Times.Once);
            divePresenter.Verify(dsp => dsp.SelectDiveModel(), Times.Never);
            divePresenter.Verify(dsp => dsp.CreateCylinders(DiveModelNames.ZHL16_B.ToString()), Times.Never);
        }

        [Fact]
        public void LoadDiveModelFromFileDivePlan()
        {
            // Given
            divePlan.Setup(dp => dp.DiveModel).Returns(TestFixture.FixtureDiveModel(null));
            divePresenter.Setup(dp => dp.WelcomeMessage());
            divePresenter.Setup(dp => dp.SelectDiveModel()).Returns(new Zhl16Buhlmann(null));
            divePresenter.Setup(dp => dp.CreateCylinders(DiveModelNames.ZHL16_B.ToString()));
            diveController = new DiveController(divePresenter.Object, diveStagesController.Object);

            // When
            diveController.SetupDivePlan(divePlan.Object);

            // Then
            divePresenter.VerifyAll();
        }

        [Fact]
        public void LoadCylindersFromFileDivePlan()
        {
            // Given
            divePlan.Setup(dp => dp.Cylinders).Returns(TestFixture.FixtureCylinders());
            divePresenter.Setup(dp => dp.WelcomeMessage());
            divePresenter.Setup(dp => dp.SelectDiveModel()).Returns(new Zhl16Buhlmann(null));
            divePresenter.Setup(dp => dp.CreateCylinders(DiveModelNames.ZHL16_B.ToString()));
            diveController = new DiveController(divePresenter.Object, diveStagesController.Object);

            // When
            diveController.SetupDivePlan(divePlan.Object);

            // Then
            divePresenter.VerifyAll();
        }

        [Fact]
        public void SetupADiveStep()
        {
            // Given
            divePlan.Setup(dp => dp.DiveModel).Returns(TestFixture.FixtureDiveModel(null));
            divePresenter.Setup(ds => ds.CreateDiveStep(0, 100));
            divePresenter.Setup(ds => ds.SelectCylinder(null));
            diveController = new DiveController(divePresenter.Object, diveStagesController.Object);

            // When
            diveController.SetupDiveStep(0);

            // Then
            divePresenter.VerifyAll();
        }

        [Fact]
        public void SetupADiveStepWithASelectedCylinder()
        {
            // Given
            Mock<IGasMixture> gasMixture = new();
            gasMixture.Setup(gm => gm.MaximumOperatingDepth).Returns(56.67);
            Mock<ICylinder> cylinder = new();
            cylinder.Setup(c => c.GasMixture).Returns(gasMixture.Object);
            divePresenter.Setup(ds => ds.CreateDiveStep(0, 56));
            divePresenter.Setup(ds => ds.SelectCylinder(null)).Returns(cylinder.Object);
            diveController = new DiveController(divePresenter.Object, diveStagesController.Object);

            // When
            diveController.SetupDiveStep(0);

            // Then
            divePresenter.VerifyAll();
        }

        [Fact]
        public void SetupADiveStepWithASelectedCylinderWithAMaximumOperatingDepthGreaterThanMaximumDepth()
        {
            // Given
            Mock<IGasMixture> gasMixture = new();
            gasMixture.Setup(gm => gm.MaximumOperatingDepth).Returns(200);
            Mock<ICylinder> cylinder = new();
            cylinder.Setup(c => c.GasMixture).Returns(gasMixture.Object);
            divePresenter.Setup(ds => ds.CreateDiveStep(0, 100));
            divePresenter.Setup(ds => ds.SelectCylinder(null)).Returns(cylinder.Object);
            diveController = new DiveController(divePresenter.Object, diveStagesController.Object);

            // When
            diveController.SetupDiveStep(0);

            // Then
            divePresenter.VerifyAll();
        }

        [Fact]
        public void RunADiveProfile()
        {
            // Given
            var divePlan = new Mock<IDivePlan>();
            diveStagesController.Setup(dc => dc.Run(divePlan.Object));
            diveController = new DiveController(divePresenter.Object, diveStagesController.Object);
            
            // When
            diveController.RunDiveProfile(divePlan.Object);

            // Then
            diveStagesController.VerifyAll();
        }

        [Fact]
        public void PrintDiveResults()
        {
            // Given
            var divePlan = new DivePlan(TestFixture.FixtureDiveModel(null), TestFixture.FixtureCylinders(), TestFixture.FixtureDiveStep, TestFixture.FixtureSelectedCylinder);
            divePresenter.Setup(dsp => dsp.PrintDiveResults(divePlan));
            diveController = new DiveController(divePresenter.Object, diveStagesController.Object);

            // When
            diveController.PrintDiveResults(divePlan);

            // Then
            divePresenter.VerifyAll();
        }
    }
}