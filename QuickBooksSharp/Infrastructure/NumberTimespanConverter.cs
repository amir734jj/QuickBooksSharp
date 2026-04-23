using System;
using Newtonsoft.Json;

namespace QuickBooksSharp.Infrastructure
{
    public class NumberTimespanConverter : JsonConverter<TimeSpan>
    {
        public override TimeSpan ReadJson(JsonReader reader, Type objectType, TimeSpan existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return TimeSpan.FromSeconds(Convert.ToInt32(reader.Value));
        }

        public override void WriteJson(JsonWriter writer, TimeSpan value, JsonSerializer serializer)
        {
            writer.WriteValue((int)value.TotalSeconds);
        }
    }
}
