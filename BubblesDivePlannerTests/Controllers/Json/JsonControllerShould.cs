namespace BubblesDivePlannerTests.Controllers.Json
{
    public class JsonControllerShould
    {
        // [SkippableFact]
        //         public void SerialiseUnix()
        //         {
        //             Skip.If(RuntimeInformation.IsOSPlatform(OSPlatform.Windows));

        //             var divePlanJson = divePlan.Serialise();

        //             Assert.Equal(EXPECTED_DIVE_PLAN_JSON_UNIX, divePlanJson);
        //         }

        //         [SkippableFact]
        //         public void SerialiseRanDivePlanUnix()
        //         {
        //             Skip.If(RuntimeInformation.IsOSPlatform(OSPlatform.Windows));

        //             var defaultList = CreateDefaultValueList();
        //             divePlan = new DivePlan(diveModel, expectedCylinders, diveStep, expectedCylinder);
        //             divePlan.DiveModel.DiveProfile.UpdateDiveProfile(new DiveProfile
        //             (
        //                 defaultList,
        //                 defaultList,
        //                 defaultList,
        //                 defaultList,
        //                 defaultList,
        //                 defaultList,
        //                 defaultList,
        //                 defaultList,
        //                 defaultValue,
        //                 defaultValue,
        //                 defaultValue
        //             ));

        //             var divePlanJson = divePlan.Serialise();

        //             Assert.Equal(EXPECTED_RAN_DIVE_PLAN_JSON_UNIX, divePlanJson);
        //         }

        //         [SkippableFact]
        //         public void SerialiseWindows()
        //         {
        //             Skip.IfNot(RuntimeInformation.IsOSPlatform(OSPlatform.Windows));

        //             var divePlanJson = divePlan.Serialise();

        //             Assert.Equal(EXPECTED_DIVE_PLAN_JSON_WINDOWS, divePlanJson);
        //         }

        //         [Theory]
        //         [InlineData(EXPECTED_DIVE_PLAN_JSON_UNIX)]
        //         [InlineData(EXPECTED_DIVE_PLAN_JSON_WINDOWS)]
        //         public void Deserialise(string expectedJsonString)
        //         {
        //             IDivePlan actualDivePlan = new DivePlan(null, null, null, null);

        //             actualDivePlan.Deserialise(expectedJsonString);

        //             AssertDiveModel(divePlan.DiveModel, actualDivePlan.DiveModel);
        //             AssertCylinders(divePlan.Cylinders, actualDivePlan.Cylinders);
        //         }

        //         [Fact]
        //         public void DeserialiseRanDivePlan()
        //         {
        //             var defaultList = CreateDefaultValueList();
        //             divePlan.DiveModel.DiveProfile.UpdateDiveProfile(new DiveProfile
        //             (
        //                 defaultList,
        //                 defaultList,
        //                 defaultList,
        //                 defaultList,
        //                 defaultList,
        //                 defaultList,
        //                 defaultList,
        //                 defaultList,
        //                 defaultValue,
        //                 defaultValue,
        //                 defaultValue
        //             ));
        //             IDivePlan actualDivePlan = new DivePlan(null, null, null, null);

        //             actualDivePlan.Deserialise(EXPECTED_RAN_DIVE_PLAN_JSON_UNIX);

        //             AssertDiveModel(divePlan.DiveModel, actualDivePlan.DiveModel);
        //             AssertCylinders(expectedCylinders, actualDivePlan.Cylinders);
        //         }

        //         private double[] CreateDefaultValueList()
        //         {
        //             return new double[16]
        //             {
        //                 defaultValue,
        //                 defaultValue,
        //                 defaultValue,
        //                 defaultValue,
        //                 defaultValue,
        //                 defaultValue,
        //                 defaultValue,
        //                 defaultValue,
        //                 defaultValue,
        //                 defaultValue,
        //                 defaultValue,
        //                 defaultValue,
        //                 defaultValue,
        //                 defaultValue,
        //                 defaultValue,
        //                 defaultValue,
        //             };
        //         }

        //         private static void AssertDiveModel(IDiveModel expected, IDiveModel actual)
        //         {
        //             Assert.Equal(expected.Name, actual.Name);
        //             Assert.Equal(expected.CompartmentCount, actual.CompartmentCount);
        //             Assert.Equal(expected.NitrogenHalfTimes, actual.NitrogenHalfTimes);
        //             Assert.Equal(expected.HeliumHalfTimes, actual.HeliumHalfTimes);
        //             Assert.Equal(expected.AValuesNitrogen, actual.AValuesNitrogen);
        //             Assert.Equal(expected.BValuesNitrogen, actual.BValuesNitrogen);
        //             Assert.Equal(expected.AValuesHelium, actual.AValuesHelium);
        //             Assert.Equal(expected.BValuesHelium, actual.BValuesHelium);
        //             AssertDiveProfile(expected.DiveProfile, actual.DiveProfile);
        //         }

        //         private static void AssertDiveProfile(IDiveProfile expected, IDiveProfile actual)
        //         {
        //             Assert.Equal(expected.OxygenPressureAtDepth, actual.OxygenPressureAtDepth);
        //             Assert.Equal(expected.HeliumPressureAtDepth, actual.HeliumPressureAtDepth);
        //             Assert.Equal(expected.NitrogenPressureAtDepth, actual.NitrogenPressureAtDepth);
        //             Assert.Equal(expected.NitrogenTissuePressures, actual.NitrogenTissuePressures);
        //             Assert.Equal(expected.HeliumTissuePressures, actual.HeliumTissuePressures);
        //             Assert.Equal(expected.TotalTissuePressures, actual.TotalTissuePressures);
        //             Assert.Equal(expected.AValues, actual.AValues);
        //             Assert.Equal(expected.BValues, actual.BValues);
        //             Assert.Equal(expected.MaxSurfacePressures, actual.MaxSurfacePressures);
        //             Assert.Equal(expected.ToleratedAmbientPressures, actual.ToleratedAmbientPressures);
        //             Assert.Equal(expected.CompartmentLoads, actual.CompartmentLoads);
        //         }

        //         private static void AssertCylinders(IList<ICylinder> expected, IList<ICylinder> actual)
        //         {
        //             Assert.NotEmpty(actual);

        //             for (int index = 0; index < expected.Count - 1; index++)
        //             {
        //                 Assert.Equal(expected[index], actual[index]);
        //             }
        //         }
    }
}