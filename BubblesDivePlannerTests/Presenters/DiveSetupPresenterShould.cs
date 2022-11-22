using BubblesDivePlanner.Models;
using BubblesDivePlanner.Presenters;
using BubblesDivePlannerTests;
using Moq;
using Xunit;

namespace Name
{
    public class DivePlanShould
    {
        private readonly Mock<IPresenter> presenter = new();
        private IDiveSetupPresenter diveSetupPresenter;

        [Fact]
        public void DisplayAWelcomeMessage()
        {
            // Given
            presenter.Setup(p => p.Print("Bubbles Dive Planner Console"));
            diveSetupPresenter = new DiveSetupPresenter(presenter.Object);

            // When
            diveSetupPresenter.WelcomeMessage();

            // Then
            presenter.VerifyAll();
        }

        [Fact]
        public void CreateCylinders()
        {
            // Given
            presenter.Setup(p => p.GetConfirmation("Create Another Cylinder?")).Returns(false);
            presenter.Setup(p => p.GetString("Enter Cylinder Name:"));
            presenter.Setup(p => p.GetByte("Enter Oxygen:", 5, 100)).Returns(20);
            presenter.Setup(p => p.GetByte("Enter Helium:", 0, 80));
            presenter.Setup(p => p.GetUshort("Enter Cylinder Volume:", 3, 15));
            presenter.Setup(p => p.GetUshort("Enter Cylinder Pressure:", 50, 300));
            presenter.Setup(p => p.GetByte("Enter Surface Air Consumption Rate:", 5, 30));
            diveSetupPresenter = new DiveSetupPresenter(presenter.Object);

            // When
            diveSetupPresenter.CreateCylinders();

            // Then
            presenter.VerifyAll();
        }

        [Fact]
        public void PrintDiveResults()
        {
            // Given
            var divePlan = new DivePlan(TestFixture.FixtureDiveModel, TestFixture.FixtureCylinders(), TestFixture.FixtureDiveStep, TestFixture.FixtureSelectedCylinder);
            var diveProfile = divePlan.DiveModel.DiveProfile;
            var selectedCylinder = divePlan.SelectedCylinder;
            presenter.Setup(p => p.Print($"Dive Model: {divePlan.DiveModel.Name}"));
            presenter.Setup(p => p.Print("Cylinders:"));
            diveSetupPresenter = new DiveSetupPresenter(presenter.Object);

            // When
            diveSetupPresenter.PrintDiveResults(divePlan);

            // Then
            presenter.VerifyAll();
        }
    }
}
