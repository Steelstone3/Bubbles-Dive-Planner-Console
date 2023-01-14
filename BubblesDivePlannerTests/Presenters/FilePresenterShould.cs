using BubblesDivePlanner.Presenters;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.Presenters
{
    public class FilePresenterShould
    {
        private const string FILE_NAME = "dive_plan.json";
        private readonly Mock<IPresenter> presenter = new();
        IFilePresenter filePresenter;

        [Fact]
        public void DisplaySaveFile()
        {
            // Given
            presenter.Setup(p => p.GetConfirmationDefaultYes($"Save File?"));
            filePresenter = new FilePresenter(presenter.Object);

            // When
            filePresenter.DisplaySaveFile();

            // Then
            presenter.VerifyAll();
        }

        [Fact]
        public void DisplayLoadFile()
        {
            // Given
            presenter.Setup(p => p.GetConfirmationDefaultYes($"Load File?"));
            filePresenter = new FilePresenter(presenter.Object);

            // When
            filePresenter.DisplayLoadFile();

            // Then
            presenter.VerifyAll();
        }

        [Fact]
        public void DisplaySaveErrorMessage()
        {
            // Given
            presenter.Setup(p => p.Print($"{FILE_NAME} could not be found or written to."));
            filePresenter = new FilePresenter(presenter.Object);

            // When
            filePresenter.DisplaySaveErrorMessage(FILE_NAME);

            // Then
            presenter.VerifyAll();
        }

        [Fact]
        public void DisplayLoadErrorMessage()
        {
            // Given
            presenter.Setup(p => p.Print($"{FILE_NAME} could not be found or read from."));
            filePresenter = new FilePresenter(presenter.Object);

            // When
            filePresenter.DisplayLoadErrorMessage(FILE_NAME);

            // Then
            presenter.VerifyAll();
        }
    }
}