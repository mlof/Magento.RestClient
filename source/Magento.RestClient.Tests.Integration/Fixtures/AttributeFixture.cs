using System.Collections.Generic;
using Newtonsoft.Json;

namespace Magento.RestClient.Tests.Integration.Integration
{
	public partial class AttributeFixture
	{
		[JsonProperty("attribute_code")] public string AttributeCode { get; set; }

		[JsonProperty("frontend_input")] public string FrontendInput { get; set; }

		[JsonProperty("frontend_label")] public string FrontendLabel { get; set; }

		[JsonProperty("option", NullValueHandling = NullValueHandling.Ignore)]
		public List<string> Option { get; set; }
	}
}