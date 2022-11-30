using System.Text.Json.Serialization;
using BubblesDivePlanner.Models.DiveModels;

namespace BubblesDivePlanner.Controllers.Json
{
    public class DiveModel : IDiveModel
    {
        [JsonConstructor]
        public DiveModel(IDiveProfile diveProfile)
        {
            switch (diveProfile)
            {
                case null:
                    DiveProfile = new DiveProfile(CompartmentCount);
                    break;
                default:
                    DiveProfile = diveProfile;
                    break;
            }
        }

        public string Name { get; protected set; }
        public byte CompartmentCount { get; protected set; }
        public double[] NitrogenHalfTimes { get; protected set; }
        public double[] HeliumHalfTimes { get; protected set; }
        public double[] AValuesNitrogen { get; protected set; }
        public double[] BValuesNitrogen { get; protected set; }
        public double[] AValuesHelium { get; protected set; }
        public double[] BValuesHelium { get; protected set; }
        public IDiveProfile DiveProfile { get; protected set; }
    }
}