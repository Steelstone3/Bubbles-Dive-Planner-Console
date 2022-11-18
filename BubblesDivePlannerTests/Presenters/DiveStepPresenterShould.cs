using BubblesDivePlanner.Presenters;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.Presenters
{
    public class DiveSetupShould
    {
        private Mock<IPresenter> presenter = new();
        private IDiveStepPresenter diveStepPresenter;

        [Fact]
        public void CreateADiveStep()
        {
            // Given
            presenter.Setup(p => p.GetByte("Enter Depth:"));
            presenter.Setup(p => p.GetByte("Enter Time:"));
            diveStepPresenter = new DiveStepPresenter(presenter.Object);

            // When
            diveStepPresenter.CreateDiveStep();

            // Then
            presenter.VerifyAll();
        }
    }
}