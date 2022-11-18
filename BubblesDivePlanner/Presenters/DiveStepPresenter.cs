using System.Collections.Generic;
using BubblesDivePlanner.Models;
using BubblesDivePlanner.Models.Cylinders;
using Spectre.Console;

namespace BubblesDivePlanner.Presenters
{
    public class DiveStepPresenter : IDiveStepPresenter
    {
        private readonly IPresenter presenter;

        public DiveStepPresenter(IPresenter presenter)
        {
            this.presenter = presenter;
        }

        public IDiveStep CreateDiveStep()
        {
            return new DiveStep(presenter.GetByte("Enter Depth:"), presenter.GetByte("Enter Time:"));
        }

        public ICylinder SelectCylinder(List<ICylinder> cylinders)
        {
            return AnsiConsole.Prompt(new SelectionPrompt<ICylinder>()
            .Title("Select Cylinder:")
            .AddChoices(cylinders));
        }
    }
}