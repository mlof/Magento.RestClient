using Newtonsoft.Json;

namespace Magento.RestClient.Data.Models.Catalog.Products
{
	public record ProductLink
	{
		[JsonProperty("category_id")] public long CategoryId { get; set; }
		[JsonProperty("sku")] public string Sku { get; set; }

		[JsonProperty("position")] public int? Position { get; set; }
	}
}