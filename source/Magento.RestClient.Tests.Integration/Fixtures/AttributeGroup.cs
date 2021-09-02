using System.Collections.Generic;
using Newtonsoft.Json;

namespace Magento.RestClient.Tests.Integration.Integration
{
	public partial class AttributeGroup
	{
		[JsonProperty("Attributes")]
		public List<string> Attributes { get; set; }

		[JsonProperty("AttributeGroupCode")]
		public string AttributeGroupCode { get; set; }

		[JsonProperty("AttributeGroupName")]
		public string AttributeGroupName { get; set; }
	}
}