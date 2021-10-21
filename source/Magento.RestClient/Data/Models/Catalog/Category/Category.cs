using System;
using System.Collections.Generic;
using Magento.RestClient.Data.Models.Common;
using Newtonsoft.Json;

namespace Magento.RestClient.Data.Models.Catalog.Category
{
	public record Category
	{
		[JsonProperty("id")] public long Id { get; set; }

		[JsonProperty("parent_id")] public long? ParentId { get; set; }
		[JsonProperty("is_active")] public bool? IsActive { get; set; }

		[JsonProperty("name")] public string Name { get; set; }

		[JsonProperty("position")] public long Position { get; set; }

		[JsonProperty("level")] public long Level { get; set; }

		[JsonProperty("children")] public string Children { get; set; }

		[JsonProperty("created_at")] public DateTimeOffset CreatedAt { get; set; }

		[JsonProperty("updated_at")] public DateTimeOffset UpdatedAt { get; set; }

		[JsonProperty("path")] public string Path { get; set; }

		[JsonProperty("available_sort_by")] public List<dynamic> AvailableSortBy { get; set; }

		[JsonProperty("include_in_menu")] public bool IncludeInMenu { get; set; }

		[JsonProperty("custom_attributes")] public List<CustomAttribute> CustomAttributes { get; set; }
	}
}