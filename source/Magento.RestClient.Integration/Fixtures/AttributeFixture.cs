using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Magento.RestClient.Integration.Fixtures
{
	public partial class FixtureFile
	{
		[JsonProperty("Attributes")]
		public List<Attribute> Attributes { get; set; }
	

	}

	public class AttributeSet
	{
		public string Name { get; set; }
		public List<AttributeGroup> AttributeGroups { get; set; }
	}

	public class AttributeGroup
	{
		public string Name { get; set; }
		public List<string> Attributes { get; set; }
	}


	public partial class Attribute
	{
		[JsonProperty("Options")]
		public List<string> Options { get; set; }

		[JsonProperty("AttributeCode")]
		public string AttributeCode { get; set; }

		[JsonProperty("Required")]
		public bool AttributeRequired { get; set; }

		[JsonProperty("Visible")]
		public bool Visible { get; set; }

		[JsonProperty("Searchable")]
		public bool Searchable { get; set; }

		[JsonProperty("UseInLayeredNavigation")]
		public bool UseInLayeredNavigation { get; set; }

		[JsonProperty("Comparable")]
		public bool Comparable { get; set; }

		[JsonProperty("FrontendInput")]
		public string FrontendInput { get; set; }

		[JsonProperty("DefaultFrontendLabel")]
		public string DefaultFrontendLabel { get; set; }

		[JsonProperty("IsPersisted")]
		public bool IsPersisted { get; set; }
	}

}
