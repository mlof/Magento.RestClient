using Newtonsoft.Json;

namespace Magento.RestClient.Exceptions
{
	internal class ErrorParameter
	{
		[JsonProperty("fieldName")] public string FieldName { get; set; }
	}
}