using BubblesDivePlanner.Models.DiveModels;
using BubblesDivePlanner.Presenters;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.Presenters
{
    public class DivePresenterShould
    {
        private readonly Mock<IPresenter> presenter = new();
        private IDivePresenter divePresenter;

        [Fact]
        public void CreateADiveStep()
        {
            // Given
            presenter.Setup(p => p.GetByte("Enter Depth:", 0, 56));
            presenter.Setup(p => p.GetByte("Enter Time:", 1, 60));
            divePresenter = new DivePresenter(presenter.Object);

            // When
            divePresenter.CreateDiveStep(0, 56);

            // Then
            presenter.VerifyAll();
        }

         [Fact]
        public void DisplayAWelcomeMessage()
        {
            // Given
            presenter.Setup(p => p.Print("Bubbles Dive Planner Console"));
            divePresenter = new DivePresenter(presenter.Object);

            // When
            divePresenter.WelcomeMessage();

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
            divePresenter = new DivePresenter(presenter.Object);

            // When
            divePresenter.CreateCylinders(DiveModelNames.ZHL16_B.ToString());

            // Then
            presenter.VerifyAll();
        }

        [Fact]
        public void CreateCylindersWithoutHeliox()
        {
            // Given
            presenter.Setup(p => p.GetConfirmation("Create Another Cylinder?")).Returns(false);
            presenter.Setup(p => p.GetString("Enter Cylinder Name:"));
            presenter.Setup(p => p.GetByte("Enter Oxygen:", 5, 100)).Returns(20);
            presenter.Setup(p => p.GetByte("Enter Helium:", 0, 80));
            presenter.Setup(p => p.GetUshort("Enter Cylinder Volume:", 3, 15));
            presenter.Setup(p => p.GetUshort("Enter Cylinder Pressure:", 50, 300));
            presenter.Setup(p => p.GetByte("Enter Surface Air Consumption Rate:", 5, 30));
            divePresenter = new DivePresenter(presenter.Object);

            // When
            divePresenter.CreateCylinders(DiveModelNames.DCAP_MF11F6.ToString());

            // Then
            presenter.Verify(p => p.GetByte("Enter Helium:", 0, 80), Times.Never);
            presenter.Verify(p => p.GetConfirmation("Create Another Cylinder?"));
            presenter.Verify(p => p.GetString("Enter Cylinder Name:"));
            presenter.Verify(p => p.GetByte("Enter Oxygen:", 5, 100));
            presenter.Verify(p => p.GetUshort("Enter Cylinder Volume:", 3, 15));
            presenter.Verify(p => p.GetUshort("Enter Cylinder Pressure:", 50, 300));
            presenter.Verify(p => p.GetByte("Enter Surface Air Consumption Rate:", 5, 30));
        }
    }
}