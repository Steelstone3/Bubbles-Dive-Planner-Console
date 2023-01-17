using System.Collections.Generic;
using BubblesDivePlanner.Models;
using BubblesDivePlanner.Models.Cylinders;
using BubblesDivePlanner.Models.DiveModels;
using BubblesDivePlanner.Models.DiveModels.Types;
using Spectre.Console;

namespace BubblesDivePlanner.Presenters
{
    public class Presenter : IPresenter
    {
        #region Presenter

        public void Print(string message)
        {
            AnsiConsole.WriteLine(message);
        }

        public void WriteResult(Table table)
        {
            AnsiConsole.Write(table);
        }

        public void WriteResult(BarChart chart)
        {
            AnsiConsole.Write(chart);
        }

        public string GetString(string message)
        {
            return AnsiConsole.Ask<string>(message);
        }

        public byte GetByte(string message, byte lowerBound, byte upperBound)
        {
            return AnsiConsole
                .Prompt(new TextPrompt<byte>(message)
                .ValidationErrorMessage($"[red]Value entered out of range: {lowerBound} - {upperBound}[/]")
                .Validate(value =>
                {
                    return value >= lowerBound && value <= upperBound
                        ? ValidationResult.Success()
                        : ValidationResult.Error($"[red]Enter a value in the range: {lowerBound} - {upperBound}[/]");
                }));
        }

        public ushort GetUshort(string message, ushort lowerBound, ushort upperBound)
        {
            return AnsiConsole
                .Prompt(new TextPrompt<ushort>(message)
                .ValidationErrorMessage($"[red]Value entered out of range: {lowerBound} - {upperBound}[/]")
                .Validate(value =>
                {
                    return value >= lowerBound && value <= upperBound
                        ? ValidationResult.Success()
                        : ValidationResult.Error($"[red]Enter a value in the range: {lowerBound} - {upperBound}[/]");
                }));
        }

        public bool GetConfirmationDefaultYes(string message)
        {
            return AnsiConsole.Confirm(message, true);
        }

        public bool GetConfirmationDefaultNo(string message)
        {
            return AnsiConsole.Confirm(message, false);
        }

        #endregion

        #region Welcome

        public string HelpMenuSelection()
        {
            var helpOption = AnsiConsole.Prompt(new SelectionPrompt<string>()
           .Title("Select Help Tutorial:")
           .AddChoices(new string[]
            {
                "Loading A File",
                "Selecting A Display Type",
                "Selecting A Dive Model",
                "Adding A Cylinder",
                "Adding A Dive Step",
                "Selecting A Cylinder",
                "Reading The Results",
                "Running Decompression Steps",
                "Saving A File"
            }));

            Print(helpOption);

            return helpOption;
        }

        #endregion

        #region Dive Plan Setup

        public IDiveModel DiveModelSelection()
        {
            List<IDiveModel> diveModels = new()
            {
                // TODO re-enable when ready for production
                new Zhl16Buhlmann(null),
                // new Zhl12Buhlmann(null),
                new UsnRevision6(null),
                new DcapMf11f6(null),
            };

            var selectionPrompt = new SelectionPrompt<IDiveModel> { Converter = diveModel => diveModel.Name };

            return AnsiConsole.Prompt(selectionPrompt
            .Title("Select Dive Model:")
            .AddChoices(diveModels));
        }

        public ICylinder CylinderSelection(List<ICylinder> cylinders)
        {
            var selectionPrompt = new SelectionPrompt<ICylinder> { Converter = cylinder => cylinder.Name };

            return AnsiConsole.Prompt(selectionPrompt
            .Title("Select Cylinder:")
            .AddChoices(cylinders));
        }

        #endregion

        #region Results

