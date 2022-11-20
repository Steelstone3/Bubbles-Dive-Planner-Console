using BubblesDivePlanner.Controllers;
using BubblesDivePlanner.Controllers.Json;
using BubblesDivePlanner.Models;
using BubblesDivePlanner.Models.DiveModels;
using BubblesDivePlanner.Presenters;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.Services
{
    public class DivePlannerServiceShould
    {
        [Fact]
        public void RunDivePlannerService()
        {
            // Given
            var presenter = new Mock<IPresenter>();
            presenter.Setup(p => p.GetConfirmation("Continue?")).Returns(false);
            var divePlan = new Mock<IDivePlan>();
            divePlan.Setup(dp => dp.DiveModel).Returns(TestFixture.FixtureDiveModel);
            var diveController = new Mock<IDiveController>();
            diveController.Setup(dc => dc.SetupDivePlan());
            diveController.Setup(dc => dc.SetupDiveStep()).Returns(divePlan.Object);
            diveController.Setup(dc => dc.RunDiveProfile(divePlan.Object)).Returns(divePlan.Object);
            diveController.Setup(dc => dc.RunGasManagement(divePlan.Object.SelectedCylinder, divePlan.Object.DiveStep));
            diveController.Setup(dc => dc.PrintDiveResults(divePlan.Object.DiveModel));
            diveController.Setup(dc => dc.PrintCylinder(divePlan.Object.SelectedCylinder));
            var fileController = new Mock<IFileController>();
            fileController.Setup(fc => fc.LoadFile()).Returns(divePlan.Object);
            fileController.Setup(fc => fc.SaveFile());
            IDivePlannerService divePlannerService = new DivePlannerService(diveController.Object, fileController.Object);

            // When
            divePlannerService.Run(presenter.Object);

            // Then
            presenter.VerifyAll();
            diveController.VerifyAll();
            fileController.VerifyAll();
        }
    }
}