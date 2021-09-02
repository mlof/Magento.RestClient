using Newtonsoft.Json;

namespace Magento.RestClient.Tests.Integration.Integration
{
	public class ProductFixture
	{

		[JsonProperty("Sku")]
		public string Sku { get; set; }

		[JsonProperty("Title")]
		public string Title { get; set; }

		[JsonProperty("Price")]
		public decimal Price { get; set; }

		[JsonProperty("InStock")]
		public long InStock { get; set; }
	}
}