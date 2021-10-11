using Magento.RestClient.Data.Models.Catalog.Products;
using Newtonsoft.Json;

namespace Magento.RestClient.Data.Repositories
{
	public class ConfigurableProductOptionRequest
	{
		[JsonProperty("sku")] public string Sku { get; set; }

		[JsonProperty("option")] public ConfigurableProductOption Option { get; set; }
	}
}