using Newtonsoft.Json;

namespace Magento.RestClient.Data.Models.Store
{
    public record StoreView
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("website_id")]
        public long WebsiteId { get; set; }

        [JsonProperty("store_group_id")]
        public long StoreGroupId { get; set; }

        [JsonProperty("is_active")]
        public long IsActive { get; set; }
    }
}