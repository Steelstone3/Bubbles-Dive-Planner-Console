using Spectre.Console;

namespace BubblesDivePlanner.Presenters
{
    public class Presenter : IPresenter
    {
        public void Print(string message)
        {
            AnsiConsole.WriteLine(message);
        }

        public byte GetByte(string message)
        {
            return AnsiConsole.Ask<byte>(message);
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
    }
}
