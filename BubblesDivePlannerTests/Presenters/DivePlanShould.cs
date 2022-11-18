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

        [Fact]
        public void CreateCylinders()
        {
            // Given
            presenter.Setup(p => p.GetConfirmation("Create Another Cylinder?")).Returns(false);
            presenter.Setup(p => p.GetByte("Enter Oxygen:"));
            presenter.Setup(p => p.GetByte("Enter Helium:"));
            presenter.Setup(p => p.GetUshort("Enter Cylinder Volume:"));
            presenter.Setup(p => p.GetUshort("Enter Cylinder Pressure:"));
            presenter.Setup(p => p.GetByte("Enter Surface Air Consumption Rate:"));
            divePlan = new DivePlan(presenter.Object);

            // When
            divePlan.CreateCylinders();

            // Then
            presenter.VerifyAll();
        }
    }
}
