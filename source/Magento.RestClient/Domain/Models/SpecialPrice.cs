using System;
using Newtonsoft.Json;

namespace Magento.RestClient.Domain.Models
{
	public class SpecialPrice
	{
		[JsonProperty("price")] public decimal Price { get; set; }

		[JsonProperty("store_id", NullValueHandling = NullValueHandling.Include)]
		public long? StoreId { get; set; }

		[JsonProperty("sku")] public string Sku { get; set; }

		[JsonProperty("price_from", ItemConverterType = typeof(SpecialPriceDateTimeConverter))]
		public DateTime PriceFrom { get; set; }

		[JsonProperty("price_to", ItemConverterType = typeof(SpecialPriceDateTimeConverter))]
		public DateTime PriceTo { get; set; }
	}
}