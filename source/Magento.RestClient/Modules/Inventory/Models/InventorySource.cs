using System.Collections.Generic;
using Newtonsoft.Json;

namespace Magento.RestClient.Modules.Inventory.Models
{
	public record InventorySource
	{
		[JsonProperty("source_code")] public string SourceCode { get; set; }

		[JsonProperty("name")] public string Name { get; set; }

		[JsonProperty("email")] public string Email { get; set; }

		[JsonProperty("contact_name")] public string ContactName { get; set; }

		[JsonProperty("enabled")] public bool Enabled { get; set; }

		[JsonProperty("description")] public string Description { get; set; }

		[JsonProperty("latitude")] public long Latitude { get; set; }

		[JsonProperty("longitude")] public long Longitude { get; set; }

		[JsonProperty("country_id")] public string CountryId { get; set; }

		[JsonProperty("region_id")] public long RegionId { get; set; }

		[JsonProperty("region")] public string Region { get; set; }

		[JsonProperty("city")] public string City { get; set; }

		[JsonProperty("street")] public string Street { get; set; }

		[JsonProperty("postcode")] public string Postcode { get; set; }

		[JsonProperty("phone")] public string Phone { get; set; }

		[JsonProperty("fax")] public string Fax { get; set; }

		[JsonProperty("use_default_carrier_config")]
		public bool UseDefaultCarrierConfig { get; set; }

		[JsonProperty("carrier_links")] public List<CarrierLink> CarrierLinks { get; set; }
		[JsonProperty("extension_attributes")] public dynamic ExtensionAttributes { get; set; }
	}
}