using Newtonsoft.Json;

namespace Magento.RestClient.Data.Models.Catalog.Products
{
	public record ProductOptionValue
	{
		public ProductOptionValue()
		{
		}

		public ProductOptionValue(string title, decimal price)
		{
			this.Title = title;
			this.Price = price;
			this.PriceType = PriceType.Fixed;
		}

		[JsonProperty("title")] public string Title { get; set; }
		[JsonProperty("price")] public decimal Price { get; set; }
		[JsonProperty("price_type", DefaultValueHandling = DefaultValueHandling.Include)] public PriceType PriceType { get; set; }
		[JsonProperty("sku")]
		public string? Sku { get; set; }
	}
}