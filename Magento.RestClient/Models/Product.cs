using System;
using System.Collections.Generic;
using System.Dynamic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Magento.RestClient.Models
{
    public class Product
    {
        public Product()
        {
        }

        [JsonProperty("id")] public long? Id { get; set; }

        [JsonProperty("sku")] public string Sku { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("attribute_set_id")] public long AttributeSetId { get; set; }

        [JsonProperty("price")] public decimal? Price { get; set; }

        [JsonProperty("status")] public long Status { get; set; }

        [JsonProperty("visibility")] public long Visibility { get; set; }

        [JsonProperty("type_id")] public ProductType TypeId { get; set; }

        [JsonProperty("created_at")] public DateTime? CreatedAt { get; set; }

        [JsonProperty("updated_at")] public DateTime? UpdatedAt { get; set; }

        [JsonProperty("extension_attributes")] public dynamic ExtensionAttributes { get; set; } = new ExpandoObject();

        [JsonProperty("product_links")] public List<dynamic> ProductLinks { get; set; }

        [JsonProperty("options")] public List<dynamic> Options { get; set; }

        [JsonProperty("media_gallery_entries")]
        public List<dynamic> MediaGalleryEntries { get; set; }

        [JsonProperty("tier_prices")] public List<TierPrice> TierPrices { get; set; }

        [JsonProperty("custom_attributes")] public List<CustomAttribute> CustomAttributes { get; set; }

        public void SetStockItem(StockItem stockItem)
        {
            this.ExtensionAttributes.stock_item = stockItem;
        }
    }
}