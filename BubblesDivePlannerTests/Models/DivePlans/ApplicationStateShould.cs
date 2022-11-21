using System.Collections.Generic;
using BubblesDivePlanner.Models;
using BubblesDivePlanner.Models.Cylinders;
using BubblesDivePlanner.Models.DiveModels;
using BubblesDivePlanner.Models.DivePlans;
using Xunit;

namespace BubblesDivePlannerTests.Models.DivePlans
{
    public class ApplicationStateShould
    {
        private IApplicationState applicationState;

        public ApplicationStateShould()
        {
            applicationState = new ApplicationState(null, null, null, null);
        }

        [Fact]
        public void ContainAllDiveProfiles()
        {
            Assert.Empty(applicationState.AllDiveProfiles);
        }

        [Fact]
        public void ContainAllCylinders()
        {
            Assert.Empty(applicationState.AllCylinders);
        }

        [Fact]
        public void ContainAllDiveSteps()
        {
            Assert.Empty(applicationState.AllDiveSteps);
        }

        [Fact]
        public void ContainAllSelectedCylinders()
        {
            Assert.Empty(applicationState.AllSelectedCylinders);
        }

        [Fact]
        public void AssignAllDivePlans()
        {
            // Given
            List<IDiveProfile> allDiveProfiles = new()
            {
                TestFixture.FixtureDiveModel.DiveProfile
            };
            List<List<ICylinder>> allCylinders = new()
            {
                TestFixture.FixtureCylinders()
            };
            List<IDiveStep> allDiveSteps = new()
            {
                TestFixture.FixtureDiveStep
            };
            List<ICylinder> allSelectedCylinders = new()
            {
                TestFixture.FixtureSelectedCylinder
            };

            // When
            applicationState = new ApplicationState(allDiveProfiles, allCylinders, allDiveSteps, allSelectedCylinders);

            // Then
            Assert.NotEmpty(applicationState.AllDiveProfiles);
            Assert.NotEmpty(applicationState.AllCylinders);
            Assert.NotEmpty(applicationState.AllDiveSteps);
            Assert.NotEmpty(applicationState.AllSelectedCylinders);
        }

        [Fact]
        public void UpdateDivePlans()
        {
            // Given
            IDivePlan divePlan = new DivePlan(TestFixture.FixtureDiveModel, TestFixture.FixtureCylinders(), TestFixture.FixtureDiveStep, TestFixture.FixtureSelectedCylinder);

            // When
            applicationState.UpdateDivePlans(divePlan);

            // Then
            Assert.NotEmpty(applicationState.AllDiveProfiles);
            Assert.NotEmpty(applicationState.AllCylinders);
            Assert.NotEmpty(applicationState.AllDiveSteps);
            Assert.NotEmpty(applicationState.AllSelectedCylinders);
        }
    }
}