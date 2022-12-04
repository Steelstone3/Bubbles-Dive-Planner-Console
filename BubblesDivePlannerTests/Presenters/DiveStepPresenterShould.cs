using BubblesDivePlanner.Presenters;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.Presenters
{
    public class DiveSetupShould
    {
        private readonly Mock<IPresenter> presenter = new();
        private IDiveStepPresenter diveStepPresenter;

        [Fact]
        public void CreateADiveStep()
        {
            // Given
            presenter.Setup(p => p.GetByte("Enter Depth:", 0, 56));
            presenter.Setup(p => p.GetByte("Enter Time:", 1, 60));
            diveStepPresenter = new DiveStepPresenter(presenter.Object);

            // When
            diveStepPresenter.CreateDiveStep(56);

            // Then
            presenter.VerifyAll();
        }
    }
}