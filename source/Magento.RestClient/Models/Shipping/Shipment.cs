using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Magento.RestClient.Models.Shipping
{
	public record Shipment
	{
		[JsonProperty("billing_address_id")]
		public long BillingAddressId { get; set; }

		[JsonProperty("created_at")]
		public DateTimeOffset CreatedAt { get; set; }

		[JsonProperty("customer_id")]
		public long CustomerId { get; set; }

		[JsonProperty("entity_id")]
		public long EntityId { get; set; }

		[JsonProperty("increment_id")]
		public string IncrementId { get; set; }

		[JsonProperty("order_id")]
		public long OrderId { get; set; }

		[JsonProperty("packages")]
		public List<dynamic> Packages { get; set; }

		[JsonProperty("shipping_address_id")]
		public long ShippingAddressId { get; set; }

		[JsonProperty("store_id")]
		public long StoreId { get; set; }

		[JsonProperty("total_qty")]
		public long TotalQty { get; set; }

		[JsonProperty("updated_at")]
		public DateTimeOffset UpdatedAt { get; set; }

		[JsonProperty("items")]
		public List<ShipmentItem> Items { get; set; }

		[JsonProperty("tracks")]
		public List<dynamic> Tracks { get; set; }

		[JsonProperty("comments")]
		public List<dynamic> Comments { get; set; }

		[JsonProperty("extension_attributes")]
		public dynamic ExtensionAttributes { get; set; }
	}
}