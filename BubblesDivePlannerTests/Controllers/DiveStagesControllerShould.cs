using System.Collections.Generic;
using BubblesDivePlanner.Controllers;
using BubblesDivePlanner.Models;
using BubblesDivePlanner.Models.Cylinders;
using BubblesDivePlanner.Models.DivePlans;
using Xunit;

namespace BubblesDivePlannerTests.Controllers
{
    public class DiveStagesControllerShould
    {
        private readonly IDivePlan divePlan;
        private IDiveStagesController diveController;

        public DiveStagesControllerShould()
        {
            var cylinders = new List<ICylinder>() { TestFixture.FixtureSelectedCylinder };
            divePlan = new DivePlan(null, TestFixture.FixtureDiveModel, cylinders, TestFixture.FixtureDiveStep, TestFixture.FixtureSelectedCylinder);
        }

        [Fact]
        public void RunADiveStage()
        {
            // Given
            diveController = new DiveStagesController();

            // When
            diveController.Run(divePlan);

            // Then
            Assert.Equal(TestFixture.ExpectedOxygenPressureAtDepth, divePlan.DiveModel.DiveProfile.OxygenPressureAtDepth);
            Assert.Equal(TestFixture.ExpectedHeliumPressureAtDepth, divePlan.DiveModel.DiveProfile.HeliumPressureAtDepth);
            Assert.Equal(TestFixture.ExpectedNitrogenPressureAtDepth, divePlan.DiveModel.DiveProfile.NitrogenPressureAtDepth);
            Assert.Equal(TestFixture.ExpectedNitrogenTissuePressures, divePlan.DiveModel.DiveProfile.NitrogenTissuePressures);
            Assert.Equal(TestFixture.ExpectedHeliumTissuePressures, divePlan.DiveModel.DiveProfile.HeliumTissuePressures);
            Assert.Equal(TestFixture.ExpectedTotalTissuePressures, divePlan.DiveModel.DiveProfile.TotalTissuePressures);
            Assert.Equal(TestFixture.ExpectedMaxSurfacePressures, divePlan.DiveModel.DiveProfile.MaxSurfacePressures);
            Assert.Equal(TestFixture.ExpectedToleratedAmbientPressures, divePlan.DiveModel.DiveProfile.ToleratedAmbientPressures);
            Assert.Equal(TestFixture.ExpectedCompartmentLoads, divePlan.DiveModel.DiveProfile.CompartmentLoads);

            Assert.Equal(TestFixture.FixtureDiveStep.Depth, divePlan.DiveStep.Depth);
            Assert.Equal(TestFixture.FixtureDiveStep.Time, divePlan.DiveStep.Time);

            Assert.Equal(TestFixture.FixtureSelectedCylinder.CylinderPressure, divePlan.SelectedCylinder.CylinderPressure);
            Assert.Equal(TestFixture.FixtureSelectedCylinder.CylinderVolume, divePlan.SelectedCylinder.CylinderVolume);
            Assert.Equal(TestFixture.FixtureSelectedCylinder.InitialPressurisedVolume, divePlan.SelectedCylinder.InitialPressurisedVolume);
            Assert.Equal(1680, divePlan.SelectedCylinder.RemainingGas);
            Assert.Equal(TestFixture.FixtureSelectedCylinder.SurfaceAirConsumptionRate, divePlan.SelectedCylinder.SurfaceAirConsumptionRate);
            Assert.Equal(720, divePlan.SelectedCylinder.UsedGas);
        }
    }
}