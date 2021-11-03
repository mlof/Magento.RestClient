using System;
using System.Collections.Generic;
using Magento.RestClient.Data.Models.Common;
using Magento.RestClient.Data.Models.Payments;
using Newtonsoft.Json;

namespace Magento.RestClient.Data.Models.Orders
{
	public record Order 
	{
		[JsonProperty("base_currency_code")] public string BaseCurrencyCode { get; set; }

		[JsonProperty("base_discount_amount")] public long BaseDiscountAmount { get; set; }

		[JsonProperty("base_grand_total")] public long BaseGrandTotal { get; set; }

		[JsonProperty("base_discount_tax_compensation_amount")]
		public long BaseDiscountTaxCompensationAmount { get; set; }

		[JsonProperty("base_shipping_amount")] public long BaseShippingAmount { get; set; }

		[JsonProperty("base_shipping_discount_amount")]
		public long BaseShippingDiscountAmount { get; set; }

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

		[JsonProperty("base_total_due")] public long BaseTotalDue { get; set; }

		[JsonProperty("base_to_global_rate")] public long BaseToGlobalRate { get; set; }

		[JsonProperty("base_to_order_rate")] public long BaseToOrderRate { get; set; }

		[JsonProperty("billing_address_id")] public long BillingAddressId { get; set; }

		[JsonProperty("created_at")] public DateTimeOffset CreatedAt { get; set; }

		[JsonProperty("customer_dob")] public DateTimeOffset CustomerDob { get; set; }

		[JsonProperty("customer_email")] public string CustomerEmail { get; set; }

		[JsonProperty("customer_firstname")] public string CustomerFirstname { get; set; }

		[JsonProperty("customer_gender")] public long CustomerGender { get; set; }

		[JsonProperty("customer_group_id")] public long CustomerGroupId { get; set; }

		[JsonProperty("customer_id")] public long CustomerId { get; set; }

		[JsonProperty("customer_is_guest")] public long CustomerIsGuest { get; set; }

		[JsonProperty("customer_lastname")] public string CustomerLastname { get; set; }

		[JsonProperty("customer_middlename")] public string CustomerMiddlename { get; set; }

		[JsonProperty("customer_note_notify")] public long CustomerNoteNotify { get; set; }

		[JsonProperty("discount_amount")] public long DiscountAmount { get; set; }

		[JsonProperty("entity_id")] public long EntityId { get; set; }

		[JsonProperty("global_currency_code")] public string GlobalCurrencyCode { get; set; }

		[JsonProperty("grand_total")] public long GrandTotal { get; set; }

		[JsonProperty("discount_tax_compensation_amount")]
		public long DiscountTaxCompensationAmount { get; set; }

		[JsonProperty("increment_id")] public string IncrementId { get; set; }

		[JsonProperty("is_virtual")] public long IsVirtual { get; set; }

		[JsonProperty("order_currency_code")] public string OrderCurrencyCode { get; set; }

		[JsonProperty("protect_code")] public string ProtectCode { get; set; }

		[JsonProperty("quote_id")] public long QuoteId { get; set; }

		[JsonProperty("shipping_amount")] public long ShippingAmount { get; set; }

		[JsonProperty("shipping_description")] public string ShippingDescription { get; set; }

		[JsonProperty("shipping_discount_amount")]
		public long ShippingDiscountAmount { get; set; }

		[JsonProperty("shipping_discount_tax_compensation_amount")]
		public long ShippingDiscountTaxCompensationAmount { get; set; }

		[JsonProperty("shipping_incl_tax")] public long ShippingInclTax { get; set; }

		[JsonProperty("shipping_tax_amount")] public long ShippingTaxAmount { get; set; }

		[JsonProperty("state")] public string State { get; set; }

		[JsonProperty("status")] public string Status { get; set; }

		[JsonProperty("store_currency_code")] public string StoreCurrencyCode { get; set; }

		[JsonProperty("store_id")] public long StoreId { get; set; }

		[JsonProperty("store_name")] public string StoreName { get; set; }

		[JsonProperty("store_to_base_rate")] public long StoreToBaseRate { get; set; }

		[JsonProperty("store_to_order_rate")] public long StoreToOrderRate { get; set; }

		[JsonProperty("subtotal")] public long Subtotal { get; set; }

		[JsonProperty("subtotal_incl_tax")] public long SubtotalInclTax { get; set; }

		[JsonProperty("tax_amount")] public long TaxAmount { get; set; }

		[JsonProperty("total_due")] public long TotalDue { get; set; }

		[JsonProperty("total_item_count")] public long TotalItemCount { get; set; }

		[JsonProperty("total_qty_ordered")] public long TotalQtyOrdered { get; set; }

		[JsonProperty("updated_at")] public DateTimeOffset UpdatedAt { get; set; }

		[JsonProperty("weight")] public long Weight { get; set; }

		[JsonProperty("items")] public List<OrderItem> Items { get; set; } = new List<OrderItem>();

		[JsonProperty("billing_address")] public OrderAddress BillingAddress { get; set; }

		[JsonProperty("payment")] public Payment Payment { get; set; }

		[JsonProperty("status_histories")] public List<StatusHistory> StatusHistories { get; set; }

		[JsonProperty("extension_attributes")] 
		public Dictionary<string, dynamic> ExtensionAttributes { get; set; } = new Dictionary<string, dynamic>();

	}


}