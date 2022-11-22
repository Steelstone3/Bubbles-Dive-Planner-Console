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
                    return value switch
                    {
                        >= 0 and <= 100 => ValidationResult.Success(),
                        _ => ValidationResult.Error($"[red]Enter a value in the range: {lowerBound} - {upperBound}[/]"),
                    };
                }));
        }

        public ushort GetUshort(string message)
        {
            return AnsiConsole.Ask<ushort>(message);
        }

        public double GetDouble(string message)
        {
            return AnsiConsole.Ask<double>(message);
        }

        public string GetString(string message)
        {
            return AnsiConsole.Ask<string>(message);
        }

        public bool GetConfirmation(string message)
        {
            return AnsiConsole.Confirm(message);
        }

        private int bob()
        {
            return AnsiConsole.Prompt(
    new TextPrompt<int>("How [green]old[/] are you?")
        .PromptStyle("green")
        .ValidationErrorMessage("[red]That's not a valid age[/]")
        .Validate(age =>
        {
            return age switch
            {
                <= 0 => ValidationResult.Error("[red]You must at least be 1 years old[/]"),
                >= 123 => ValidationResult.Error("[red]You must be younger than the oldest person alive[/]"),
                _ => ValidationResult.Success(),
            };
        }));
        }
    }
}
