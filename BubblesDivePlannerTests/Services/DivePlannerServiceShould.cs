using BubblesDivePlanner.Controllers;
using BubblesDivePlanner.Controllers.Json;
using BubblesDivePlanner.Models;
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
            var divePlan = new DivePlan(TestFixture.FixtureDiveModel, TestFixture.FixtureCylinders(), TestFixture.FixtureDiveStep, TestFixture.FixtureSelectedCylinder);
            var diveController = new Mock<IDiveController>();
            diveController.Setup(dc => dc.SetupDivePlan(divePlan));
            diveController.Setup(dc => dc.SetupDiveStep()).Returns(divePlan);
            diveController.Setup(dc => dc.RunDiveProfile(divePlan)).Returns(divePlan);
            diveController.Setup(dc => dc.PrintDiveResults(divePlan));
            var fileController = new Mock<IFileController>();
            fileController.Setup(fc => fc.LoadFile()).Returns(divePlan);
            fileController.Setup(fc => fc.SaveFile(divePlan));
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