using System.Collections.Generic;
using BubblesDivePlanner.Controllers.Json;
using BubblesDivePlanner.Models.Cylinders;
using BubblesDivePlanner.Models.DiveModels;
using Newtonsoft.Json;

namespace BubblesDivePlanner.Models.DivePlans
{
    public class DivePlan : IDivePlan
    {
        public DivePlan(IDivePlanner divePlanner, IDiveModel diveModel, List<ICylinder> cylinders, IDiveStep diveStep, ICylinder selectedCylinder)
        {
            DivePlanner = divePlanner ?? new DivePlanner(null, null, null, null);
            DiveModel = diveModel;
            Cylinders = cylinders;
            DiveStep = diveStep;
            SelectedCylinder = selectedCylinder;
        }

        public IDivePlanner DivePlanner { get; private set; }
        public IDiveModel DiveModel { get; private set; }
        public List<ICylinder> Cylinders { get; private set; }
        public IDiveStep DiveStep { get; private set; }
        public ICylinder SelectedCylinder { get; private set; }

        public string Serialise()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public void Deserialise(string expectedDivePlanJson)
        {
            var settings = new JsonSerializerSettings
            {
                Converters =
                {
                    new AbstractConverter<DivePlanner, IDivePlanner>(),
                    new AbstractConverter<DivePlan, IDivePlan>(),
                    new AbstractConverter<Zhl16Buhlmann, IDiveModel>(),
                    new AbstractConverter<DiveProfile, IDiveProfile>(),
                    new AbstractConverter<Cylinder, ICylinder>(),
                    new AbstractConverter<GasMixture, IGasMixture>(),
                    new AbstractConverter<DiveStep, IDiveStep>(),
                },
            };

            var divePlan = JsonConvert.DeserializeObject<IDivePlan>(expectedDivePlanJson, settings);

            if (divePlan != null)
            {
                DiveModel = divePlan.DiveModel;
                Cylinders = divePlan.Cylinders;
            }
        }
    }
}