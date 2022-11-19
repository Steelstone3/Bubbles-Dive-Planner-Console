using BubblesDivePlanner.Controllers;
using BubblesDivePlanner.Models;
using BubblesDivePlanner.Models.DiveModels;
using BubblesDivePlanner.Presenters;
using BubblesDivePlannerTests.Services;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.Controllers
{
    public class DiveControllerShould
    {
        private Mock<IDiveSetupPresenter> diveSetupPresenter = new();
        private readonly Mock<IDiveStepPresenter> diveStepPresenter = new();
        private readonly Mock<IDiveStagesController> diveStagesController = new();
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
            diveController.SetupDivePlan();

            // Then
            diveSetupPresenter.VerifyAll();
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
            var diveModel = new Zhl16Buhlmann();
            diveSetupPresenter.Setup(dsp => dsp.PrintDiveResults(diveModel.DiveProfile));
            diveController = new DiveController(diveStepPresenter.Object, diveSetupPresenter.Object, diveStagesController.Object);

            // When
            diveController.PrintDiveResults(diveModel.DiveProfile);

            // Then
            diveSetupPresenter.VerifyAll();
        }
    }
}