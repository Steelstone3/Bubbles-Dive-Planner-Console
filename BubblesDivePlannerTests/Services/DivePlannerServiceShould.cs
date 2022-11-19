using BubblesDivePlanner.Controllers;
using BubblesDivePlanner.Models;
using BubblesDivePlanner.Models.DiveModels;
using BubblesDivePlanner.Presenters;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.Services
{
    public class DiveServiceShould
    {
        private Mock<IDiveSetupPresenter> diveSetupPresenter = new Mock<IDiveSetupPresenter>();
        private Mock<IDiveStepPresenter> diveStepPresenter = new Mock<IDiveStepPresenter>();
        private Mock<IDiveController> diveController = new Mock<IDiveController>();
        private IDivePlannerService divePlannerService;

        [Fact]
        public void SetupADivePlan()
        {
            // Given
            diveSetupPresenter = new Mock<IDiveSetupPresenter>();
            diveSetupPresenter.Setup(dp => dp.WelcomeMessage());
            diveSetupPresenter.Setup(dp => dp.SelectDiveModel());
            diveSetupPresenter.Setup(dp => dp.CreateCylinders());
            divePlannerService = new DivePlannerService(diveStepPresenter.Object, diveSetupPresenter.Object, diveController.Object);

            // When
            divePlannerService.SetupDivePlan();

            // Then
            diveSetupPresenter.VerifyAll();
        }

        [Fact]
        public void SetupADiveStep()
        {
            // Given
            diveStepPresenter.Setup(ds => ds.CreateDiveStep());
            diveStepPresenter.Setup(ds => ds.SelectCylinder(null));
            divePlannerService = new DivePlannerService(diveStepPresenter.Object, diveSetupPresenter.Object, diveController.Object);

            // When
            divePlannerService.SetupDiveStep();

            // Then
            diveStepPresenter.VerifyAll();
        }

        [Fact]
        public void RunADiveProfile()
        {
            // Given
            var divePlan = new Mock<IDivePlan>();
            diveController.Setup(dc => dc.Run(divePlan.Object));
            divePlannerService = new DivePlannerService(diveStepPresenter.Object, diveSetupPresenter.Object, diveController.Object);
            // When
            divePlannerService.RunDiveProfile(divePlan.Object);

            // Then
            diveController.VerifyAll();
        }

        [Fact]
        public void PrintDiveResults()
        {
            // Given
            var diveModel = new Zhl16Buhlmann();
            diveSetupPresenter.Setup(dsp => dsp.PrintDiveResults(diveModel.DiveProfile));
            divePlannerService = new DivePlannerService(diveStepPresenter.Object, diveSetupPresenter.Object, diveController.Object);

            // When
            divePlannerService.PrintDiveResults(diveModel.DiveProfile);

            // Then
            diveSetupPresenter.VerifyAll();
        }
    }
}