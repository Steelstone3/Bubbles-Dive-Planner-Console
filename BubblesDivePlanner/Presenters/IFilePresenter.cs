namespace BubblesDivePlanner.Presenters
{
    public interface IFilePresenter
    {
        bool DisplaySaveFile();
        bool DisplayLoadFile();
        void DisplaySaveErrorMessage(string fileName);
        void DisplayLoadErrorMessage(string fileName);
    }
}