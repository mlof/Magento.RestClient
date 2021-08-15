using System.Runtime.Serialization;

namespace Magento.RestClient.Models
{
    public enum ProductType
    {
        [EnumMember(Value = "simple")] Simple,
        [EnumMember(Value = "configurable")] Configurable,

        [EnumMember(Value = "grouped")] Grouped,
        [EnumMember(Value = "virtual")] Virtual,
        [EnumMember(Value = "downloadable")] Downloadable,
        [EnumMember(Value = "bundle")] Bundle
    }
}