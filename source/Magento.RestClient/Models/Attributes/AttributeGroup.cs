using Newtonsoft.Json;

namespace Magento.RestClient.Models.Attributes
{
    public class AttributeGroup
    {
        [JsonProperty("attribute_group_id")]
        public long AttributeGroupId { get; set; }

        [JsonProperty("attribute_group_name")]
        public string AttributeGroupName { get; set; }

        [JsonProperty("attribute_set_id")]
        public long AttributeSetId { get; set; }
    }
}