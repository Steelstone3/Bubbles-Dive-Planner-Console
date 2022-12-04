using BubblesDivePlanner.Models.DiveModels;
using BubblesDivePlanner.Models.DiveModels.Types;
using Xunit;

namespace BubblesDivePlannerTests.Models.DiveModels.Types
{
    public class DcapMf11f6Should
    {
        private const byte COMPARTMENT_COUNT = 11;
        private readonly IDiveModel diveModel = new DcapMf11f6(null);

        [Fact]
        public void ContainsDiveModelName()
        {
            Assert.Equal("DCAP_MF11F6", diveModel.Name);
        }

        [Fact]
        public void ContainsCompartmentCount()
        {
            Assert.Equal(COMPARTMENT_COUNT, diveModel.CompartmentCount);
        }

        [Fact]
        public void ContainsNitrogenHalfTime()
        {
            double[] expectedValue = new double[COMPARTMENT_COUNT] { 5.0, 10.0, 25.0, 55.0, 95.0, 145.0, 200.0, 285.0, 385.0, 520.0, 670.0 };

            Assert.Equal(expectedValue, diveModel.NitrogenHalfTimes);
        }

        [Fact]
        public void ContainsHeliumHalfTime()
        {
            // No published helium half times
            double[] expectedValue = new double[COMPARTMENT_COUNT] { 5.0 / 2.65, 10.0 / 2.65, 25.0 / 2.65, 55.0 / 2.65, 95.0 / 2.65, 145.0 / 2.65, 200.0 / 2.65, 285.0 / 2.65, 385.0 / 2.65, 520.0 / 2.65, 670.0 / 2.65 };

            Assert.Equal(expectedValue, diveModel.HeliumHalfTimes);
        }

        [Fact]
        public void ContainsAValuesNitrogen()
        {
            double[] expectedValue = new double[COMPARTMENT_COUNT] { 1.89, 1.415, 0.824, 0.418, 0.352, 0.346, 0.343, 0.35, 0.35, 0.34, 0.33 };

            Assert.Equal(expectedValue, diveModel.AValuesNitrogen);
        }

        [Fact]
        public void ContainsBValuesNitrogen()
        {
            double[] expectedValue = new double[COMPARTMENT_COUNT] { 0.769, 0.952, 0.926, 0.943, 0.962, 0.98, 0.99, 1.0, 1.0, 1.0, 1.0 };

            Assert.Equal(expectedValue, diveModel.BValuesNitrogen);
        }

        [Fact]
        public void ContainsAValuesHelium()
        {
            // No published a values for helium
            double[] expectedValue = new double[COMPARTMENT_COUNT] { 1.89, 1.415, 0.824, 0.418, 0.352, 0.346, 0.343, 0.35, 0.35, 0.34, 0.33 };

            Assert.Equal(expectedValue, diveModel.AValuesHelium);
        }

        [Fact]
        public void ContainsBValuesHelium()
        {
            // No published b values for helium
            double[] expectedValue = new double[COMPARTMENT_COUNT] { 0.769, 0.952, 0.926, 0.943, 0.962, 0.98, 0.99, 1.0, 1.0, 1.0, 1.0 };

            Assert.Equal(expectedValue, diveModel.BValuesHelium);
        }

        [Fact]
        public void ContainsDiveProfileDefault()
        {
            Assert.NotNull(diveModel.DiveProfile);
        }

        [Fact]
        public void ContainsDiveProfileDefaultDiveProfile()
        {
            // Given
            var diveModel = new UsnRevision6(new DiveProfile(9));

            // Then
            Assert.NotNull(diveModel.DiveProfile);
        }

        [Fact]
        public void ContainsDiveProfile()
        {
            // Given
            var diveModel = new UsnRevision6(TestFixture.FixtureDiveModel(null).DiveProfile);

            // Then
            Assert.NotNull(diveModel.DiveProfile);
        }
    }
}