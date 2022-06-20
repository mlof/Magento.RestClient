using Newtonsoft.Json;

namespace Magento.RestClient.Modules.Inventory.Models
{
	public record InventoryStock
	{
		[JsonProperty("stock_id")] public long StockId { get; set; }

		[JsonProperty("name")] public string Name { get; set; }

		[JsonProperty("extension_attributes")] public dynamic ExtensionAttributes { get; set; }
	}
}