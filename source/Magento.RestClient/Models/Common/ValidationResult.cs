using System.Collections.Generic;
using Newtonsoft.Json;

namespace Magento.RestClient.Models.Common
{
    public class ValidationResult
    {
        [JsonProperty("valid")]
        public bool Valid { get; set; }
        [JsonProperty("messages")]
        public List<string> Messages { get; set; }
    }
}