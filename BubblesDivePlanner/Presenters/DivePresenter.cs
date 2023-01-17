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
        private bool isChartDisplay;
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
            isChartDisplay = presenter.GetConfirmationDefaultNo("Use Simplified Display?");
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

        public IDiveStep CreateDiveStep(double depthCeiling, byte maximumOperatingDepth)
        {
            return new DiveStep(presenter.GetByte("Enter Depth (m):", (byte)(depthCeiling > 1 ? depthCeiling : 1), maximumOperatingDepth), presenter.GetByte("Enter Time (min):", 1, 60));
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
            diveProfileChart.Label($"Dive Profile Result\nDive Model: {diveModelName}");
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
                do
                {
                    var helpOption = presenter.GenerateHelpMenu();
                    DisplayHelpMessage(helpOption);
                } while (presenter.GetConfirmationDefaultYes("Continue With Help?"));
            }
        }

        private void DisplayHelpMessage(string helpOption)
        {
            switch (helpOption)
            {
                case "Loading A File":
                    presenter.Print("Bubbles dive planner saves files in JSON format. The default file name is dive_plan.json. When running the application a choice of \"yes\" or \"no\" will be presented with the message \"Load File:\". Selecting yes will load the file contained in the base of the program file structure.");
                    break;
                case "Selecting A Display Type":
                    presenter.Print("Bubbles dive planner has two display options. Simplified view will display results as a chart of \"Compartment Loads\". Compartment loads that are \"in decompression\" have bars displayed in red and those in tolerance are in blue. Expert view displays a table. This contains details of the dive model whereby compartments over 100% in the compartment loads column are in decompression and those in tolerance are below 100%.");
                    break;
                case "Selecting A Dive Model":
                    presenter.Print("Bubbles dive planner supports multiple dive models. Select a dive model from the list by using the up and down arrow keys and pressing the enter key. The option selected is fixed for the duration of the dive.");
                    break;
                case "Adding A Cylinder":
                    presenter.Print("Bubbles dive planner creates cylinders at the start of a dive. The cylinders added are fixed for the duration of the dive. To add a cylinder enter values when prompted. For example:\n\nCylinder Name: Air\nEnter Cylinder Volume: 12\nEnter Cylinder Volume: 200\nEnter Surface Air Consumption Rate: 12\nEnter Oxygen (%): 21\nEnter Helium (%): 0\n");
                    break;
                case "Adding A Dive Step":
                    presenter.Print("Bubbles dive planner adds a dive step. A dive step consists of depth and time so can be seen as time spent at a depth conceptually. The maximum depth that can be entered is based on the maximum operating depth of the selected cylinder's gas mixture. The minimum depth that can be entered is based on the depth ceiling which is calculated from the dive model. To add a dive step enter values when prompted. For example:\n\nDepth: 50\nTime: 10\n");
                    break;
                case "Selecting A Cylinder":
                    presenter.Print("Bubbles dive planner supports mutliple cylinders. Select one of the cylinders created earlier from the list. Use the up and down arrow keys to navigate and the enter key to select the cylinder. The option selected is used for the \"dive step\".");
                    break;
                case "Reading The Results":
                    presenter.Print("Bubbles dive planner displays a result at the end of each \"dive step\". Depending on which display was selected will depend on how the result is displayed. The simplified view displays a bar chart of \"Compartment Loads\". A compartment load is a represented simulated tissue. Over 100% means the tissue is overloaded and in decompression. These results are displayed in red on the chart. Results that are in tolerance are displayed in blue.\n\nSimilarly in the expert view the table represents compartment loads in the table which works in the same way along with other fields.\n\nTotal tissue pressures represents the pressure within the tissues from both nitrogen and helium (inert gas) that has been ongased.\nTolerated ambient pressures represents the current value in the dive model that the tissues are relative to the max surface pressure. The proportion between tolerated ambient pressure and maximum surface pressure converted to a percentage is represented as a \"compartment load\".\nMax surface pressures represents the amount of pressure a single tissue can tolerate in pressure difference to the surface pressure. A large differential would result in faster off-gassing. The aim with a dive when planning is to have a controlled off-gassing.\n");
                    break;
                case "Running Decompression Steps":
                    presenter.Print("Bubbles dive planner supports algorithmic automated decompression. If depth ceiling is over 0 meters then an option will be presented; \"Run Decompression Steps?\" selecting yes will prompt \"Select Cylinder:\" select the cylinder to do decompression on. The application will then run a series of dive steps to return to the surface safely.");
                    break;
                case "Saving A File":
                    presenter.Print("Bubbles dive planner saves files in JSON format. The default file name is dive_plan.json. When running the application a choice of \"yes\" or \"no\" will be presented with the message \"Save File:\". Selecting yes will save the file which will be contained in the base of the program file structure.");
                    break;
                default:
                    presenter.Print("Invalid option");
                    break;
            }
        }
    }
}