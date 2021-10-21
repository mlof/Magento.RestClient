using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Magento.RestClient.Domain.Converters
{
	public class SpecialPriceDateTimeConverter : DateTimeConverterBase
	{
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
			JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
		{
			writer.WriteValue(((DateTime)value).ToString("yyyy-MM-dd hh:mm:ss"));
		}
	}
}