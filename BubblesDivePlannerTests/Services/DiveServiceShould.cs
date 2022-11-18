using BubblesDivePlanner.Presenters;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.Services
{
    public class DiveServiceShould
    {
        private Mock<IDivePlan> divePlan;
        private Mock<IDiveSetup> diveSetup;

        public DiveServiceShould()
        {
            divePlan = new Mock<IDivePlan>();
            diveSetup = new Mock<IDiveSetup>();
        }

        [Fact]
        public void SetupADivePlan()
        {
            // Given
            divePlan = new Mock<IDivePlan>();
            divePlan.Setup(dp => dp.WelcomeMessage());
            divePlan.Setup(dp => dp.SelectDiveModel());
            IDiveService diveService = new DiveService(diveSetup.Object, divePlan.Object);

            // When
            diveService.SetupDivePlan();

            // Then
            divePlan.VerifyAll();
        }

        [Fact(Skip="Next")]
        public void SetupADiveStep()
        {
            // Given


            // When
        
            // Then
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