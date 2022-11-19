using System.Collections.Generic;
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
            return AnsiConsole.Prompt(new SelectionPrompt<IDiveModel>()
            .Title("Select Dive Model:")
            .AddChoices(new[] {
                new Zhl16Buhlmann()
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

        public void PrintDiveResults(IDiveProfile diveProfile)
        {
            for (int compartment = 0; compartment < diveProfile.CompartmentLoads.Length; compartment++)
            {
                presenter.Print($"| C: {compartment + 1} | TPt: {diveProfile.TotalTissuePressures[compartment]} | TAP: {diveProfile.ToleratedAmbientPressures[compartment]} | MSP: {diveProfile.MaxSurfacePressures[compartment]} | CLp: {diveProfile.CompartmentLoads[compartment]} |");
            }
        }

        public void PrintCylinder(ICylinder selectedCylinder)
        {
            presenter.Print($"| Cylinder: {selectedCylinder.Name} | Initial Pressurised Volume: {selectedCylinder.InitialPressurisedVolume} | Remaining Gas: {selectedCylinder.RemainingGas} | Used Gas: {selectedCylinder.UsedGas} | Oxygen: {selectedCylinder.GasMixture.Oxygen}% | Nitrogen: {selectedCylinder.GasMixture.Nitrogen}% | Helium: {selectedCylinder.GasMixture.Helium}% |");
        }

        private Cylinder CreateCylinder()
        {
            var name = presenter.GetString("Enter Cylinder Name:");
            var cylinderVolume = presenter.GetUshort("Enter Cylinder Volume:");
            var cylinderPressure = presenter.GetUshort("Enter Cylinder Pressure:");
            var surfaceAirConsumptionRate = presenter.GetByte("Enter Surface Air Consumption Rate:");
            var gasMixture = new GasMixture(presenter.GetByte("Enter Oxygen:"), presenter.GetByte("Enter Helium:"));
            return new Cylinder(name, cylinderVolume, cylinderPressure, gasMixture, surfaceAirConsumptionRate);
        }
    }
}