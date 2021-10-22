using System.Collections.Generic;
using Magento.RestClient.Data.Models.Common;
using Newtonsoft.Json;

namespace Magento.RestClient.Data.Models.EAV.Attributes
{
	public record EntityAttribute
	{
		[JsonProperty("attribute_id")] public long AttributeId { get; set; }

		[JsonProperty("attribute_code")] public string AttributeCode { get; set; }

		[JsonProperty("frontend_input")] public AttributeFrontendInput? FrontendInput { get; set; }

		[JsonProperty("entity_type_id")] public EntityType EntityTypeId { get; set; }

		[JsonProperty("is_required")] public bool IsRequired { get; set; }

		[JsonProperty("options")] public List<Option> Options { get; set; }

		[JsonProperty("is_user_defined")] public bool IsUserDefined { get; set; }

		[JsonProperty("default_frontend_label", NullValueHandling = NullValueHandling.Ignore)]
		public string DefaultFrontendLabel { get; set; }

		[JsonProperty("frontend_labels")] public List<AttributeLabel> FrontendLabels { get; set; }

		[JsonProperty("backend_type")] public string BackendType { get; set; }

		[JsonProperty("is_unique")] public long IsUnique { get; set; }

		[JsonProperty("validation_rules")] public List<dynamic> ValidationRules { get; set; }

		[JsonProperty("source_model", NullValueHandling = NullValueHandling.Ignore)]
		public string SourceModel { get; set; }

		[JsonProperty("default_value", NullValueHandling = NullValueHandling.Ignore)]
		public string DefaultValue { get; set; }

		[JsonProperty("backend_model", NullValueHandling = NullValueHandling.Ignore)]
		public string BackendModel { get; set; }

		[JsonProperty("frontend_class", NullValueHandling = NullValueHandling.Ignore)]
		public string FrontendClass { get; set; }

		[JsonProperty("note", NullValueHandling = NullValueHandling.Ignore)]
		public string Note { get; set; }
	}
}