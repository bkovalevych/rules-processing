using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RulesExercise.Application.Helpers
{
    public class CustomDateTimeOffsetConverter : JsonConverter<DateTimeOffset>
	{
		public override void Write(Utf8JsonWriter writer, DateTimeOffset date, JsonSerializerOptions options)
		{
			writer.WriteStringValue(date.ToUnixTimeSeconds().ToString());
		}
		public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var rawValue = reader.GetInt64();
			return DateTimeOffset.FromUnixTimeSeconds(rawValue);
		}
    }
}
