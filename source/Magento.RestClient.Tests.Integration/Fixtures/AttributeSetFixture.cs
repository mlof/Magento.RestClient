using System.Collections.Generic;
using Newtonsoft.Json;

namespace Magento.RestClient.Tests.Integration.Integration
{
	public class AttributeSetFixture
	{
	
		[JsonProperty("AttributeSetCode")]
		public string AttributeSetCode { get; set; }

		[JsonProperty("AttributeSetName")]
		public string AttributeSetName { get; set; }

		[JsonProperty("AttributeGroups")]
		public List<AttributeGroup> AttributeGroups { get; set; }


	}
}