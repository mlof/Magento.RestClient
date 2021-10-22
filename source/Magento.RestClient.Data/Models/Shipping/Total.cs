using Newtonsoft.Json;

namespace Magento.RestClient.Data.Models.Shipping
{
	public record Total
	{
		[JsonProperty("base_shipping_amount")] public long BaseShippingAmount { get; set; }

		[JsonProperty("base_shipping_discount_amount")]
		public long BaseShippingDiscountAmount { get; set; }

		[JsonProperty("base_shipping_discount_tax_compensation_amnt")]
		public long BaseShippingDiscountTaxCompensationAmnt { get; set; }

		[JsonProperty("base_shipping_incl_tax")]
		public long BaseShippingInclTax { get; set; }

		[JsonProperty("base_shipping_tax_amount")]
		public long BaseShippingTaxAmount { get; set; }

		[JsonProperty("shipping_amount")] public long ShippingAmount { get; set; }

		[JsonProperty("shipping_discount_amount")]
		public long ShippingDiscountAmount { get; set; }

		[JsonProperty("shipping_discount_tax_compensation_amount")]
		public long ShippingDiscountTaxCompensationAmount { get; set; }

		[JsonProperty("shipping_incl_tax")] public long ShippingInclTax { get; set; }

		[JsonProperty("shipping_tax_amount")] public long ShippingTaxAmount { get; set; }
	}
}