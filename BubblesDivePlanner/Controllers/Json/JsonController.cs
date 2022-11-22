using System.Collections.Generic;
using System.Linq;
using BubblesDivePlanner.Models;
using BubblesDivePlanner.Models.Cylinders;
using BubblesDivePlanner.Models.DiveModels;
using Newtonsoft.Json;

namespace BubblesDivePlanner.Controllers.Json
{
    public class JsonController : IJsonController
    {
        // TODO write a test for json controller for just unix with a fake data in the dive plans (2)
        public string Serialise(List<IDivePlan> divePlans)
        {
            return JsonConvert.SerializeObject(divePlans, Formatting.Indented);
        }

        public IDivePlan Deserialise(string expectedDivePlanJson)
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

            var divePlans = JsonConvert.DeserializeObject<List<IDivePlan>>(expectedDivePlanJson, settings);
            var divePlan = divePlans.Last();

            return divePlan ?? null;
        }
    }
}