using System.Collections.Generic;
using Newtonsoft.Json;

namespace Magento.RestClient.Data.Models.Catalog.Products
{
	public record ProductOption
	{
		[JsonProperty("productSku")] public string ProductSku { get; set; }
		[JsonProperty("title")] public string Title { get; set; }
		[JsonProperty("type")] public ProductOptionType Type { get; set; }


		[JsonProperty("sort_order")] public int? SortOrder { get; set; }
		[JsonProperty("is_require", DefaultValueHandling = DefaultValueHandling.Include)] public bool IsRequired { get; set; }

		[JsonProperty("values")] public List<ProductOptionValue> Values { get; set; } = new List<ProductOptionValue>();
	}
}