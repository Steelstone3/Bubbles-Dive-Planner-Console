using BubblesDivePlanner.Presenters;
using Moq;
using Xunit;

namespace Name
{
    public class DivePlanShould
    {
        private Mock<IPresenter> presenter = new();
        private IDivePlan divePlan;

        [Fact]
        public void DisplayAWelcomeMessage()
        {
            // Given
            presenter.Setup(p => p.Print("Bubbles Dive Planner Console"));
            divePlan = new DivePlan(presenter.Object);

            // When
            divePlan.WelcomeMessage();

            // Then
            presenter.VerifyAll();
        }
    }
}
