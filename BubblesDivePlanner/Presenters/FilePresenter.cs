namespace BubblesDivePlanner.Presenters
{
    public class FilePresenter : IFilePresenter
    {
        private readonly IPresenter presenter;

        public FilePresenter(IPresenter presenter)
        {
            this.presenter = presenter;
        }

        public bool DisplaySaveFile()
        {
            return presenter.GetConfirmationDefaultYes("Save File?");
        }

        public bool DisplayLoadFile()
        {
            return presenter.GetConfirmationDefaultNo("Load File?");
        }

        public void DisplaySaveErrorMessage(string fileName)
        {
            presenter.Print($"{fileName} could not be found or written to.");
        }

        public void DisplayLoadErrorMessage(string fileName)
        {
            presenter.Print($"{fileName} could not be found or read from.");
        }
    }
}