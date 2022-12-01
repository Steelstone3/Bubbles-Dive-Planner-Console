using System.Collections.Generic;
using System.Linq;
using BubblesDivePlanner.Models;
using BubblesDivePlanner.Models.Cylinders;
using BubblesDivePlanner.Models.DiveModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            var converters = AddConverters();

            var settings = new JsonSerializerSettings
            {
                Converters = converters
            };

            var divePlans = JsonConvert.DeserializeObject<List<IDivePlan>>(expectedDivePlanJson, settings);
            var divePlan = divePlans.Last();

            return divePlan ?? null;
        }

        private List<JsonConverter> AddConverters()
        {
            List<JsonConverter> jsonConverters = new()
            {
                new AbstractConverter<DivePlan, IDivePlan>(),
                new AbstractConverter<Cylinder, ICylinder>(),
                new AbstractConverter<GasMixture, IGasMixture>(),
                new AbstractConverter<DiveStep, IDiveStep>(),
                new AbstractConverter<DiveProfile, IDiveProfile>(),
            };

            DetermineDiveModel(GetDiveModelName(), jsonConverters);

            return jsonConverters;
        }

        private string GetDiveModelName()
        {
            // TODO get the dive model name from the file
            return DiveModelNames.FAKE_USN.ToString();
        }

        private void DetermineDiveModel(string diveModelName, List<JsonConverter> jsonConverters)
        {
            if (diveModelName == DiveModelNames.ZHL16.ToString())
            {
                jsonConverters.Add(new AbstractConverter<Zhl16Buhlmann, IDiveModel>());
            }
            else if (diveModelName == DiveModelNames.FAKE_ZHL12.ToString())
            {
                jsonConverters.Add(new AbstractConverter<FakeZhl12Buhlmann, IDiveModel>());
            }
            else if (diveModelName == DiveModelNames.FAKE_USN.ToString())
            {
                jsonConverters.Add(new AbstractConverter<FakeUsnRev6, IDiveModel>());
            }
        }
    }
}