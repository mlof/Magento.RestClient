using Newtonsoft.Json;

namespace Magento.RestClient.Domain.Cart
{
    public partial class Customer
    {
        [JsonProperty("email")] public string Email { get; set; }

        [JsonProperty("firstname")] public string Firstname { get; set; }

        [JsonProperty("lastname")] public string Lastname { get; set; }
    }
}