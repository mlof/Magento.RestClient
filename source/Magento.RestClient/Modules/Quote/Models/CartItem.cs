using JsonExts.JsonPath;
using Newtonsoft.Json;

namespace Magento.RestClient.Modules.Quote.Models
{
	[JsonConverter(typeof(JsonPathObjectConverter))]
	public record CartItem
	{
		[JsonProperty("item_id")] public long? ItemId { get; set; }

		[JsonProperty("sku")] public string Sku { get; set; }

		[JsonProperty("qty")] public long Qty { get; set; }

		[JsonProperty("name")] public string Name { get; set; }

		[JsonProperty("price")] public long? Price { get; set; }

		[JsonProperty("product_type")] public string ProductType { get; set; }

		[JsonProperty("quote_id")] public long QuoteId { get; set; }
	}
}