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

        public IDiveStep CreateDiveStep(byte depthCeiling, byte maximumOperatingDepth)
        {
            return new DiveStep(presenter.GetByte("Enter Depth:", depthCeiling, maximumOperatingDepth), presenter.GetByte("Enter Time:", 1, 60));
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
    }
}