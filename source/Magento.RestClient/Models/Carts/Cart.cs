using System;
using System.Collections.Generic;
using Magento.RestClient.Models.Common;
using Magento.RestClient.Models.Customers;
using Newtonsoft.Json;

namespace Magento.RestClient.Models.Carts
{
	public record Cart
	{
		#region Properties

		[JsonProperty("created_at")] public DateTimeOffset CreatedAt { get; set; }

		[JsonProperty("updated_at")] public DateTimeOffset UpdatedAt { get; set; }

		[JsonProperty("is_active")] public bool IsActive { get; set; }

		[JsonProperty("is_virtual")] public bool IsVirtual { get; set; }

		[JsonProperty("items")] public List<CartItem> Items { get; set; }

		[JsonProperty("items_count")] public long ItemsCount { get; set; }

		[JsonProperty("items_qty")] public long ItemsQty { get; set; }

		[JsonProperty("customer")] public Customer Customer { get; set; }

		[JsonProperty("billing_address")] public BillingAddress BillingAddress { get; set; }

		[JsonProperty("orig_order_id")] public long OrigOrderId { get; set; }

		[JsonProperty("currency")] public Currency Currency { get; set; }

		[JsonProperty("customer_is_guest")] public bool CustomerIsGuest { get; set; }

		[JsonProperty("customer_note_notify")] public bool CustomerNoteNotify { get; set; }

		[JsonProperty("customer_tax_class_id")]
		public long CustomerTaxClassId { get; set; }

		[JsonProperty("store_id")] public long StoreId { get; set; }

		[JsonProperty("extension_attributes")] public ExtensionAttributes ExtensionAttributes { get; set; }

		#endregion
	}
}