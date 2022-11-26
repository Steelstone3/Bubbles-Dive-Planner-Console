using System;
using BubblesDivePlanner.Models.DiveModels;
using Newtonsoft.Json;

namespace BubblesDivePlanner.Controllers.Json
{
    public class AbstractConverter<TReal, TAbstract> : JsonConverter where TReal : TAbstract
    {
        public override Boolean CanConvert(Type objectType)
            => objectType == typeof(TAbstract);

        public override Object ReadJson(JsonReader reader, Type type, Object value, JsonSerializer jsonSerialiser)
        {
            if (value.GetType() == typeof(FakeUsnRev6))
            {
                return jsonSerialiser.Deserialize<FakeUsnRev6>(reader);
            }
            else if (value.GetType() == typeof(FakeZhl12Buhlmann))
            {
                return jsonSerialiser.Deserialize<FakeZhl12Buhlmann>(reader);
            }
            else if (value.GetType() == typeof(Zhl16Buhlmann))
            {
                return jsonSerialiser.Deserialize<Zhl16Buhlmann>(reader);
            }

            return jsonSerialiser.Deserialize<TReal>(reader);
        }

        public override void WriteJson(JsonWriter writer, Object value, JsonSerializer jsonSerialiser)
            => jsonSerialiser.Serialize(writer, value);
    }
}
