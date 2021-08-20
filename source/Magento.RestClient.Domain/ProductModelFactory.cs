using Magento.RestClient.Repositories.Abstractions;

namespace Magento.RestClient.Domain
{
	public class ProductModelFactory
	{
		private readonly IAdminClient client;

		public ProductModelFactory(IAdminClient client)
		{
			this.client = client;
		}



		public ProductModel CreateNew(string sku)
		{



			return new ProductModel(client.Products, sku);
		}

		public static ProductModelFactory CreateInstance(IAdminClient client)
		{
			return new ProductModelFactory(client);
		}
	}
}