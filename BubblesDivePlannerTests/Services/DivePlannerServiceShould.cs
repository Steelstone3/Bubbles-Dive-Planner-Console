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
        private readonly Mock<IDivePresenter> divePresenter = new();
        private readonly Mock<IDiveController> diveController = new();
        private readonly Mock<IDecompressionController> decompressionController = new();
        private readonly Mock<IFileController> fileController = new();
        private readonly IDivePlannerService divePlannerService;
        public DivePlannerServiceShould()
        {
            var divePlan = new DivePlan(TestFixture.FixtureDiveModel(null), TestFixture.FixtureCylinders(), TestFixture.FixtureDiveStep, TestFixture.FixtureSelectedCylinder);

            divePresenter.Setup(p => p.ConfirmContinueWithDive()).Returns(false);
            diveController.Setup(dc => dc.SetupDivePlan(divePlan));
            diveController.Setup(dc => dc.SetupDiveStep(0)).Returns(divePlan);
            diveController.Setup(dc => dc.RunDiveProfile(divePlan)).Returns(divePlan);
            diveController.Setup(dc => dc.PrintDiveResults(divePlan));
            fileController.Setup(fc => fc.LoadFile()).Returns(divePlan);
            fileController.Setup(fc => fc.AddDivePlan(divePlan));
            fileController.Setup(fc => fc.SaveFile());
            divePlannerService = new DivePlannerService(diveController.Object, fileController.Object);
        }


        [Fact]
        public void RunDivePlannerService()
        {
            // When
            divePlannerService.Run(divePresenter.Object);

            // Then
            divePresenter.VerifyAll();
            diveController.VerifyAll();
            decompressionController.VerifyAll();
            fileController.VerifyAll();
        }
    }
}