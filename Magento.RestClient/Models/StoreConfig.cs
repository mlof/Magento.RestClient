using System;
using Newtonsoft.Json;

namespace MagentoApi.Models
{
    public class StoreConfig
    {

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("website_id")]
        public long WebsiteId { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("base_currency_code")]
        public string BaseCurrencyCode { get; set; }

        [JsonProperty("default_display_currency_code")]
        public string DefaultDisplayCurrencyCode { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("weight_unit")]
        public string WeightUnit { get; set; }

        [JsonProperty("base_url")]
        public Uri BaseUrl { get; set; }

        [JsonProperty("base_link_url")]
        public Uri BaseLinkUrl { get; set; }

        [JsonProperty("base_static_url")]
        public Uri BaseStaticUrl { get; set; }

        [JsonProperty("base_media_url")]
        public Uri BaseMediaUrl { get; set; }

        [JsonProperty("secure_base_url")]
        public Uri SecureBaseUrl { get; set; }

        [JsonProperty("secure_base_link_url")]
        public Uri SecureBaseLinkUrl { get; set; }

        [JsonProperty("secure_base_static_url")]
        public Uri SecureBaseStaticUrl { get; set; }

        [JsonProperty("secure_base_media_url")]
        public Uri SecureBaseMediaUrl { get; set; }
    }
}