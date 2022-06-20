using Newtonsoft.Json;

namespace Magento.RestClient.Modules.AsynchronousOperations.Models
{
	public class RequestItem
	{
		[JsonProperty("id")] public long Id { get; set; }

		[JsonProperty("data_hash")] public string DataHash { get; set; }

		[JsonProperty("status")] public string Status { get; set; }
	}
}