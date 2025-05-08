using Newtonsoft.Json;
using System.Globalization;

namespace BasketballAcademyManagementSystemAPI.Common.Converter
{
    public class DateOnlyConverter : JsonConverter<DateOnly?>
    {
        private const string Format = "dd/MM/yyyy";

        public override DateOnly? ReadJson(JsonReader reader, Type objectType, DateOnly? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.Value == null) return null;

            if (DateOnly.TryParseExact(reader.Value.ToString(), Format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            {
                return date;
            }

            throw new JsonSerializationException($"Invalid date format: {reader.Value}");
        }

        public override void WriteJson(JsonWriter writer, DateOnly? value, JsonSerializer serializer)
        {
            writer.WriteValue(value?.ToString(Format));
        }
    }
}
