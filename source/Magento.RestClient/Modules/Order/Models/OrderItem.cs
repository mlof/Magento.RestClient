using System;
using Magento.RestClient.Modules.Catalog.Models.Products;
using Newtonsoft.Json;

namespace Magento.RestClient.Modules.Order.Models
{
	public record OrderItem
	{
		[JsonProperty("amount_refunded")] public decimal AmountRefunded { get; set; }

		[JsonProperty("base_amount_refunded")] public decimal BaseAmountRefunded { get; set; }

		[JsonProperty("base_discount_amount")] public decimal BaseDiscountAmount { get; set; }

		[JsonProperty("base_discount_invoiced")]
		public decimal BaseDiscountInvoiced { get; set; }

		[JsonProperty("base_discount_tax_compensation_amount")]
		public decimal BaseDiscountTaxCompensationAmount { get; set; }

		[JsonProperty("base_original_price")] public decimal BaseOriginalPrice { get; set; }

		[JsonProperty("base_price")] public decimal BasePrice { get; set; }

		[JsonProperty("base_price_incl_tax")] public decimal BasePriceInclTax { get; set; }

		[JsonProperty("base_row_invoiced")] public decimal BaseRowInvoiced { get; set; }

		[JsonProperty("base_row_total")] public decimal BaseRowTotal { get; set; }

		[JsonProperty("base_row_total_incl_tax")]
		public decimal BaseRowTotalInclTax { get; set; }

		[JsonProperty("base_tax_amount")] public decimal BaseTaxAmount { get; set; }

		[JsonProperty("base_tax_invoiced")] public decimal BaseTaxInvoiced { get; set; }

		[JsonProperty("created_at")] public DateTimeOffset CreatedAt { get; set; }

		[JsonProperty("discount_amount")] public decimal DiscountAmount { get; set; }

		[JsonProperty("discount_invoiced")] public decimal DiscountInvoiced { get; set; }

		[JsonProperty("discount_percent")] public decimal DiscountPercent { get; set; }

		[JsonProperty("free_shipping")] public decimal FreeShipping { get; set; }

		[JsonProperty("discount_tax_compensation_amount")]
		public decimal DiscountTaxCompensationAmount { get; set; }

		[JsonProperty("is_qty_decimal")] public bool IsQtyDecimal { get; set; }

		[JsonProperty("is_virtual")] public bool IsVirtual { get; set; }

		[JsonProperty("item_id")] public decimal ItemId { get; set; }

		[JsonProperty("name")] public string Name { get; set; }

		[JsonProperty("no_discount")] public decimal NoDiscount { get; set; }

		[JsonProperty("order_id")] public long OrderId { get; set; }

		[JsonProperty("original_price")] public decimal OriginalPrice { get; set; }

		[JsonProperty("price")] public decimal Price { get; set; }

		[JsonProperty("price_incl_tax")] public decimal PriceInclTax { get; set; }

		[JsonProperty("product_id")] public long ProductId { get; set; }

		[JsonProperty("product_type")] public ProductType ProductType { get; set; }

		[JsonProperty("qty_canceled")] public long QtyCanceled { get; set; }

		[JsonProperty("qty_invoiced")] public long QtyInvoiced { get; set; }

		[JsonProperty("qty_ordered")] public long QtyOrdered { get; set; }

		[JsonProperty("qty_refunded")] public long QtyRefunded { get; set; }

		[JsonProperty("qty_shipped")] public long QtyShipped { get; set; }

		[JsonProperty("quote_item_id")] public long QuoteItemId { get; set; }

		[JsonProperty("row_invoiced")] public decimal RowInvoiced { get; set; }

		[JsonProperty("row_total")] public decimal RowTotal { get; set; }

		[JsonProperty("row_total_incl_tax")] public decimal RowTotalInclTax { get; set; }

		[JsonProperty("row_weight")] public decimal RowWeight { get; set; }

		[JsonProperty("sku")] public string Sku { get; set; }

		[JsonProperty("store_id")] public long StoreId { get; set; }

		[JsonProperty("tax_amount")] public decimal TaxAmount { get; set; }

		[JsonProperty("tax_invoiced")] public decimal TaxInvoiced { get; set; }

		[JsonProperty("tax_percent")] public decimal TaxPercent { get; set; }

		[JsonProperty("updated_at")] public DateTimeOffset UpdatedAt { get; set; }

		[JsonProperty("parent_item")] public OrderItem ParentItem { get; set; }
	}
}