using Magento.RestClient.Data.Models.Catalog.Products;
using Newtonsoft.Json;

namespace Magento.RestClient.Data.Repositories.Requests
{
	public record CreateOrUpdateMediaRequest
	{
		[JsonProperty("sku")]
		public string Sku { get; set; }
		[JsonProperty("entry")]
		public MediaEntry Entry { get; set; }
	}
}