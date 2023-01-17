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
        public void DisplayResultOption()
        {
            // Given
            presenter.Setup(p => p.GetConfirmationDefaultNo("Use Simplified Display?"));
            divePresenter = new DivePresenter(presenter.Object);

            // When
            divePresenter.DisplayResultOption();

            // Then
            presenter.VerifyAll();
        }

        [Fact]
        public void CreateCylinders()
        {
            // Given
            presenter.Setup(p => p.GetConfirmationDefaultNo("Create Another Cylinder?")).Returns(false);
            presenter.Setup(p => p.GetString("Enter Cylinder Name:"));
            presenter.Setup(p => p.GetByte("Enter Oxygen (%):", 5, 100)).Returns(20);
            presenter.Setup(p => p.GetByte("Enter Helium (%):", 0, 80));
            presenter.Setup(p => p.GetUshort("Enter Cylinder Volume (l):", 3, 15));
            presenter.Setup(p => p.GetUshort("Enter Cylinder Pressure (bar):", 50, 300));
            presenter.Setup(p => p.GetByte("Enter Surface Air Consumption Rate (l/min):", 5, 30));
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
            presenter.Setup(p => p.GetConfirmationDefaultNo("Create Another Cylinder?")).Returns(false);
            presenter.Setup(p => p.GetString("Enter Cylinder Name:"));
            presenter.Setup(p => p.GetByte("Enter Oxygen (%):", 5, 100)).Returns(20);
            presenter.Setup(p => p.GetByte("Enter Helium (%):", 0, 80));
            presenter.Setup(p => p.GetUshort("Enter Cylinder Volume (l):", 3, 15));
            presenter.Setup(p => p.GetUshort("Enter Cylinder Pressure (bar):", 50, 300));
            presenter.Setup(p => p.GetByte("Enter Surface Air Consumption Rate (l/min):", 5, 30));
            divePresenter = new DivePresenter(presenter.Object);

            // When
            divePresenter.CreateCylinders(DiveModelNames.DCAP_MF11F6.ToString());

            // Then
            presenter.Verify(p => p.GetByte("Enter Helium (%):", 0, 80), Times.Never);
            presenter.Verify(p => p.GetConfirmationDefaultNo("Create Another Cylinder?"));
            presenter.Verify(p => p.GetString("Enter Cylinder Name:"));
            presenter.Verify(p => p.GetByte("Enter Oxygen (%):", 5, 100));
            presenter.Verify(p => p.GetUshort("Enter Cylinder Volume (l):", 3, 15));
            presenter.Verify(p => p.GetUshort("Enter Cylinder Pressure (bar):", 50, 300));
            presenter.Verify(p => p.GetByte("Enter Surface Air Consumption Rate (l/min):", 5, 30));
        }

        [Fact]
        public void CreateADiveStep()
        {
            // Given
            presenter.Setup(p => p.GetByte("Enter Depth (m):", 1, byte.MaxValue));
            presenter.Setup(p => p.GetByte("Enter Time (min):", 1, 60));
            divePresenter = new DivePresenter(presenter.Object);

            // When
            divePresenter.CreateDiveStep(-0.1, 255);

            // Then
            presenter.VerifyAll();
        }

        [Fact]
        public void ConfirmDecompression()
        {
            // Given
            presenter.Setup(p => p.GetConfirmationDefaultYes("Run Decompression Steps?"));
            divePresenter = new DivePresenter(presenter.Object);

            // When
            divePresenter.ConfirmDecompression(1.0);

            // Then
            presenter.VerifyAll();
        }

        [Fact]
        public void ConfirmContinueWithDive()
        {
            // Given
            presenter.Setup(p => p.GetConfirmationDefaultYes("Continue?"));
            divePresenter = new DivePresenter(presenter.Object);

            // When
            divePresenter.ConfirmContinueWithDive();

            // Then
            presenter.VerifyAll();
        }

        [Fact]
        public void DisplayHelp()
        {
            // Given
            presenter.Setup(p => p.GetConfirmationDefaultNo("Bubbles Dive Planner Help?")).Returns(true);
            presenter.Setup(p => p.GetConfirmationDefaultYes("Continue With Help?")).Returns(false);
            presenter.Setup(p => p.GenerateHelpMenu()).Returns("Loading A File");
            presenter.Setup(p => p.Print("Bubbles dive planner saves files in JSON format. The default file name is dive_plan.json. When running the application a choice of \"yes\" or \"no\" will be presented with the message \"Load File:\". Selecting yes will load the file contained in the base of the program file structure."));
            divePresenter = new DivePresenter(presenter.Object);

            // When
            divePresenter.DisplayHelp();

            // Then
            presenter.VerifyAll();
        }
    }
}