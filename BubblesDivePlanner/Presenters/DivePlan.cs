using System.Collections.Generic;
using BubblesDivePlanner.Models.Cylinders;
using BubblesDivePlanner.Models.DiveModels;
using Spectre.Console;

namespace BubblesDivePlanner.Presenters
{
    public class DivePlan : IDivePlan
    {
        private IPresenter presenter;

        public DivePlan(IPresenter @object)
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

        public ICylinder SelectCylinder(List<ICylinder> cylinders)
        {
            return AnsiConsole.Prompt(new SelectionPrompt<ICylinder>()
            .Title("Select Cylinder:")
            .AddChoices(cylinders));
        }
    }
}