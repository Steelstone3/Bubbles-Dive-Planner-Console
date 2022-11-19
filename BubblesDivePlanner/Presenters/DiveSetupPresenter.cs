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

        private Cylinder CreateCylinder()
        {
            var gasMixture = new GasMixture(presenter.GetByte("Enter Oxygen:"), presenter.GetByte("Enter Helium:"));
            return new Cylinder(presenter.GetUshort("Enter Cylinder Volume:"), presenter.GetUshort("Enter Cylinder Pressure:"), gasMixture, presenter.GetByte("Enter Surface Air Consumption Rate:"));
        }
    }
}