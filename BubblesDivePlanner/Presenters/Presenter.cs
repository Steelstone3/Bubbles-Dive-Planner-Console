using Spectre.Console;

namespace BubblesDivePlanner.Presenters
{
    public class Presenter : IPresenter
    {
        public void Print(string message)
        {
            AnsiConsole.WriteLine(message);
        }

        public byte GetByte(string message, byte lowerBound, byte upperBound)
        {
            return AnsiConsole
                .Prompt(new TextPrompt<byte>(message)
                .ValidationErrorMessage($"[red]Value entered out of range: {lowerBound} - {upperBound}[/]")
                .Validate(value =>
                {
                    return value >= lowerBound && value <= upperBound
                        ? ValidationResult.Success()
                        : ValidationResult.Error($"[red]Enter a value in the range: {lowerBound} - {upperBound}[/]");
                }));
        }

        public ushort GetUshort(string message, ushort lowerBound, ushort upperBound)
        {
            return AnsiConsole
                .Prompt(new TextPrompt<ushort>(message)
                .ValidationErrorMessage($"[red]Value entered out of range: {lowerBound} - {upperBound}[/]")
                .Validate(value =>
                {
                    return value >= lowerBound && value <= upperBound
                        ? ValidationResult.Success()
                        : ValidationResult.Error($"[red]Enter a value in the range: {lowerBound} - {upperBound}[/]");
                }));
        }

        public string GetString(string message)
        {
            return AnsiConsole.Ask<string>(message);
        }

        public bool GetConfirmationDefaultYes(string message)
        {
            return AnsiConsole.Confirm(message, true);
        }

        public bool GetConfirmationDefaultNo(string message)
        {
            return AnsiConsole.Confirm(message, false);
        }

        public string GenerateHelpMenu()
        {
            var helpOption = AnsiConsole.Prompt(new SelectionPrompt<string>()
           .Title("Select Help Tutorial:")
           .AddChoices(new string[]
            {
                "Loading A File",
                "Selecting A Display Type",
                "Selecting A Dive Model",
                "Adding A Cylinder",
                "Adding A Dive Step",
                "Selecting A Cylinder",
                "Reading The Results",
                "Running Decompression Steps",
                "Saving A File"
            }));

            Print(helpOption);

            return helpOption;
        }
    }
}
