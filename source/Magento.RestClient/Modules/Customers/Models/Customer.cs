using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Magento.RestClient.Modules.Customers.Models
{
	public record Customer
	{
		[JsonProperty("id")] public long Id { get; set; }

		[JsonProperty("group_id")] public long GroupId { get; set; }

		[JsonProperty("default_billing")] public long DefaultBilling { get; set; }

		[JsonProperty("default_shipping")] public long DefaultShipping { get; set; }

		[JsonProperty("created_at")] public DateTime CreatedAt { get; set; }

		[JsonProperty("updated_at")] public DateTime UpdatedAt { get; set; }

		[JsonProperty("created_in")] public string CreatedIn { get; set; }

		[JsonProperty("dob")] public DateTime Dob { get; set; }

		[JsonProperty("email")] public string Email { get; set; }

		[JsonProperty("firstname")] public string Firstname { get; set; }

		[JsonProperty("lastname")] public string Lastname { get; set; }

		[JsonProperty("middlename")] public string Middlename { get; set; }

		[JsonProperty("gender")] public long Gender { get; set; }

		[JsonProperty("store_id")] public long StoreId { get; set; }

		[JsonProperty("website_id")] public long WebsiteId { get; set; }

		[JsonProperty("addresses")] public List<Address> Addresses { get; set; }

		[JsonProperty("disable_auto_group_change")]
		public long DisableAutoGroupChange { get; set; }

		[JsonProperty("extension_attributes")] public dynamic ExtensionAttributes { get; set; }
	}
}