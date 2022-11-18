namespace BubblesDivePlanner.Presenters
{
    public interface IPresenter
    {
        void Print(string message);
        byte GetByte(string message);
        ushort GetUshort(string message);
        double GetDouble(string message);
        bool GetConfirmation(string message);
    }
}