namespace BubblesDivePlanner.Models.DiveModels
{
    public class FakeUsnRev6 : DiveModel
    {
        private const byte NUMBER_OF_COMPARTMENTS = 12;

        public FakeUsnRev6(IDiveProfile diveProfile) : base(diveProfile, null)
        {
            Name = DiveModelNames.FAKE_USN.ToString(); // "Fake USN Revision 6 Model";
            CompartmentCount = NUMBER_OF_COMPARTMENTS;
            NitrogenHalfTimes = new double[NUMBER_OF_COMPARTMENTS] { 4.0, 8.0, 12.5, 18.5, 27.0, 38.3, 54.3, 77.0, 109.0, 146.0, 187.0, 239.0 };
            HeliumHalfTimes = new double[NUMBER_OF_COMPARTMENTS] { 1.51, 3.02, 4.72, 6.99, 10.21, 14.48, 20.53, 29.11, 41.20, 55.19, 70.69, 90.34 };
            AValuesNitrogen = new double[NUMBER_OF_COMPARTMENTS] { 1.2559, 1.0000, 0.8618, 0.7562, 0.6667, 0.5600, 0.4947, 0.4500, 0.4187, 0.3798, 0.3497, 0.3223 };
            BValuesNitrogen = new double[NUMBER_OF_COMPARTMENTS] { 0.5050, 0.6514, 0.7222, 0.7825, 0.8126, 0.8434, 0.8693, 0.8910, 0.9092, 0.9222, 0.9319, 0.9403 };
            AValuesHelium = new double[NUMBER_OF_COMPARTMENTS] { 1.7424, 1.3830, 1.1919, 1.0458, 0.9220, 0.8205, 0.7305, 0.6502, 0.5950, 0.5545, 0.5333, 0.5189 };
            BValuesHelium = new double[NUMBER_OF_COMPARTMENTS] { 0.4245, 0.5747, 0.6527, 0.7223, 0.7582, 0.7957, 0.8279, 0.8553, 0.8757, 0.8903, 0.8997, 0.9073 };
            AssignDiveProfile();
        }
    }
}