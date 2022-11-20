using System.Collections.Generic;
using BubblesDivePlanner.Controllers.Json;
using BubblesDivePlanner.Models.Cylinders;
using BubblesDivePlanner.Models.DiveModels;
using Newtonsoft.Json;

namespace BubblesDivePlanner.Models
{
    public class DivePlan : IDivePlan
    {
        public DivePlan(IDiveModel diveModel, IList<ICylinder> cylinders, IDiveStep diveStep, ICylinder selectedCylinder)
        {
            DiveModel = diveModel;
            Cylinders = cylinders;
            DiveStep = diveStep;
            SelectedCylinder = selectedCylinder;
        }

        public IDiveModel DiveModel { get; private set; }
        public IList<ICylinder> Cylinders { get; private set; }
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
                    new AbstractConverter<DivePlan, IDivePlan>(),
                    new AbstractConverter<Zhl16Buhlmann, IDiveModel>(),
                    new AbstractConverter<DiveProfile, IDiveProfile>(),
                    new AbstractConverter<Cylinder, ICylinder>(),
                    new AbstractConverter<GasMixture, IGasMixture>(),
                    new AbstractConverter<DiveStep, IDiveStep>(),
                },
            };

            var divePlan = JsonConvert.DeserializeObject<IDivePlan>(expectedDivePlanJson, settings);

            DiveModel = divePlan.DiveModel;
            Cylinders = divePlan.Cylinders;
        }
    }
}