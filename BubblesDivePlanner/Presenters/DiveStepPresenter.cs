using System;
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
            var selectionPrompt = new SelectionPrompt<ICylinder>();
            selectionPrompt.Converter = cylinder => cylinder.Name;;

            return AnsiConsole.Prompt(selectionPrompt
            .Title("Select Cylinder:")
            .AddChoices(cylinders));
        }
    }
}