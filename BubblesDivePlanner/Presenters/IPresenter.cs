namespace BubblesDivePlanner.Presenters
{
    public interface IPresenter
    {
        void Print(string message);
        byte GetByte(string message);
        ushort GetUnsignedInteger16(string message);
        double GetDouble(string message);
    }
}