        public Table AssignDiveProfileTable(string diveModelName, IDiveProfile diveProfile)
        {
            var diveProfileTable = CreateDiveProfileTable(diveModelName);

            for (int compartment = 0; compartment < diveProfile.CompartmentLoads.Length; compartment++)
            {
                string[] row = new[] {
                    (compartment + 1).ToString(),
                    diveProfile.TotalTissuePressures[compartment].ToString(),
                    diveProfile.ToleratedAmbientPressures[compartment].ToString(),
                    diveProfile.MaxSurfacePressures[compartment].ToString(),
                    diveProfile.CompartmentLoads[compartment].ToString(),
                };

                diveProfileTable.AddRow(row);
            }

            return diveProfileTable;
        }

        public BarChart AssignDiveProfileChart(string diveModelName, IDiveProfile diveProfile)
        {
            var diveProfileChart = new BarChart();
            diveProfileChart.Label($"Dive Profile Result\nDive Model: {diveModelName}");
            diveProfileChart.CenterLabel();

            for (int i = 0; i < diveProfile.CompartmentLoads.Length; i++)
            {
                double compartmentLoad = diveProfile.CompartmentLoads[i];
                diveProfileChart.AddItem($"Tissue Compartment: {i + 1}", compartmentLoad, DetermineColour(compartmentLoad));
            }

            return diveProfileChart;
        }

        public Table AssignDiveStepTable(IDiveStep diveStep, string depthCeiling)
        {
            var diveStepTable = CreateDiveStepTable();

            var row = new[] {
                diveStep.Depth.ToString(),
                diveStep.Time.ToString(),
                depthCeiling
            };

            diveStepTable.AddRow(row);

            return diveStepTable;
        }

        public Table AssignCylindersTable(List<ICylinder> cylinders)
        {
            var cylindersTable = CreateCylindersTable();

            foreach (var cylinder in cylinders)
            {
                var row = new[] {
                    cylinder.Name,
                    cylinder.InitialPressurisedVolume.ToString(),
                    cylinder.RemainingGas.ToString(),
                    cylinder.UsedGas.ToString(),
                    cylinder.GasMixture.Oxygen.ToString(),
                    cylinder.GasMixture.Nitrogen.ToString(),
                    cylinder.GasMixture.Helium.ToString(),
                    cylinder.GasMixture.MaximumOperatingDepth.ToString(),
                };
                cylindersTable.AddRow(row);
            }

            return cylindersTable;
        }

        #endregion

        #region Private Methods

        private static Table CreateDiveProfileTable(string diveModelName)
        {
            var diveProfileTable = new Table();
            diveProfileTable.Title($"Dive Profile Result\nDive Model: {diveModelName}");
            diveProfileTable.AddColumn("Compartment");
            diveProfileTable.AddColumn("Total Tissue Pressures");
            diveProfileTable.AddColumn("Tolerated Ambient Pressures");
            diveProfileTable.AddColumn("MaxSurface Pressures");
            diveProfileTable.AddColumn("Compartment Loads (%)");

            return diveProfileTable;
        }

        private static Table CreateDiveStepTable()
        {
            var diveStepTable = new Table();
            diveStepTable.Title("Dive Step");
            diveStepTable.AddColumn("Depth (m)");
            diveStepTable.AddColumn("Time (min)");
            diveStepTable.AddColumn("Depth Ceiling (m)");

            return diveStepTable;
        }

        private static Table CreateCylindersTable()
        {
            var cylindersTable = new Table();
            cylindersTable.Title("Cylinders");
            cylindersTable.AddColumn("Cylinder");
            cylindersTable.AddColumn("Initial Pressurised Volume (l)");
            cylindersTable.AddColumn("Remaining Gas (l)");
            cylindersTable.AddColumn("Used Gas (l)");
            cylindersTable.AddColumn("Oxygen (%)");
            cylindersTable.AddColumn("Nitrogen (%)");
            cylindersTable.AddColumn("Helium (%)");
            cylindersTable.AddColumn("Maximum Operating Depth (m)");

            return cylindersTable;
        }

        private static Color DetermineColour(double compartmentLoad)
        {
            return compartmentLoad > 100.0 ? Color.DarkRed_1 : Color.SteelBlue1;
        }

        #endregion
    }
}
