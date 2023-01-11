using System;
using System.Collections.Generic;
using BubblesDivePlanner.Models;
using BubblesDivePlanner.Models.Cylinders;
using BubblesDivePlanner.Models.DiveModels;
using BubblesDivePlanner.Models.DiveModels.Types;
using Spectre.Console;

namespace BubblesDivePlanner.Presenters
{
    public class DivePresenter : IDivePresenter
    {
        private readonly IPresenter presenter;

        public DivePresenter(IPresenter presenter)
        {
            this.presenter = presenter;
        }

        public IDiveStep CreateDiveStep(byte depthCeiling, byte maximumOperatingDepth)
        {
            return new DiveStep(presenter.GetByte("Enter Depth:", depthCeiling, maximumOperatingDepth), presenter.GetByte("Enter Time:", 1, 60));
        }

        public ICylinder SelectCylinder(List<ICylinder> cylinders)
        {
            var selectionPrompt = new SelectionPrompt<ICylinder> { Converter = cylinder => cylinder.Name };

            var selectedCylinder = AnsiConsole.Prompt(selectionPrompt
            .Title("Select Cylinder:")
            .AddChoices(cylinders));

            presenter.Print($"Selected Cylinder: {selectedCylinder.Name}");

            return selectedCylinder;
        }

        public void WelcomeMessage()
        {
            presenter.Print("Bubbles Dive Planner Console");
        }

        public IDiveModel SelectDiveModel()
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

        public List<ICylinder> CreateCylinders(string diveModelName)
        {
            var cylinders = new List<ICylinder>();
            do
            {
                cylinders.Add(CreateCylinder(diveModelName));
            } while (presenter.GetConfirmation("Create Another Cylinder?"));

            return cylinders;
        }

        public void PrintDiveResult(IDivePlan divePlan)
        {
            var diveModel = divePlan.DiveModel;
            var diveProfile = diveModel.DiveProfile;

            var diveProfileTable = CreateDiveProfileTable(diveModel.Name);
            diveProfileTable = AssignDiveProfileTableRows(diveProfileTable, diveProfile);
            AnsiConsole.Write(diveProfileTable);

            var diveStepTable = CreateDiveStepTable();
            diveStepTable = AssignDiveStepTableRows(diveStepTable, divePlan.DiveStep);
            AnsiConsole.Write(diveStepTable);

            var cylindersTable = CreateCylindersTable();
            cylindersTable = AssignCylindersTableRows(cylindersTable, divePlan.Cylinders);
            AnsiConsole.Write(cylindersTable);
            
            presenter.Print($"Depth Ceiling: {diveModel.DiveProfile.DepthCeiling}");
        }

        private static Table CreateDiveProfileTable(string diveModelName)
        {
            var diveProfileTable = new Table();
            diveProfileTable.Title($"Dive Model: {diveModelName}");
            diveProfileTable.AddColumn("Compartment");
            diveProfileTable.AddColumn("Total Tissue Pressures");
            diveProfileTable.AddColumn("Tolerated Ambient Pressures");
            diveProfileTable.AddColumn("MaxSurface Pressures");
            diveProfileTable.AddColumn("Compartment Loads");

            return diveProfileTable;
        }

        private static Table CreateDiveStepTable()
        {
            var diveStepTable = new Table();
            diveStepTable.Title("Dive Step");
            diveStepTable.AddColumn("Depth");
            diveStepTable.AddColumn("Time");

            return diveStepTable;
        }

        private static Table CreateCylindersTable()
        {
            var cylindersTable = new Table();
            cylindersTable.Title("Cylinders");
            cylindersTable.AddColumn("Cylinder");
            cylindersTable.AddColumn("Initial Pressurised Volume");
            cylindersTable.AddColumn("Remaining Gas");
            cylindersTable.AddColumn("Used Gas");
            cylindersTable.AddColumn("Oxygen");
            cylindersTable.AddColumn("Nitrogen");
            cylindersTable.AddColumn("Helium");
            cylindersTable.AddColumn("Maximum Operating Depth");

            return cylindersTable;
        }

        private static Table AssignDiveProfileTableRows(Table diveProfileTable, IDiveProfile diveProfile)
        {
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

        private static Table AssignDiveStepTableRows(Table diveStepTable, IDiveStep diveStep)
        {
            var row = new[] {
                diveStep.Depth.ToString(),
                diveStep.Time.ToString()
            };

            diveStepTable.AddRow(row);

            return diveStepTable;
        }

        private static Table AssignCylindersTableRows(Table cylindersTable, List<ICylinder> cylinders)
        {
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

        private Cylinder CreateCylinder(string diveModelName)
        {
            IGasMixture gasMixture;
            var name = presenter.GetString("Enter Cylinder Name:");
            var cylinderVolume = presenter.GetUshort("Enter Cylinder Volume:", 3, 15);
            var cylinderPressure = presenter.GetUshort("Enter Cylinder Pressure:", 50, 300);
            var surfaceAirConsumption = presenter.GetByte("Enter Surface Air Consumption Rate:", 5, 30);
            var oxygenPercentage = presenter.GetByte("Enter Oxygen:", 5, 100);
            if (diveModelName == DiveModelNames.DCAP_MF11F6.ToString())
            {
                gasMixture = new GasMixture(oxygenPercentage, 0);
            }
            else
            {
                gasMixture = new GasMixture(oxygenPercentage, presenter.GetByte("Enter Helium:", 0, (byte)(100 - oxygenPercentage)));
            }

            return new Cylinder(
                name,
                cylinderVolume,
                cylinderPressure,
                surfaceAirConsumption,
                gasMixture,
                0,
                0
            );
        }

        public bool ConfirmDecompression()
        {
            return presenter.GetConfirmation("Run Decompression Steps?");
        }

        public bool ConfirmContinueWithDive()
        {
            return presenter.GetConfirmation("Continue?");
        }
    }
}