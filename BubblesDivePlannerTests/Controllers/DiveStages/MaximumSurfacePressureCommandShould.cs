using BubblesDivePlanner.Controllers.DiveStages;
using BubblesDivePlanner.DiveStages;
using BubblesDivePlanner.Models.DiveModels;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.DiveStages
{
    public class MaximumSurfacePressureCommandShould
    {
        [Fact]
        public void RunMaximumSurfacePressureStage()
        {
            //Arrange
            var diveModel = TestFixture.FixtureDiveModel;
            diveModel.DiveProfile.AValues = TestFixture.ExpectedAValues;
            diveModel.DiveProfile.BValues = TestFixture.ExpectedBValues;
            var diveStage = new MaximumSurfacePressure(diveModel);

            //Act
            diveStage.RunDiveStage();

            //Assert
            Assert.Equal(TestFixture.ExpectedMaxSurfacePressures, diveModel.DiveProfile.MaxSurfacePressures);
        }
    }
}