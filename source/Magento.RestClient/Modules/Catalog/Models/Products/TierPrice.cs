using JsonExts.JsonPath;
using Newtonsoft.Json;

namespace Magento.RestClient.Modules.Catalog.Models.Products
{
	[JsonConverter(typeof(JsonPathObjectConverter))]
	public record TierPrice
	{
		[JsonProperty("customer_group_id")] public long CustomerGroupId { get; set; }

		[JsonProperty("qty")] public long Qty { get; set; }

		[JsonProperty("value")] public long Value { get; set; }

		[JsonPath("extension_attributes.website_id")]
		public int WebsiteId { get; set; }
	}
}