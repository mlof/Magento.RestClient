using System.Collections.Generic;
using Newtonsoft.Json;

namespace Magento.RestClient.Models.Carts
{
	public record BillingAddress
	{
		[JsonProperty("id")] public long Id { get; set; }

		[JsonProperty("region")] public dynamic Region { get; set; }

		[JsonProperty("region_id")] public dynamic RegionId { get; set; }

		[JsonProperty("region_code")] public dynamic RegionCode { get; set; }

		[JsonProperty("country_id")] public dynamic CountryId { get; set; }

		[JsonProperty("street")] public List<string> Street { get; set; }

		[JsonProperty("telephone")] public dynamic Telephone { get; set; }

		[JsonProperty("postcode")] public dynamic Postcode { get; set; }

		[JsonProperty("city")] public dynamic City { get; set; }

		[JsonProperty("firstname")] public dynamic Firstname { get; set; }

		[JsonProperty("lastname")] public dynamic Lastname { get; set; }

		[JsonProperty("email")] public dynamic Email { get; set; }

		[JsonProperty("same_as_billing")] public long SameAsBilling { get; set; }

		[JsonProperty("save_in_address_book")] public long SaveInAddressBook { get; set; }
	}
}