using Newtonsoft.Json;

namespace Magento.RestClient.Modules.AsynchronousOperations.Models
{
	public class OperationItem
	{
		[JsonProperty("id")] public long Id { get; set; }

		[JsonProperty("status")] public OperationStatus Status { get; set; }

		[JsonProperty("result_message")] public string ResultMessage { get; set; }

		[JsonProperty("error_code")] public dynamic ErrorCode { get; set; }
	}
}