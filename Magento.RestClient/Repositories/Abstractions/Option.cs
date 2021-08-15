using Newtonsoft.Json;

namespace Magento.RestClient.Repositories.Abstractions
{
    public partial class Option
    {
        [JsonProperty("label")] public string Label { get; set; }

        [JsonProperty("value")] public string Value { get; set; }

        [JsonProperty("is_default", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsDefault { get; set; }
    }
}