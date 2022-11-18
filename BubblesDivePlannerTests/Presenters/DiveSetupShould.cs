using BubblesDivePlanner.Presenters;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.Presenters
{
    public class DiveSetupShould
    {
        private Mock<IPresenter> presenter = new();
        private IDiveSetup diveSetup;

        [Fact]
        public void CreateADiveStep()
        {
            // Given
            presenter.Setup(p => p.GetByte("Enter Depth:"));
            presenter.Setup(p => p.GetByte("Enter Time:"));
            diveSetup = new DiveSetup(presenter.Object);

            // When
            diveSetup.CreateDiveStep();

            // Then
            presenter.VerifyAll();
        }
    }
}