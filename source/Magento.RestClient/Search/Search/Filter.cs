using Newtonsoft.Json;

namespace Magento.RestClient.Search.Search
{
	public record Filter
	{
		[JsonProperty("field")] public string Field { get; set; }
		[JsonProperty("value")] public string Value { get; set; }
		[JsonProperty("condition_type")] public string ConditionType { get; set; }
	}
}