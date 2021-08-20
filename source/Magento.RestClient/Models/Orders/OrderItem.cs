using System;
using Newtonsoft.Json;

namespace Magento.RestClient.Models.Orders
{
    public record OrderItem
    {
        [JsonProperty("amount_refunded")] public long AmountRefunded { get; set; }

        [JsonProperty("base_amount_refunded")] public long BaseAmountRefunded { get; set; }

        [JsonProperty("base_discount_amount")] public long BaseDiscountAmount { get; set; }

        [JsonProperty("base_discount_invoiced")]
        public long BaseDiscountInvoiced { get; set; }

        [JsonProperty("base_discount_tax_compensation_amount")]
        public long BaseDiscountTaxCompensationAmount { get; set; }

        [JsonProperty("base_original_price")] public long BaseOriginalPrice { get; set; }

        [JsonProperty("base_price")] public long BasePrice { get; set; }

        [JsonProperty("base_price_incl_tax")] public long BasePriceInclTax { get; set; }

        [JsonProperty("base_row_invoiced")] public long BaseRowInvoiced { get; set; }

        [JsonProperty("base_row_total")] public long BaseRowTotal { get; set; }

        [JsonProperty("base_row_total_incl_tax")]
        public long BaseRowTotalInclTax { get; set; }

        [JsonProperty("base_tax_amount")] public long BaseTaxAmount { get; set; }

        [JsonProperty("base_tax_invoiced")] public long BaseTaxInvoiced { get; set; }

        [JsonProperty("created_at")] public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("discount_amount")] public long DiscountAmount { get; set; }

        [JsonProperty("discount_invoiced")] public long DiscountInvoiced { get; set; }

        [JsonProperty("discount_percent")] public long DiscountPercent { get; set; }

        [JsonProperty("free_shipping")] public long FreeShipping { get; set; }

        [JsonProperty("discount_tax_compensation_amount")]
        public long DiscountTaxCompensationAmount { get; set; }

        [JsonProperty("is_qty_decimal")] public long IsQtyDecimal { get; set; }

        [JsonProperty("is_virtual")] public long IsVirtual { get; set; }

        [JsonProperty("item_id")] public long ItemId { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("no_discount")] public long NoDiscount { get; set; }

        [JsonProperty("order_id")] public long OrderId { get; set; }

        [JsonProperty("original_price")] public long OriginalPrice { get; set; }

        [JsonProperty("price")] public long Price { get; set; }

        [JsonProperty("price_incl_tax")] public long PriceInclTax { get; set; }

        [JsonProperty("product_id")] public long ProductId { get; set; }

        [JsonProperty("product_type")] public string ProductType { get; set; }

        [JsonProperty("qty_canceled")] public long QtyCanceled { get; set; }

        [JsonProperty("qty_invoiced")] public long QtyInvoiced { get; set; }

        [JsonProperty("qty_ordered")] public long QtyOrdered { get; set; }

        [JsonProperty("qty_refunded")] public long QtyRefunded { get; set; }

        [JsonProperty("qty_shipped")] public long QtyShipped { get; set; }

        [JsonProperty("quote_item_id")] public long QuoteItemId { get; set; }

        [JsonProperty("row_invoiced")] public long RowInvoiced { get; set; }

        [JsonProperty("row_total")] public long RowTotal { get; set; }

        [JsonProperty("row_total_incl_tax")] public long RowTotalInclTax { get; set; }

        [JsonProperty("row_weight")] public long RowWeight { get; set; }

        [JsonProperty("sku")] public string Sku { get; set; }

        [JsonProperty("store_id")] public long StoreId { get; set; }

        [JsonProperty("tax_amount")] public long TaxAmount { get; set; }

        [JsonProperty("tax_invoiced")] public long TaxInvoiced { get; set; }

        [JsonProperty("tax_percent")] public long TaxPercent { get; set; }

        [JsonProperty("updated_at")] public DateTimeOffset UpdatedAt { get; set; }
    }
}