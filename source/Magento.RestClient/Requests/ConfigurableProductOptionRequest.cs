using Magento.RestClient.Modules.Catalog.Models.Products;
using Newtonsoft.Json;

namespace Magento.RestClient.Requests
{
	public class ConfigurableProductOptionRequest
	{
		[JsonProperty("sku")] public string Sku { get; set; }

		[JsonProperty("option")] public ConfigurableProductOption Option { get; set; }
	}
}