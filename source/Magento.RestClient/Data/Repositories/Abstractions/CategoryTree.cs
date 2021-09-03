using System.Collections.Generic;
using Magento.RestClient.Data.Models.Category;
using Newtonsoft.Json;

namespace Magento.RestClient.Data.Repositories.Abstractions
{
	public record CategoryTree : Category
	{
		[JsonProperty("product_count")] public long ProductCount { get; set; }

		[JsonProperty("children_data")] public List<Category> ChildrenData { get; set; }
	}
}