using Newtonsoft.Json;

namespace Magento.RestClient.Models
{
    public class StoreGroup
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("website_id")]
        public long WebsiteId { get; set; }

        [JsonProperty("root_category_id")]
        public long RootCategoryId { get; set; }

        [JsonProperty("default_store_id")]
        public long DefaultStoreId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }
}