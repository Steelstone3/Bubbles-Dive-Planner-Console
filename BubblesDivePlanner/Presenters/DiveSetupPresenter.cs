using System.Collections.Generic;
using BubblesDivePlanner.Models.Cylinders;
using BubblesDivePlanner.Models.DiveModels;
using Spectre.Console;

namespace BubblesDivePlanner.Presenters
{
    public class DiveSetupPresenter : IDiveSetupPresenter
    {
        private IPresenter presenter;

        public DiveSetupPresenter(IPresenter @object)
        {
            this.presenter = @object;
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

        private Cylinder CreateCylinder()
        {
            var gasMixture = new GasMixture(presenter.GetByte("Enter Oxygen:"), presenter.GetByte("Enter Helium:"));
            return new Cylinder(presenter.GetUshort("Enter Cylinder Volume:"), presenter.GetUshort("Enter Cylinder Pressure:"), gasMixture, presenter.GetByte("Enter Surface Air Consumption Rate:"));
        }
    }
}