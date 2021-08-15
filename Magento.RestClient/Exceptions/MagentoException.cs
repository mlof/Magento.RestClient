using System;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Magento.RestClient.Exceptions
{
    public class MagentoException : Exception
    {
        private MagentoException(string message) : base(message)
        {
        }

        public static MagentoException Parse(string content)
        {
            var c = JsonConvert.DeserializeObject<MagentoError>(content);
            Debug.Assert(c != null, nameof(c) + " != null");
            return new MagentoException(c.Message);
        }

        internal class MagentoError
        {
            [JsonProperty("message")] public string Message { get; set; }
            [JsonProperty("parameters")] public dynamic Parameters { get; set; }
        }
    }
}