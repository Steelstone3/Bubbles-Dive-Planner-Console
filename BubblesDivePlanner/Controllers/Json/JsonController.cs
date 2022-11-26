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
                    new AbstractConverter<Zhl16Buhlmann, IDiveModel>(),
                    new AbstractConverter<FakeUsnRev6, IDiveModel>(),
                    new AbstractConverter<FakeZhl12Buhlmann, IDiveModel>(),
                    new AbstractConverter<DivePlan, IDivePlan>(),
                    new AbstractConverter<Cylinder, ICylinder>(),
                    new AbstractConverter<GasMixture, IGasMixture>(),
                    new AbstractConverter<DiveStep, IDiveStep>(),
                    new AbstractConverter<DiveProfile, IDiveProfile>(),
                },
            };

            var divePlans = JsonConvert.DeserializeObject<List<IDivePlan>>(expectedDivePlanJson, settings);
            var divePlan = divePlans.Last();

            return divePlan ?? null;
        }
    }
}