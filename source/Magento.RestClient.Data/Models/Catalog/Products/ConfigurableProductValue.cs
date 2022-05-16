using Newtonsoft.Json;

namespace Magento.RestClient.Data.Models.Catalog.Products
{
	public class ConfigurableProductValue
	{
		[JsonProperty("value_index")] public long ValueIndex { get; set; }
	}
}