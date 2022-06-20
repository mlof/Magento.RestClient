using System.Collections.Generic;
using Magento.RestClient.Modules.Quote.Models;
using Magento.RestClient.Modules.Sales.Models;
using Newtonsoft.Json;

namespace Magento.RestClient.Modules.Common.Models
{
	public record ExtensionAttributes
	{
		[JsonProperty("shipping_assignments")] public List<ShippingAssignment> ShippingAssignments { get; set; }

		[JsonProperty("payment_additional_info")]
		public List<PaymentAdditionalInfo> PaymentAdditionalInfo { get; set; }

		[JsonProperty("applied_taxes")] public List<dynamic> AppliedTaxes { get; set; }

		[JsonProperty("item_applied_taxes")] public List<dynamic> ItemAppliedTaxes { get; set; }
	}
}