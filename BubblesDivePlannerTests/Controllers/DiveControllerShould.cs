using BubblesDivePlanner.Controllers;
using BubblesDivePlanner.Models;
using BubblesDivePlanner.Models.Cylinders;
using BubblesDivePlanner.Models.DiveModels;
using BubblesDivePlanner.Models.DivePlans;
using BubblesDivePlanner.Presenters;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.Controllers
{
    public class DiveControllerShould
    {
        private Mock<IDiveSetupPresenter> diveSetupPresenter = new();
        private readonly Mock<IDiveStepPresenter> diveStepPresenter = new();
        private readonly Mock<IDiveStagesController> diveStagesController = new();
        private readonly Mock<IDivePlan> divePlan = new();
        private IDiveController diveController;

        [Fact]
        public void SetupADivePlan()
        {
            // Given
            diveSetupPresenter = new Mock<IDiveSetupPresenter>();
            diveSetupPresenter.Setup(dp => dp.WelcomeMessage());
            diveSetupPresenter.Setup(dp => dp.SelectDiveModel());
            diveSetupPresenter.Setup(dp => dp.CreateCylinders());
            diveController = new DiveController(diveStepPresenter.Object, diveSetupPresenter.Object, diveStagesController.Object);

            // When
            diveController.SetupDivePlan(null);

            // Then
            diveSetupPresenter.VerifyAll();
        }

        [Fact]
        public void LoadFromFileDivePlan()
        {
            // Given
            divePlan.Setup(dp => dp.DiveModel).Returns(TestFixture.FixtureDiveModel);
            divePlan.Setup(dp => dp.Cylinders).Returns(TestFixture.FixtureCylinders());
            diveSetupPresenter = new Mock<IDiveSetupPresenter>();
            diveSetupPresenter.Setup(dp => dp.WelcomeMessage());
            diveSetupPresenter.Setup(dp => dp.SelectDiveModel());
            diveSetupPresenter.Setup(dp => dp.CreateCylinders());
            diveController = new DiveController(diveStepPresenter.Object, diveSetupPresenter.Object, diveStagesController.Object);

            // When
            diveController.SetupDivePlan(divePlan.Object);

            // Then
            diveSetupPresenter.Verify(dsp => dsp.WelcomeMessage(), Times.Once);
            diveSetupPresenter.Verify(dsp => dsp.SelectDiveModel(), Times.Never);
            diveSetupPresenter.Verify(dsp => dsp.CreateCylinders(), Times.Never);
        }

        [Fact]
        public void LoadDiveModelFromFileDivePlan()
        {
            // Given
            divePlan.Setup(dp => dp.DiveModel).Returns(TestFixture.FixtureDiveModel);
            diveSetupPresenter = new Mock<IDiveSetupPresenter>();
            diveSetupPresenter.Setup(dp => dp.WelcomeMessage());
            diveSetupPresenter.Setup(dp => dp.SelectDiveModel());
            diveSetupPresenter.Setup(dp => dp.CreateCylinders());
            diveController = new DiveController(diveStepPresenter.Object, diveSetupPresenter.Object, diveStagesController.Object);

            // When
            diveController.SetupDivePlan(divePlan.Object);

            // Then
            diveSetupPresenter.Verify(dsp => dsp.WelcomeMessage(), Times.Once);
            diveSetupPresenter.Verify(dsp => dsp.CreateCylinders(), Times.Once);
            diveSetupPresenter.Verify(dsp => dsp.SelectDiveModel(), Times.Never);
        }

        [Fact]
        public void LoadCylindersFromFileDivePlan()
        {
            // Given
            divePlan.Setup(dp => dp.Cylinders).Returns(TestFixture.FixtureCylinders());
            diveSetupPresenter = new Mock<IDiveSetupPresenter>();
            diveSetupPresenter.Setup(dp => dp.WelcomeMessage());
            diveSetupPresenter.Setup(dp => dp.SelectDiveModel());
            diveSetupPresenter.Setup(dp => dp.CreateCylinders());
            diveController = new DiveController(diveStepPresenter.Object, diveSetupPresenter.Object, diveStagesController.Object);

            // When
            diveController.SetupDivePlan(divePlan.Object);

            // Then
            diveSetupPresenter.Verify(dsp => dsp.WelcomeMessage(), Times.Once);
            diveSetupPresenter.Verify(dsp => dsp.SelectDiveModel(), Times.Once);
            diveSetupPresenter.Verify(dsp => dsp.CreateCylinders(), Times.Never);
        }

        [Fact]
        public void SetupADiveStep()
        {
            // Given
            diveStepPresenter.Setup(ds => ds.CreateDiveStep());
            diveStepPresenter.Setup(ds => ds.SelectCylinder(null));
            diveController = new DiveController(diveStepPresenter.Object, diveSetupPresenter.Object, diveStagesController.Object);

            // When
            diveController.SetupDiveStep();

            // Then
            diveStepPresenter.VerifyAll();
        }

        [Fact]
        public void RunADiveProfile()
        {
            // Given
            var divePlan = new Mock<IDivePlan>();
            diveStagesController.Setup(dc => dc.Run(divePlan.Object));
            diveController = new DiveController(diveStepPresenter.Object, diveSetupPresenter.Object, diveStagesController.Object);
            // When
            diveController.RunDiveProfile(divePlan.Object);

            // Then
            diveStagesController.VerifyAll();
        }

        [Fact]
        public void PrintDiveResults()
        {
            // Given
            var divePlan = new DivePlan(TestFixture.FixtureDiveModel, TestFixture.FixtureCylinders(), TestFixture.FixtureDiveStep, TestFixture.FixtureSelectedCylinder);
            diveSetupPresenter.Setup(dsp => dsp.PrintDiveResults(divePlan));
            diveController = new DiveController(diveStepPresenter.Object, diveSetupPresenter.Object, diveStagesController.Object);

            // When
            diveController.PrintDiveResults(divePlan);

            // Then
            diveSetupPresenter.VerifyAll();
        }
    }
}