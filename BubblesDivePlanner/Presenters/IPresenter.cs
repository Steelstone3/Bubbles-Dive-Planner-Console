namespace BubblesDivePlanner.Presenters
{
    public interface IPresenter
    {
        void Print(string message);
        byte GetByte(string message, byte lowerBound, byte upperBound);
        ushort GetUshort(string message, ushort lowerBound, ushort upperBound);
        double GetDouble(string message);
        string GetString(string message);
        bool GetConfirmation(string message);
    }
}