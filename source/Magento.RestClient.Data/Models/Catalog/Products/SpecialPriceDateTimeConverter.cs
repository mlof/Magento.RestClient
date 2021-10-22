using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace Magento.RestClient.Data.Models.Catalog.Products
{
	public class SpecialPriceDateTimeConverter : DateTimeConverterBase
	{
	

		public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
		{
			writer.WriteValue(((DateTime)value).ToString("yyyy-MM-dd hh:mm:ss"));
		}

		public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}
}