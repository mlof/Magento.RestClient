using Magento.RestClient.Data.Models.Common;
using Newtonsoft.Json;

namespace Magento.RestClient.Data.Models.Attributes
{
	public record AttributeSet
	{
		[JsonProperty("attribute_set_id")] public long? AttributeSetId { get; set; }

		[JsonProperty("attribute_set_name")] public string AttributeSetName { get; set; }

		[JsonProperty("sort_order")] public long SortOrder { get; set; }

		[JsonProperty("entity_type_id")] public EntityType EntityTypeId { get; set; }
	}
}