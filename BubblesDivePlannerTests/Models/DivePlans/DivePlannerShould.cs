using System.Collections.Generic;
using BubblesDivePlanner.Models;
using BubblesDivePlanner.Models.Cylinders;
using BubblesDivePlanner.Models.DiveModels;
using BubblesDivePlanner.Models.DivePlans;
using Xunit;

namespace BubblesDivePlannerTests.Models.DivePlans
{
    public class DivePlannerShould
    {
        private IDivePlanner divePlanner;

        public DivePlannerShould()
        {
            divePlanner = new DivePlanner(null, null, null, null);
        }

        [Fact]
        public void ContainAllDiveProfiles()
        {
            Assert.Empty(divePlanner.AllDiveProfiles);
        }

        [Fact]
        public void ContainAllCylinders()
        {
            Assert.Empty(divePlanner.AllCylinders);
        }

        [Fact]
        public void ContainAllDiveSteps()
        {
            Assert.Empty(divePlanner.AllDiveSteps);
        }

        [Fact]
        public void ContainAllSelectedCylinders()
        {
            Assert.Empty(divePlanner.AllSelectedCylinders);
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
            divePlanner = new DivePlanner(allDiveProfiles, allCylinders, allDiveSteps, allSelectedCylinders);

            // Then
            Assert.NotEmpty(divePlanner.AllDiveProfiles);
            Assert.NotEmpty(divePlanner.AllCylinders);
            Assert.NotEmpty(divePlanner.AllDiveSteps);
            Assert.NotEmpty(divePlanner.AllSelectedCylinders);
        }

        [Fact]
        public void UpdateDivePlans()
        {
            // Given
            IDivePlan divePlan = new DivePlan(TestFixture.FixtureDiveModel, TestFixture.FixtureCylinders(), TestFixture.FixtureDiveStep, TestFixture.FixtureSelectedCylinder);

            // When
            divePlanner.UpdateDivePlans(divePlan);

            // Then
            Assert.NotEmpty(divePlanner.AllDiveProfiles);
            Assert.NotEmpty(divePlanner.AllCylinders);
            Assert.NotEmpty(divePlanner.AllDiveSteps);
            Assert.NotEmpty(divePlanner.AllSelectedCylinders);
        }
    }
}