using Newtonsoft.Json;

namespace Magento.RestClient.Data.Repositories.Requests
{
	public record CreateOrUpdateConfigurationRequest
	{
		[JsonProperty("sku")] public string Sku { get; set; }
		[JsonProperty("childSku")] public string ChildSku { get; set; }
	}
}