using System.Collections.Generic;
using BubblesDivePlanner.Models.Cylinders;
using BubblesDivePlanner.Models.DiveModels;
using Spectre.Console;

namespace BubblesDivePlanner.Presenters
{
    public class DivePlan
    {
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