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
            presenter.Setup(p => p.GetByte("Enter Oxygen:"));
            presenter.Setup(p => p.GetByte("Enter Helium:"));
            presenter.Setup(p => p.GetUshort("Enter Cylinder Volume:"));
            presenter.Setup(p => p.GetUshort("Enter Cylinder Pressure:"));
            presenter.Setup(p => p.GetByte("Enter Surface Air Consumption Rate:"));
            diveSetupPresenter = new DiveSetupPresenter(presenter.Object);

            // When
            diveSetupPresenter.CreateCylinders();

            // Then
            presenter.VerifyAll();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        [InlineData(11)]
        [InlineData(12)]
        [InlineData(13)]
        [InlineData(14)]
        [InlineData(15)]
        public void PrintDiveResults(int compartment)
        {
            // Given
            var divePlan = new DivePlan(TestFixture.FixtureDiveModel, TestFixture.FixtureCylinders(), TestFixture.FixtureDiveStep, TestFixture.FixtureSelectedCylinder);
            var diveProfile = divePlan.DiveModel.DiveProfile;
            var selectedCylinder = divePlan.SelectedCylinder;
            presenter.Setup(p => p.Print($"Dive Model: {divePlan.DiveModel.Name}"));
            presenter.Setup(p => p.Print($"| C: {compartment + 1} | TPt: {diveProfile.TotalTissuePressures[compartment]} | TAP: {diveProfile.ToleratedAmbientPressures[compartment]} | MSP: {diveProfile.MaxSurfacePressures[compartment]} | CLp: {diveProfile.CompartmentLoads[compartment]} |"));
            presenter.Setup(p => p.Print($"| Cylinder: {selectedCylinder.Name} | Initial Pressurised Volume: {selectedCylinder.InitialPressurisedVolume} | Remaining Gas: {selectedCylinder.RemainingGas} | Used Gas: {selectedCylinder.UsedGas} | Oxygen: {selectedCylinder.GasMixture.Oxygen}% | Nitrogen: {selectedCylinder.GasMixture.Nitrogen}% | Helium: {selectedCylinder.GasMixture.Helium}% |"));
            diveSetupPresenter = new DiveSetupPresenter(presenter.Object);

            // When
            diveSetupPresenter.PrintDiveResults(divePlan);

            // Then
            presenter.VerifyAll();
        }
    }
}
