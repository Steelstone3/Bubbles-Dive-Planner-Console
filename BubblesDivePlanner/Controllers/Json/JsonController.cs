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
            var converters = AddConverters(expectedDivePlanJson);

            var settings = new JsonSerializerSettings
            {
                Converters = converters
            };

            var divePlans = JsonConvert.DeserializeObject<List<IDivePlan>>(expectedDivePlanJson, settings);
            var divePlan = divePlans.Last();

            return divePlan ?? null;
        }

        private List<JsonConverter> AddConverters(string expectedDivePlanJson)
        {
            List<JsonConverter> jsonConverters = new()
            {
                new AbstractConverter<DivePlan, IDivePlan>(),
                new AbstractConverter<Cylinder, ICylinder>(),
                new AbstractConverter<GasMixture, IGasMixture>(),
                new AbstractConverter<DiveStep, IDiveStep>(),
                new AbstractConverter<DiveProfile, IDiveProfile>(),
            };

            DetermineDiveModel(GetDiveModelName(expectedDivePlanJson), jsonConverters);

            return jsonConverters;
        }

        private string GetDiveModelName(string expectedDivePlanJson)
        {
            if (expectedDivePlanJson.Contains(DiveModelNames.ZHL16.ToString()))
            {
                return DiveModelNames.ZHL16.ToString();
            }
            else if (expectedDivePlanJson.Contains(DiveModelNames.FAKE_ZHL12.ToString()))
            {
                return DiveModelNames.FAKE_ZHL12.ToString();
            }
            else if (expectedDivePlanJson.Contains(DiveModelNames.FAKE_USN.ToString()))
            {
                return DiveModelNames.FAKE_USN.ToString();
            }

            return null;
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