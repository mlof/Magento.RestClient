using System.Collections.Generic;
using Newtonsoft.Json;

namespace Magento.RestClient.Data.Models
{
	public record CategoryTree : Category.Category
	{
		[JsonProperty("product_count")] public long ProductCount { get; set; }

		[JsonProperty("children_data")] public List<Category.Category> ChildrenData { get; set; }
	}
}