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
            if (type == typeof(DiveModel))
            {
                // TODO Read the dive model name from the file then return the casted type based on the name
                var bob = value as DiveModel;
                
                if(bob.Name == DiveModelNames.ZHL16.ToString()) {
                    return jsonSerialiser.Deserialize<Zhl16Buhlmann>(reader);
                }
            }

            return jsonSerialiser.Deserialize<TReal>(reader);
        }

        public override void WriteJson(JsonWriter writer, Object value, JsonSerializer jsonSerialiser)
            => jsonSerialiser.Serialize(writer, value);
    }
}
