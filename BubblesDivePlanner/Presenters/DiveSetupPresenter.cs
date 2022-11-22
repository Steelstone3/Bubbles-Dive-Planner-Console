using System.Collections.Generic;
using BubblesDivePlanner.Models;
using BubblesDivePlanner.Models.Cylinders;
using BubblesDivePlanner.Models.DiveModels;
using Spectre.Console;

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
            var selectionPrompt = new SelectionPrompt<IDiveModel> { Converter = diveModel => diveModel.Name };

            return AnsiConsole.Prompt(selectionPrompt
            .Title("Select Dive Model:")
            .AddChoices(new[] {
                new Zhl16Buhlmann(null)
            }));
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
            var selectedCylinder = divePlan.SelectedCylinder;

            presenter.Print($"Dive Model: {diveModel.Name}");

            for (int compartment = 0; compartment < diveProfile.CompartmentLoads.Length; compartment++)
            {
                presenter.Print($"| C: {compartment + 1} | TPt: {diveProfile.TotalTissuePressures[compartment]} | TAP: {diveProfile.ToleratedAmbientPressures[compartment]} | MSP: {diveProfile.MaxSurfacePressures[compartment]} | CLp: {diveProfile.CompartmentLoads[compartment]} |");
            }

            presenter.Print($"| Cylinder: {selectedCylinder.Name} | Initial Pressurised Volume: {selectedCylinder.InitialPressurisedVolume} | Remaining Gas: {selectedCylinder.RemainingGas} | Used Gas: {selectedCylinder.UsedGas} | Oxygen: {selectedCylinder.GasMixture.Oxygen}% | Nitrogen: {selectedCylinder.GasMixture.Nitrogen}% | Helium: {selectedCylinder.GasMixture.Helium}% |");
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