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
                    //TODO Dirty hack this abstract coverter is providing a default implementation to later be sorted out by the dive plan
                    new AbstractConverter<Zhl16Buhlmann, IDiveModel>(),
                    new AbstractConverter<DivePlan, IDivePlan>(),
                    new AbstractConverter<Cylinder, ICylinder>(),
                    new AbstractConverter<GasMixture, IGasMixture>(),
                    new AbstractConverter<DiveStep, IDiveStep>(),
                    new DiveModelConverter<IDiveModel>(),
                    new AbstractConverter<DiveProfile, IDiveProfile>(),
                },
            };

            var divePlans = JsonConvert.DeserializeObject<List<IDivePlan>>(expectedDivePlanJson, settings);
            var divePlan = divePlans.Last();

            return divePlan ?? null;
        }
    }
}