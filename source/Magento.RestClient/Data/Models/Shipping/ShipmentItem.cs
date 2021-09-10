using Newtonsoft.Json;

namespace Magento.RestClient.Data.Models.Shipping
{
	public record ShipmentItem
	{
		[JsonProperty("entity_id")] public long EntityId { get; set; }

		[JsonProperty("name")] public string Name { get; set; }

		[JsonProperty("parent_id")] public long ParentId { get; set; }

		[JsonProperty("price")] public long Price { get; set; }

		[JsonProperty("product_id")] public long ProductId { get; set; }

		[JsonProperty("sku")] public string Sku { get; set; }

		[JsonProperty("order_item_id")] public long OrderItemId { get; set; }

		[JsonProperty("qty")] public long Qty { get; set; }
	}
}