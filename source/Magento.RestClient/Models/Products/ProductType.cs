using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Magento.RestClient.Models.Products
{
    [JsonConverter(typeof(StringEnumConverter))]

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