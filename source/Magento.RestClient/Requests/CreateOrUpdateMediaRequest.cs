using Magento.RestClient.Modules.Catalog.Models.Products;
using Newtonsoft.Json;

namespace Magento.RestClient.Requests
{
	public record CreateOrUpdateMediaRequest
	{
		[JsonProperty("sku")] public string Sku { get; set; }

		[JsonProperty("entry")] public MediaEntry Entry { get; set; }
	}
}