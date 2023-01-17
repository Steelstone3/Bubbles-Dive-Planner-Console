using System.Collections.Generic;
using BubblesDivePlanner.Models;
using BubblesDivePlanner.Models.Cylinders;
using BubblesDivePlanner.Models.DiveModels;
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

        #region Welcome

        public void WelcomeMessage()
        {
            presenter.Print("Bubbles Dive Planner Console");
        }

        public void DisplayHelp()
        {
            if (presenter.GetConfirmationDefaultNo("Bubbles Dive Planner Help?"))
            {
                do
                {
                    var helpOption = presenter.HelpMenuSelection();
                    DisplayHelpMessage(helpOption);
                } while (presenter.GetConfirmationDefaultYes("Continue With Help?"));
            }
        }

        public void DisplayResultOption()
        {
            isChartDisplay = presenter.GetConfirmationDefaultNo("Use Simplified Display?");
        }

        #endregion

        #region Dive Plan Setup

        public IDiveModel SelectDiveModel()
        {
            IDiveModel diveModel = presenter.DiveModelSelection();
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

        #endregion

        #region Dive Plan Step

        public IDiveStep CreateDiveStep(double depthCeiling, byte maximumOperatingDepth)
        {
            return new DiveStep(presenter.GetByte("Enter Depth (m):", (byte)(depthCeiling > 1 ? depthCeiling : 1), maximumOperatingDepth), presenter.GetByte("Enter Time (min):", 1, 60));
        }

        public ICylinder SelectCylinder(List<ICylinder> cylinders)
        {
            var selectedCylinder = presenter.CylinderSelection(cylinders);
            presenter.Print($"Selected Cylinder: {selectedCylinder.Name}");

            return selectedCylinder;
        }

        #endregion

        #region Results

        public void PrintDiveResult(IDivePlan divePlan)
        {
            var diveModel = divePlan.DiveModel;
            var diveProfile = diveModel.DiveProfile;

            if (isChartDisplay)
            {
                var diveProfileChart = presenter.AssignDiveProfileChart(diveModel.Name, diveProfile);
                presenter.WriteResult(diveProfileChart);
            }
            else
            {
                var diveProfileTable = presenter.AssignDiveProfileTable(diveModel.Name, diveProfile);
                presenter.WriteResult(diveProfileTable);
            }

            var diveStepTable = presenter.AssignDiveStepTable(divePlan.DiveStep, diveModel.DiveProfile.DepthCeiling.ToString());
            presenter.WriteResult(diveStepTable);

            var cylindersTable = presenter.AssignCylindersTable(divePlan.Cylinders);
            presenter.WriteResult(cylindersTable);
        }

        public bool ConfirmContinueWithDive()
        {
            return presenter.GetConfirmationDefaultYes("Continue?");
        }

        public bool ConfirmDecompression(double depthCeiling)
        {
            if (depthCeiling > 0.0)
            {
                return presenter.GetConfirmationDefaultYes("Run Decompression Steps?");
            }

            return false;
        }

        #endregion

        #region Private Methods

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

        #endregion
    }
}