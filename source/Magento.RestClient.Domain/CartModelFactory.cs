using Magento.RestClient.Repositories.Abstractions;

namespace Magento.RestClient.Domain
{
	public class CartModelFactory
	{
		private readonly IAdminClient client;

		public CartModelFactory(IAdminClient client)
		{
			this.client = client;
		}


		public CartModel CreateNew()
		{
			var cart = new CartModel(client.Carts);
			return cart.CreateNew();
		}


		public CartModel GetExisting(long id)
		{
			var cart = new CartModel(client.Carts, id);


			return cart;
		}

		public static ProductModelFactory CreateInstance(IAdminClient client)
		{
			return new ProductModelFactory(client);
		}
	}
}