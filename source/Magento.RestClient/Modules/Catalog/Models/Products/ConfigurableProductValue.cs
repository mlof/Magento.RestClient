using Newtonsoft.Json;

namespace Magento.RestClient.Modules.Catalog.Models.Products
{
	public class ConfigurableProductValue
	{
		[JsonProperty("value_index")] public long ValueIndex { get; set; }
	}
}