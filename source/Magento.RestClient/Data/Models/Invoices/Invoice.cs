using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Magento.RestClient.Data.Models.Invoices
{
	public record Invoice
	{
		[JsonProperty("base_currency_code")] public string BaseCurrencyCode { get; set; }

		[JsonProperty("base_discount_amount")] public long BaseDiscountAmount { get; set; }

		[JsonProperty("base_grand_total")] public long BaseGrandTotal { get; set; }

		[JsonProperty("base_discount_tax_compensation_amount")]
		public long BaseDiscountTaxCompensationAmount { get; set; }

		[JsonProperty("base_shipping_amount")] public long BaseShippingAmount { get; set; }

		[JsonProperty("base_shipping_discount_tax_compensation_amnt")]
		public long BaseShippingDiscountTaxCompensationAmnt { get; set; }

		[JsonProperty("base_shipping_incl_tax")]
		public long BaseShippingInclTax { get; set; }

		[JsonProperty("base_shipping_tax_amount")]
		public long BaseShippingTaxAmount { get; set; }

		[JsonProperty("base_subtotal")] public long BaseSubtotal { get; set; }

		[JsonProperty("base_subtotal_incl_tax")]
		public long BaseSubtotalInclTax { get; set; }

		[JsonProperty("base_tax_amount")] public long BaseTaxAmount { get; set; }

		[JsonProperty("base_to_global_rate")] public long BaseToGlobalRate { get; set; }

		[JsonProperty("base_to_order_rate")] public long BaseToOrderRate { get; set; }

		[JsonProperty("billing_address_id")] public long BillingAddressId { get; set; }

		[JsonProperty("can_void_flag")] public long CanVoidFlag { get; set; }

		[JsonProperty("created_at")] public DateTimeOffset CreatedAt { get; set; }

		[JsonProperty("discount_amount")] public long DiscountAmount { get; set; }

		[JsonProperty("entity_id")] public long EntityId { get; set; }

		[JsonProperty("global_currency_code")] public string GlobalCurrencyCode { get; set; }

		[JsonProperty("grand_total")] public long GrandTotal { get; set; }

		[JsonProperty("discount_tax_compensation_amount")]
		public long DiscountTaxCompensationAmount { get; set; }

		[JsonProperty("increment_id")] public string IncrementId { get; set; }

		[JsonProperty("order_currency_code")] public string OrderCurrencyCode { get; set; }

		[JsonProperty("order_id")] public long OrderId { get; set; }

		[JsonProperty("shipping_address_id")] public long ShippingAddressId { get; set; }

		[JsonProperty("shipping_amount")] public long ShippingAmount { get; set; }

		[JsonProperty("shipping_discount_tax_compensation_amount")]
		public long ShippingDiscountTaxCompensationAmount { get; set; }

		[JsonProperty("shipping_incl_tax")] public long ShippingInclTax { get; set; }

		[JsonProperty("shipping_tax_amount")] public long ShippingTaxAmount { get; set; }

		[JsonProperty("state")] public long State { get; set; }

		[JsonProperty("store_currency_code")] public string StoreCurrencyCode { get; set; }

		[JsonProperty("store_id")] public long StoreId { get; set; }

		[JsonProperty("store_to_base_rate")] public long StoreToBaseRate { get; set; }

		[JsonProperty("store_to_order_rate")] public long StoreToOrderRate { get; set; }

		[JsonProperty("subtotal")] public long Subtotal { get; set; }

		[JsonProperty("subtotal_incl_tax")] public long SubtotalInclTax { get; set; }

		[JsonProperty("tax_amount")] public long TaxAmount { get; set; }

		[JsonProperty("total_qty")] public long TotalQty { get; set; }

		[JsonProperty("updated_at")] public DateTimeOffset UpdatedAt { get; set; }

		[JsonProperty("items")] public List<InvoiceItem> Items { get; set; }

		[JsonProperty("comments")] public List<dynamic> Comments { get; set; }
	}
}