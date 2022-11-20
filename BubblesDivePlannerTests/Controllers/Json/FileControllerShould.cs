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
            fileController = new FileController(presenter.Object);
            var divePlan = new Mock<IDivePlan>();
            divePlan.Setup(dp => dp.Serialise());

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
            fileController = new FileController(presenter.Object);
            var divePlan = new Mock<IDivePlan>();
            divePlan.Setup(dp => dp.Serialise());

            // When
            fileController.SaveFile(divePlan.Object);

            // Then
            presenter.VerifyAll();
            divePlan.Verify(dp => dp.Serialise(), Times.Never);
        }
    }
}