using Newtonsoft.Json;

namespace Magento.RestClient.Context
{
	public record InventorySourceItem
	{
		[JsonProperty("sku")]
		public string Sku { get; set; }

		[JsonProperty("source_code")]
		public string SourceCode { get; set; }

		[JsonProperty("quantity")]
		public long Quantity { get; set; }

		[JsonProperty("status")]
		public long Status { get; set; }

		[JsonProperty("extension_attributes")]
		public dynamic ExtensionAttributes { get; set; }
	}
}