using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Magento.RestClient.Data.Models.Inventory
{
	public record InventorySourceItem
	{
		[JsonProperty("sku")] public string Sku { get; set; }

		[JsonProperty("source_code")] public string SourceCode { get; set; }

		[JsonProperty("quantity", DefaultValueHandling = DefaultValueHandling.Include)]
		public long Quantity { get; set; }

		[JsonProperty("status", DefaultValueHandling = DefaultValueHandling.Include)]
		public InventorySourceItemStatus Status { get; set; }

		[JsonProperty("extension_attributes")] public dynamic ExtensionAttributes { get; set; }
	}
}