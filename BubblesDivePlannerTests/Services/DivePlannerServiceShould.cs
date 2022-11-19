using BubblesDivePlanner.Controllers;
using BubblesDivePlanner.Models;
using BubblesDivePlanner.Models.DiveModels;
using BubblesDivePlanner.Presenters;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.Services
{
    public class DivePlannerServiceShould
    {
        [Fact(Skip = "To do next")]
        public void RunDivePlannerService()
        {
            // Given
            var divePlan = new Mock<IDivePlan>();
            var diveProfile = new Mock<IDiveProfile>();
            var diveController = new Mock<IDiveController>();
            diveController.Setup(dc => dc.SetupDivePlan());
            diveController.Setup(dc => dc.SetupDiveStep());
            diveController.Setup(dc => dc.RunDiveProfile(divePlan.Object));
            diveController.Setup(dc => dc.PrintDiveResults(diveProfile.Object));
            var diveStagesController = new Mock<IDiveStagesController>();
            IDivePlannerService divePlannerService = new DivePlannerService();
            
            // When
            divePlannerService.Run();
        
            // Then
            diveController.VerifyAll();
        }
    }
}