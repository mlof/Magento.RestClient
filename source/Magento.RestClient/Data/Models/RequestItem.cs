using Newtonsoft.Json;

namespace Magento.RestClient.Data.Models
{
	public partial class RequestItem
	{
		[JsonProperty("id")] public long Id { get; set; }

		[JsonProperty("data_hash")] public string DataHash { get; set; }

		[JsonProperty("status")] public string Status { get; set; }
	}
}