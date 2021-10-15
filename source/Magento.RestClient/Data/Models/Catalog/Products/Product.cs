using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using Magento.RestClient.Data.Models.Common;
using Newtonsoft.Json;

namespace Magento.RestClient.Data.Models.Catalog.Products
{
	public record Product
	{
		public Product()
		{
		}

		public Product(string sku)
		{
			this.Sku = sku;
			this.AttributeSetId = 4;
			this.CustomAttributes = new List<CustomAttribute>();
		}

		[JsonProperty("id")] public long? Id { get; set; }

		[JsonProperty("sku"), Required] public string Sku { get; set; }

		[JsonProperty("name"), Required] public string Name { get; set; }

		[JsonProperty("attribute_set_id"), Required]
		public long AttributeSetId { get; set; }

		[JsonProperty("price"), Required] public decimal? Price { get; set; }

		[JsonProperty("status")] public long Status { get; set; }

		[JsonProperty("visibility")] public long Visibility { get; set; }

		[JsonProperty("type_id")] public ProductType TypeId { get; set; }

		[JsonProperty("created_at")] public DateTime? CreatedAt { get; set; }

		[JsonProperty("updated_at")] public DateTime? UpdatedAt { get; set; }

		[JsonProperty("extension_attributes")]
		public Dictionary<string, dynamic> ExtensionAttributes { get; set; } = new Dictionary<string, dynamic>();

		[JsonProperty("product_links")] public List<dynamic> ProductLinks { get; set; }

		[JsonProperty("options")] public List<dynamic> Options { get; set; }

		[JsonProperty("media_gallery_entries")]
		public List<MediaEntry> MediaGalleryEntries { get; set; }

		[JsonProperty("tier_prices")] public List<TierPrice> TierPrices { get; set; }

		[JsonProperty("custom_attributes")] public List<CustomAttribute> CustomAttributes { get; set; }
	}
}