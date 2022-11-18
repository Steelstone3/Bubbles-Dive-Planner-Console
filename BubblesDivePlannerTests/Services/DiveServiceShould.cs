using BubblesDivePlanner.Presenters;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.Services
{
    public class DiveServiceShould
    {
        private Mock<IDiveSetupPresenter> diveSetupPresenter;
        private Mock<IDiveStepPresenter> diveStepPresenter;

        public DiveServiceShould()
        {
            diveSetupPresenter = new Mock<IDiveSetupPresenter>();
            diveStepPresenter = new Mock<IDiveStepPresenter>();
        }

        [Fact]
        public void SetupADivePlan()
        {
            // Given
            diveSetupPresenter = new Mock<IDiveSetupPresenter>();
            diveSetupPresenter.Setup(dp => dp.WelcomeMessage());
            diveSetupPresenter.Setup(dp => dp.SelectDiveModel());
            diveSetupPresenter.Setup(dp => dp.CreateCylinders());
            IDiveService diveService = new DiveService(diveStepPresenter.Object, diveSetupPresenter.Object);

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
            IDiveService diveService = new DiveService(diveStepPresenter.Object, diveSetupPresenter.Object);

            // When
            diveService.SetupDiveStep();

            // Then
            diveStepPresenter.VerifyAll();
        }

        [Fact(Skip = "Not implemented")]
        public void RunADiveProfile()
        {
            // Given

            // When

            // Then
        }
    }
}