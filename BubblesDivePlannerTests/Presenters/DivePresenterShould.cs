using System.Collections.Generic;
using BubblesDivePlanner.Models;
using BubblesDivePlanner.Models.Cylinders;
using BubblesDivePlanner.Models.DiveModels;
using BubblesDivePlanner.Models.DiveModels.Types;
using BubblesDivePlanner.Presenters;
using Moq;
using Spectre.Console;
using Xunit;

namespace BubblesDivePlannerTests.Presenters
{
    public class DivePresenterShould
    {
        private readonly Mock<IPresenter> presenter = new();
        private IDivePresenter divePresenter;

        #region Welcome

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
        public void DisplayHelp()
        {
            // Given
            presenter.Setup(p => p.GetConfirmationDefaultNo("Bubbles Dive Planner Help?")).Returns(true);
            presenter.Setup(p => p.GetConfirmationDefaultYes("Continue With Help?")).Returns(false);
            presenter.Setup(p => p.HelpMenuSelection()).Returns("Loading A File");
            presenter.Setup(p => p.Print("Bubbles dive planner saves files in JSON format. The default file name is dive_plan.json. When running the application a choice of \"yes\" or \"no\" will be presented with the message \"Load File:\". Selecting yes will load the file contained in the base of the program file structure."));
            divePresenter = new DivePresenter(presenter.Object);

            // When
            divePresenter.DisplayHelp();

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

        #endregion Welcome

        #region Dive Plan Setup

        [Fact]
        public void SelectDiveModel()
        {
            // Given
            var diveModel = new Zhl12Buhlmann(null);
            presenter.Setup(p => p.DiveModelSelection()).Returns(diveModel);
            presenter.Setup(p => p.Print($"Selected Dive Model: {diveModel.Name}"));
            divePresenter = new DivePresenter(presenter.Object);

            // When
            divePresenter.SelectDiveModel();

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
            divePresenter.CreateCylinders(nameof(DiveModelNames.ZHL16_B));

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
            divePresenter.CreateCylinders(nameof(DiveModelNames.DCAP_MF11F6));

            // Then
            presenter.Verify(p => p.GetByte("Enter Helium (%):", 0, 80), Times.Never);
            presenter.Verify(p => p.GetConfirmationDefaultNo("Create Another Cylinder?"));
            presenter.Verify(p => p.GetString("Enter Cylinder Name:"));
            presenter.Verify(p => p.GetByte("Enter Oxygen (%):", 5, 100));
            presenter.Verify(p => p.GetUshort("Enter Cylinder Volume (l):", 3, 15));
            presenter.Verify(p => p.GetUshort("Enter Cylinder Pressure (bar):", 50, 300));
            presenter.Verify(p => p.GetByte("Enter Surface Air Consumption Rate (l/min):", 5, 30));
        }

        #endregion

        #region Dive Plan Step

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
        public void SelectCylinder()
        {
            // Given
            var cylinder = TestFixture.FixtureSelectedCylinder;
            var cylinders = new List<ICylinder> { cylinder, cylinder };
            presenter.Setup(p => p.CylinderSelection(cylinders)).Returns(cylinder);
            presenter.Setup(p => p.Print($"Selected Cylinder: {cylinder.Name}"));
            divePresenter = new DivePresenter(presenter.Object);

            // When
            divePresenter.SelectCylinder(cylinders);

            // Then
            presenter.VerifyAll();
        }

        #endregion

        #region Results

        [Fact]
        public void PrintDiveResult()
        {
            // Given
            var table = new Table();
            var diveProfile = TestFixture.ExpectedDiveProfile;
            var diveModel = TestFixture.FixtureDiveModel(diveProfile);
            var diveStep = TestFixture.FixtureDiveStep;
            var cylinder = TestFixture.FixtureSelectedCylinder;
            var cylinders = new List<ICylinder> { cylinder, cylinder };
            var divePlan = new DivePlan(diveModel, cylinders, diveStep, cylinder);
            presenter.Setup(p => p.AssignDiveProfileTable(diveModel.Name, diveProfile)).Returns(table);
            presenter.Setup(p => p.AssignDiveStepTable(divePlan.DiveStep, divePlan.DiveModel.DiveProfile.DepthCeiling.ToString())).Returns(table);
            presenter.Setup(p => p.AssignCylindersTable(cylinders)).Returns(table);
            presenter.Setup(p => p.WriteResult(table));
            divePresenter = new DivePresenter(presenter.Object);

            // When
            divePresenter.PrintDiveResult(divePlan);

            // Then
            presenter.VerifyAll();
        }

        [Fact]
        public void PrintSimplfiedDiveResult()
        {
            // Given
            var table = new Table();
            var barChart = new BarChart();
            var diveProfile = TestFixture.ExpectedDiveProfile;
            var diveModel = TestFixture.FixtureDiveModel(diveProfile);
            var diveStep = TestFixture.FixtureDiveStep;
            var cylinder = TestFixture.FixtureSelectedCylinder;
            var cylinders = new List<ICylinder> { cylinder, cylinder };
            var divePlan = new DivePlan(diveModel, cylinders, diveStep, cylinder);
            presenter.Setup(p => p.GetConfirmationDefaultNo("Use Simplified Display?")).Returns(true);
            presenter.Setup(p => p.AssignDiveProfileChart(diveModel.Name, diveProfile)).Returns(barChart);
            presenter.Setup(p => p.AssignDiveStepTable(divePlan.DiveStep, divePlan.DiveModel.DiveProfile.DepthCeiling.ToString())).Returns(table);
            presenter.Setup(p => p.AssignCylindersTable(cylinders)).Returns(table);
            presenter.Setup(p => p.WriteResult(table));
            presenter.Setup(p => p.WriteResult(barChart));
            divePresenter = new DivePresenter(presenter.Object);

            // When
            divePresenter.DisplayResultOption();
            divePresenter.PrintDiveResult(divePlan);

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

        #endregion
    }
}