namespace BubblesDivePlanner.Presenters
{
    public interface IPresenter
    {
        void Print(string message);
        byte GetByte(string message, byte lowerBound, byte upperBound);
        ushort GetUshort(string message);
        double GetDouble(string message);
        string GetString(string message);
        bool GetConfirmation(string message);
    }
}