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

        [Fact]
        public void CreateACylinder()
        {
            // Given
            presenter.Setup(p => p.GetByte("Enter Oxygen:"));
            presenter.Setup(p => p.GetByte("Enter Helium:"));
            presenter.Setup(p => p.GetUshort("Enter Cylinder Volume:"));
            presenter.Setup(p => p.GetUshort("Enter Cylinder Pressure:"));
            presenter.Setup(p => p.GetByte("Enter Surface Air Consumption Rate:"));
            diveSetup = new DiveSetup(presenter.Object);

            // When
            diveSetup.CreateCylinder();

            // Then
            presenter.VerifyAll();
        }
    }
}