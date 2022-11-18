using BubblesDivePlanner.Controllers;
using BubblesDivePlanner.Presenters;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.Services
{
    public class DiveServiceShould
    {
        private Mock<IDiveSetupPresenter> diveSetupPresenter = new Mock<IDiveSetupPresenter>();
        private Mock<IDiveStepPresenter> diveStepPresenter =new Mock<IDiveStepPresenter>();
        private Mock<IDiveController> diveController = new Mock<IDiveController>();
        private IDiveService diveService;

        [Fact]
        public void SetupADivePlan()
        {
            // Given
            diveSetupPresenter = new Mock<IDiveSetupPresenter>();
            diveSetupPresenter.Setup(dp => dp.WelcomeMessage());
            diveSetupPresenter.Setup(dp => dp.SelectDiveModel());
            diveSetupPresenter.Setup(dp => dp.CreateCylinders());
            diveService = new DiveService(diveStepPresenter.Object, diveSetupPresenter.Object, diveController.Object);

            // When
            diveService.SetupDivePlan();

            // Then
            diveSetupPresenter.VerifyAll();
        }

        [Fact]
        public void SetupADiveStep()
        {
            // Given
            diveStepPresenter = new Mock<IDiveStepPresenter>();
            diveStepPresenter.Setup(ds => ds.CreateDiveStep());
            diveStepPresenter.Setup(ds => ds.SelectCylinder(null));
            diveService = new DiveService(diveStepPresenter.Object, diveSetupPresenter.Object, diveController.Object);

            // When
            diveService.SetupDiveStep();

            // Then
            diveStepPresenter.VerifyAll();
        }

        [Fact]
        public void RunADiveProfile()
        {
            // Given
            diveController = new Mock<IDiveController>();
            diveController.Setup(dc => dc.Run());
            diveService = new DiveService(diveStepPresenter.Object, diveSetupPresenter.Object, diveController.Object);
            // When
            diveService.RunDiveProfile();

            // Then
            diveController.VerifyAll();
        }
    }
}