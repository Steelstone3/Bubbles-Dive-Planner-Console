using System.IO;
using BubblesDivePlanner.Controllers.Json;
using BubblesDivePlanner.Models;
using BubblesDivePlanner.Presenters;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.Controllers.Json
{
    public class FileControllerShould
    {
        private Mock<IPresenter> presenter = new();
        private IFileController fileController;

        [Fact]
        public void ConfirmSaveFile()
        {
            // Given
            presenter.Setup(p => p.GetConfirmation("Save File?")).Returns(true);
            var divePlan = new Mock<IDivePlan>();
            divePlan.Setup(dp => dp.Serialise());
            fileController = new FileController(presenter.Object);

            // When
            fileController.SaveFile(divePlan.Object);

            // Then
            presenter.VerifyAll();
            divePlan.VerifyAll();
        }

        [Fact]
        public void DenySaveFile()
        {
            // Given
            presenter.Setup(p => p.GetConfirmation("Save File?")).Returns(false);
            var divePlan = new Mock<IDivePlan>();
            divePlan.Setup(dp => dp.Serialise());
            fileController = new FileController(presenter.Object);

            // When
            fileController.SaveFile(divePlan.Object);

            // Then
            presenter.VerifyAll();
            divePlan.Verify(dp => dp.Serialise(), Times.Never);
        }

        [Fact]
        public void LoadFile()
        {
            // Given
            presenter.Setup(p => p.GetConfirmation("Load File?")).Returns(true);
            fileController = new FileController(presenter.Object);

            // When
            fileController.LoadFile();

            // Then
            presenter.VerifyAll();
        }
    }
}