using System;
using System.Collections.Generic;
using BubblesDivePlanner.Models;
using BubblesDivePlanner.Models.Cylinders;
using BubblesDivePlanner.Models.DiveModels;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace BubblesDivePlanner.Presenters
{
    public class DiveSetupPresenter : IDiveSetupPresenter
    {
        private readonly IPresenter presenter;

        public DiveSetupPresenter(IPresenter presenter)
        {
            this.presenter = presenter;
        }

        public void WelcomeMessage()
        {
            presenter.Print("Bubbles Dive Planner Console");
        }

        public IDiveModel SelectDiveModel()
        {
            List<IDiveModel> diveModels = new List<IDiveModel>()
            {
                new Zhl16Buhlmann(null),
                new FakeZhl12Buhlmann(null),
                new FakeUsnRev6(null),
            };

            var selectionPrompt = new SelectionPrompt<IDiveModel> { Converter = diveModel => diveModel.Name };

            return AnsiConsole.Prompt(selectionPrompt
            .Title("Select Dive Model:")
            .AddChoices(diveModels));
        }

        public List<ICylinder> CreateCylinders()
        {
            var cylinders = new List<ICylinder>();
            do
            {
                cylinders.Add(CreateCylinder());
            } while (presenter.GetConfirmation("Create Another Cylinder?"));

            return cylinders;
        }

        public void PrintDiveResults(IDivePlan divePlan)
        {
            var diveModel = divePlan.DiveModel;
            var diveProfile = diveModel.DiveProfile;

            presenter.Print($"Dive Model: {diveModel.Name}");
            var diveProfileTable = CreateDiveProfileTable();
            diveProfileTable = AssignDiveProfileTableRows(diveProfileTable, diveProfile);
            AnsiConsole.Write(diveProfileTable);

            presenter.Print("Cylinders:");
            var cylindersTable = CreateCylindersTable();
            cylindersTable = AssignCylindersTableRows(cylindersTable, divePlan.Cylinders);
            AnsiConsole.Write(cylindersTable);
        }


        private static Table CreateDiveProfileTable()
        {
            var diveProfileTable = new Table();
            diveProfileTable.AddColumn("Compartment");
            diveProfileTable.AddColumn("Total Tissue Pressures");
            diveProfileTable.AddColumn("Tolerated Ambient Pressures");
            diveProfileTable.AddColumn("MaxSurface Pressures");
            diveProfileTable.AddColumn("Compartment Loads");

            return diveProfileTable;
        }

        private static Table CreateCylindersTable()
        {
            var cylindersTable = new Table();
            cylindersTable.AddColumn("Cylinder");
            cylindersTable.AddColumn("Initial Pressurised Volume");
            cylindersTable.AddColumn("Remaining Gas");
            cylindersTable.AddColumn("Used Gas");
            cylindersTable.AddColumn("Oxygen");
            cylindersTable.AddColumn("Nitrogen");
            cylindersTable.AddColumn("Helium");

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
                    diveProfile.CompartmentLoads[compartment].ToString()
                };

                diveProfileTable.AddRow(row);
            }

            return diveProfileTable;
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
                    cylinder.GasMixture.Helium.ToString()
                };
                cylindersTable.AddRow(row);
            }

            return cylindersTable;
        }

        private Cylinder CreateCylinder()
        {
            var name = presenter.GetString("Enter Cylinder Name:");
            var cylinderVolume = presenter.GetUshort("Enter Cylinder Volume:", 3, 15);
            var cylinderPressure = presenter.GetUshort("Enter Cylinder Pressure:", 50, 300);
            var surfaceAirConsumption = presenter.GetByte("Enter Surface Air Consumption Rate:", 5, 30);
            var oxygenPercentage = presenter.GetByte("Enter Oxygen:", 5, 100);
            var gasMixture = new GasMixture(oxygenPercentage, presenter.GetByte("Enter Helium:", 0, (byte)(100 - oxygenPercentage)));

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
    }
}