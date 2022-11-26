using System;
using BubblesDivePlanner.Models.DiveModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BubblesDivePlanner.Controllers.Json
{
    public class DiveModelConverter<TAbstract> : JsonConverter
    {
        public override Boolean CanConvert(Type objectType)
             => objectType == typeof(TAbstract);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (existingValue.GetType() == typeof(FakeUsnRev6))
            {
                return serializer.Deserialize<FakeUsnRev6>(reader);
            }
            else if (existingValue.GetType() == typeof(FakeZhl12Buhlmann))
            {
                return serializer.Deserialize<FakeZhl12Buhlmann>(reader);
            }
            else if (existingValue.GetType() == typeof(Zhl16Buhlmann))
            {
                return serializer.Deserialize<Zhl16Buhlmann>(reader);
            }
            else
            {
                return null;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}