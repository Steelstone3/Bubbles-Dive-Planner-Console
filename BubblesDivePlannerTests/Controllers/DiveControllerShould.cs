using System.Collections.Generic;
using BubblesDivePlanner.Controllers;
using BubblesDivePlanner.Models;
using BubblesDivePlanner.Models.Cylinders;
using Xunit;

namespace BubblesDivePlannerTests.Controllers
{
    public class DiveControllerShould
    {
        private IDivePlan divePlan;
        private IDiveController diveController;

        public DiveControllerShould()
        {
            var cylinders = new List<ICylinder>() { TestFixture.FixtureSelectedCylinder };
            divePlan = new DivePlan(TestFixture.FixtureDiveModel, cylinders, TestFixture.FixtureDiveStep, TestFixture.FixtureSelectedCylinder);
        }

        [Fact(Skip="Need to finish off assertions")]
        public void RunADiveStage()
        {
            // Given
            diveController = new DiveController();

            // When
            diveController.Run(divePlan);

            // Then
            Assert.Equal(TestFixture.ExpectedOxygenPressureAtDepth, divePlan.DiveModel.DiveProfile.OxygenPressureAtDepth);
            Assert.Equal(TestFixture.ExpectedHeliumPressureAtDepth, divePlan.DiveModel.DiveProfile.HeliumPressureAtDepth);
            Assert.Equal(TestFixture.ExpectedNitrogenPressureAtDepth, divePlan.DiveModel.DiveProfile.NitrogenPressureAtDepth);
            //TODO The rest of the asserts
            Assert.Equal(TestFixture.FixtureDiveStep.Depth, divePlan.DiveStep.Depth);
            Assert.Equal(TestFixture.FixtureDiveStep.Time, divePlan.DiveStep.Time);

            Assert.Equal(TestFixture.FixtureSelectedCylinder.CylinderPressure, divePlan.SelectedCylinder.CylinderPressure);
            Assert.Equal(TestFixture.FixtureSelectedCylinder.CylinderVolume, divePlan.SelectedCylinder.CylinderVolume);
            Assert.Equal(TestFixture.FixtureSelectedCylinder.InitialPressurisedVolume, divePlan.SelectedCylinder.InitialPressurisedVolume);
            Assert.Equal(TestFixture.FixtureSelectedCylinder.RemainingGas, divePlan.SelectedCylinder.RemainingGas);
            Assert.Equal(TestFixture.FixtureSelectedCylinder.SurfaceAirConsumptionRate, divePlan.SelectedCylinder.SurfaceAirConsumptionRate);
            Assert.Equal(TestFixture.FixtureSelectedCylinder.UsedGas, divePlan.SelectedCylinder.UsedGas);
        }
    }
}