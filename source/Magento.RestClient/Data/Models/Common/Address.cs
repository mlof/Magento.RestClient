using System.Collections.Generic;
using Newtonsoft.Json;

namespace Magento.RestClient.Data.Models.Common
{
	public record Address
	{
		[JsonProperty("id")] public long Id { get; set; }

		[JsonProperty("customer_id")] public long CustomerId { get; set; }

		[JsonProperty("region")] public Region Region { get; set; }

		[JsonProperty("region_id")] public long RegionId { get; set; }

		[JsonProperty("country_id")] public string CountryId { get; set; }

		[JsonProperty("street")] public List<string> Street { get; set; }

		[JsonProperty("telephone")] public string Telephone { get; set; }

		[JsonProperty("postcode")] public string Postcode { get; set; }

		[JsonProperty("city")] public string City { get; set; }

		[JsonProperty("firstname")] public string Firstname { get; set; }

		[JsonProperty("lastname")] public string Lastname { get; set; }

		[JsonProperty("default_billing", NullValueHandling = NullValueHandling.Ignore)]
		public bool? DefaultBilling { get; set; }

		[JsonProperty("company", NullValueHandling = NullValueHandling.Ignore)]
		public string Company { get; set; }

		[JsonProperty("default_shipping", NullValueHandling = NullValueHandling.Ignore)]
		public bool? DefaultShipping { get; set; }
	}
}