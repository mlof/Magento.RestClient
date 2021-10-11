using Newtonsoft.Json;

namespace Magento.RestClient.Extensions
{
	public class CreateOrUpdateConfigurationRequest
	{
		[JsonProperty("sku")] public string Sku { get; set; }
		[JsonProperty("childSku")] public string ChildSku { get; set; }
	}
}