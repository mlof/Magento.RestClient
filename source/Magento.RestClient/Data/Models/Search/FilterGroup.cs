using System.Collections.Generic;
using Newtonsoft.Json;

namespace Magento.RestClient.Data.Models.Search
{
	public record FilterGroup
	{
		[JsonProperty("filters")] public List<Filter> Filters { get; set; }
	}
}