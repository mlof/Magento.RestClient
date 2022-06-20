using System.Collections.Generic;
using Newtonsoft.Json;

namespace Magento.RestClient.Modules.Catalog.Models.Category
{
	public record CategoryTree : Category
	{
		[JsonProperty("product_count")] public long? ProductCount { get; set; }

		[JsonProperty("children_data")] public List<CategoryTree> ChildrenData { get; set; }
	}
}