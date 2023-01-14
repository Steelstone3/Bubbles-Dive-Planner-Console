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
        private bool isChartDisplay = false;
        private readonly IPresenter presenter;

        public DivePresenter(IPresenter presenter)
        {
            this.presenter = presenter;
        }

        public void WelcomeMessage()
        {
            presenter.Print("Bubbles Dive Planner Console");
        }

        public void DisplayResultOption()
        {
            isChartDisplay = presenter.GetConfirmationDefaultNo("Use Chart Display?");
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

            var diveModel = AnsiConsole.Prompt(selectionPrompt
            .Title("Select Dive Model:")
            .AddChoices(diveModels));

            presenter.Print($"Selected Dive Model: {diveModel.Name}");

            return diveModel;
        }

        public List<ICylinder> CreateCylinders(string diveModelName)
        {
            var cylinders = new List<ICylinder>();
            do
            {
                cylinders.Add(CreateCylinder(diveModelName));
            } while (presenter.GetConfirmationDefaultNo("Create Another Cylinder?"));

            return cylinders;
        }

        public IDiveStep CreateDiveStep(byte depthCeiling, byte maximumOperatingDepth)
        {
            return new DiveStep(presenter.GetByte("Enter Depth (m):", depthCeiling, maximumOperatingDepth), presenter.GetByte("Enter Time (min):", 1, 60));
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

        public void PrintDiveResult(IDivePlan divePlan)
        {
            var diveModel = divePlan.DiveModel;
            var diveProfile = diveModel.DiveProfile;

            if (isChartDisplay)
            {
                var diveProfileChart = CreateAndAssignDiveProfileChart(diveModel.Name, diveProfile);
                AnsiConsole.Write(diveProfileChart);
            }
            else
            {
                var diveProfileTable = CreateDiveProfileTable(diveModel.Name);
                diveProfileTable = AssignDiveProfileTableRows(diveProfileTable, diveProfile);
                AnsiConsole.Write(diveProfileTable);
            }

            var diveStepTable = CreateDiveStepTable();
            diveStepTable = AssignDiveStepTableRows(diveStepTable, divePlan.DiveStep, diveModel.DiveProfile.DepthCeiling.ToString());
            AnsiConsole.Write(diveStepTable);

            var cylindersTable = CreateCylindersTable();
            cylindersTable = AssignCylindersTableRows(cylindersTable, divePlan.Cylinders);
            AnsiConsole.Write(cylindersTable);
        }

        private static BarChart CreateAndAssignDiveProfileChart(string diveModelName, IDiveProfile diveProfile)
        {
            var diveProfileChart = new BarChart();
            diveProfileChart.Label($"{diveModelName}");
            diveProfileChart.CenterLabel();

            for (int i = 0; i < diveProfile.CompartmentLoads.Length; i++)
            {
                double compartmentLoad = diveProfile.CompartmentLoads[i];
                diveProfileChart.AddItem($"Tissue Compartment: {i + 1}", compartmentLoad, DetermineColour(compartmentLoad));
            }

            return diveProfileChart;
        }

        private static Color DetermineColour(double compartmentLoad)
        {
            return compartmentLoad > 100.0 ? Color.DarkRed_1 : Color.SteelBlue1;
        }

        private static Table CreateDiveProfileTable(string diveModelName)
        {
            var diveProfileTable = new Table();
            diveProfileTable.Title($"Dive Model: {diveModelName}");
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

        private static Table AssignDiveStepTableRows(Table diveStepTable, IDiveStep diveStep, string depthCeiling)
        {
            var row = new[] {
                diveStep.Depth.ToString(),
                diveStep.Time.ToString(),
                depthCeiling
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
            var cylinderVolume = presenter.GetUshort("Enter Cylinder Volume (l):", 3, 15);
            var cylinderPressure = presenter.GetUshort("Enter Cylinder Pressure (bar):", 50, 300);
            var surfaceAirConsumption = presenter.GetByte("Enter Surface Air Consumption Rate (l/min):", 5, 30);
            var oxygenPercentage = presenter.GetByte("Enter Oxygen (%):", 5, 100);
            if (diveModelName == DiveModelNames.DCAP_MF11F6.ToString())
            {
                gasMixture = new GasMixture(oxygenPercentage, 0);
            }
            else
            {
                gasMixture = new GasMixture(oxygenPercentage, presenter.GetByte("Enter Helium (%):", 0, (byte)(100 - oxygenPercentage)));
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

        public bool ConfirmDecompression(double depthCeiling)
        {
            if (depthCeiling > 0.0)
            {
                return presenter.GetConfirmationDefaultYes("Run Decompression Steps?");
            }

            return false;
        }

        public bool ConfirmContinueWithDive()
        {
            return presenter.GetConfirmationDefaultYes("Continue?");
        }

        public void DisplayHelp()
        {
            if (presenter.GetConfirmationDefaultNo("Bubbles Dive Planner Help?"))
            {
                presenter.Print("Bubbles Dive Planner Console is an all in one tool for planning scuba diving activities. Currently the dive planner only support metric measurements which are used throughout the dive planner.\n\nThe dive planner aims to act like dive tables.\n\nThe dive planner will ask whether to load a file. Provide a y/n answer (default y). If no file is detected then an error will display otherwise the file will be loaded.\n\nWhen planning a dive the dive planner will ask you to select a dive model, set up the cylinders needed for the dive and then will proceed to create \"Dive Steps\" consisting of an amount of time at a given depth. This information is used to create the dive profile which is displayed as a table.\n\nWhen selecting a dive model select one of the options avaliable from the list. Note that some dive models are more conservative than others. A message will be displayed stating the selected dive model.\n\nWhen creating a cylinder provide a name of what the cylinder is to be referred to. This will be used when selecting a cylinder for a \"Dive Step\" such as \"Select Cylinder: Air, EAN32, EAN50\". The cylinder requires a volume. This is the cylinder's size such as a \"10 Litre Cylinder\". The cylinder requires a starting pressure. This is the amount of pressure the cylinder holds such as \"200 BAR of pressure\". The cylinder requires a surface air consumption rate. This is the rate in which a person breaths in litres per minute for example \"12 l/min\". The cylinder then requires a gas mixture of Oxygen and Helium where the remained is calculated for Nitrogen. Enter the desired mix and not that it will limit the maximum operating depth at 1.4 PPO2. The units used are percent for example \"21% Oxygen, 0% Helium and a calculated 79% Nitrogen giving a maximum operating depth of 56.6 Metres\". The dive planner will then ask whether to create another cylinder following the same process. Enter y/n (default y) based on the dive plan's needs the cylinder list cannot be altered past this point without starting a new dive plan.\n\nThe dive planner will now enter into a loop whereby it will ask for a \"dive step\" and provide a result. A \"dive step\" consists of the amount of time to be spent a given depth on a cylinder.\n\nSelect a cylinder from the list to be used as part of the \"dive step\". This will manage both gas management and gas mixture for the \"dive step\". For example \"Select Cylinder: Air, EAN32\". A message will be displayed stating the selected cylinder. Next enter both depth and time values for the dive profile. Validation will warn of out of range values. For example \"Depth: 50 metres, Time: 10 minutes, Oxygen: 32%\" will inform you the maximum range for depth is between 0 and 33 metres.\n\nOnce a valid dive step is entered the results will be displayed. Note the main table. This informs of simulated tissue loads, the pressure that tissue can tolerate in the ambient enviroment, the maximum surface pressure and most importantly the compartment's load. If a compartment load is over 100% then the simulated tissue is in decompression meaning the diver (you) would be in decompression for following this dive profile.\n\nThe next table displays the time at depth spent as part of the \"dive step\" as well as the dive ceiling. If the dive ceiling is greater than 0 then the diver is in decompression. For example \"Depth Ceiling: 3.1\" means that the diver must remain below 3.1 metres.\n\nThe next table displays the selected cylinder for the \"dive step\". This table displays the cylinder name, the initial pressurised volume (volume * pressure) in litres, the remaining gas in litres, the gas used for the \"dive step\" in litres, the gas mixture consisting of a percentage of oxygen, nitrogen and helium and the maximum operating depth or dive floor which a diver must not descend below due to the risk of seizures from CNS toxicity; the maximum operating depth is based on the oxygen percentage of the gas mixture in the selected cylinder.\n\nThe dive planner may ask whether to decompress the diver with a y/n. This is an algorithm that will run decompression proceedures automatically applying \"dive steps\" and displaying the results in 3 metre increments.\n\nThe dive planner will then ask if you wish to contine to create another dive step and will repeat the process.\n\nThe dive planner will ask if the diver wishes to save their dive profile. Selecting y will result in a .json file being created with the contents of the entire dive planner state. This includes the dive model selected, all the dive steps and all the results.");
            }
        }
    }
}