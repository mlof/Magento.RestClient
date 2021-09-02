using Newtonsoft.Json;

namespace Magento.RestClient.Models
{
	public record InvoiceItem
	{
		[JsonProperty("base_discount_tax_compensation_amount")]
		public long BaseDiscountTaxCompensationAmount { get; set; }

		[JsonProperty("base_price")] public long BasePrice { get; set; }

		[JsonProperty("base_price_incl_tax")] public long BasePriceInclTax { get; set; }

		[JsonProperty("base_row_total")] public long BaseRowTotal { get; set; }

		[JsonProperty("base_row_total_incl_tax")]
		public long BaseRowTotalInclTax { get; set; }

		[JsonProperty("base_tax_amount")] public long BaseTaxAmount { get; set; }

		[JsonProperty("entity_id")] public long EntityId { get; set; }

		[JsonProperty("discount_tax_compensation_amount")]
		public long DiscountTaxCompensationAmount { get; set; }

		[JsonProperty("name")] public string Name { get; set; }

		[JsonProperty("parent_id")] public long ParentId { get; set; }

		[JsonProperty("price")] public long Price { get; set; }

		[JsonProperty("price_incl_tax")] public long PriceInclTax { get; set; }

		[JsonProperty("product_id")] public long ProductId { get; set; }

		[JsonProperty("row_total")] public long RowTotal { get; set; }

		[JsonProperty("row_total_incl_tax")] public long RowTotalInclTax { get; set; }

		[JsonProperty("sku")] public string Sku { get; set; }

		[JsonProperty("tax_amount")] public long TaxAmount { get; set; }

		[JsonProperty("order_item_id")] public long OrderItemId { get; set; }

		[JsonProperty("qty")] public long Qty { get; set; }
	}
}