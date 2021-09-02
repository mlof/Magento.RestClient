using Newtonsoft.Json;

namespace Magento.RestClient.Models.Attributes
{
	public record Option
	{
		[JsonProperty("label")] public string Label { get; set; }

		[JsonProperty("value")] public string Value { get; set; }

		[JsonProperty("is_default", NullValueHandling = NullValueHandling.Ignore)]
		public bool? IsDefault { get; set; }
	}

}