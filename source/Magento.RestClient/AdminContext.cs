using System.Collections.Generic;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Data.Repositories;
using Magento.RestClient.Data.Repositories.Abstractions;
using Magento.RestClient.Exceptions;
using Microsoft.Extensions.Caching.Memory;
using RestSharp;

namespace Magento.RestClient
{
	internal class AdminContext : IAdminContext
	{
		private readonly IRestClient _client;
		private readonly MemoryCache cache;

		public AdminContext(MagentoClient client)
		{
			_client = client._client;
			this.cache = client.cache;
		}


		public IBulkRepository Bulk => new BulkRepository(_client);
		public ISpecialPriceRepository SpecialPrices => new SpecialPriceRepository(_client);
		public IProductAttributeGroupRepository ProductAttributeGroups => new ProductAttributeGroupRepository(_client);
		public IStoreRepository Stores => new StoreRepository(_client);
		public IProductRepository Products => new ProductRepository(_client);
		public IProductMediaRepository ProductMedia => new ProductMediaRepository(_client);
		public IConfigurableProductRepository ConfigurableProducts => new ConfigurableProductRepository(_client);
		public IOrderRepository Orders => new OrderRepository(_client);
		public ICustomerRepository Customers => new CustomerRepository(_client);
		public ICustomerGroupRepository CustomerGroups => new CustomerGroupRepository(_client);
		public IDirectoryRepository Directory => new DirectoryRepository(_client);
		public IAttributeSetRepository AttributeSets => new AttributeSetRepository(_client, cache);
		public IInvoiceRepository Invoices => new InvoiceRepository(_client);
		public ICategoryRepository Categories => new CategoryRepository(_client);
		public ICartRepository Carts => new CartRepository(_client);
		public IAttributeRepository Attributes => new AttributeRepository(_client, cache);
		public IShipmentRepository Shipments => new ShipmentRepository(_client);

		/// <inheritdoc cref="ICanGetModules" />
		async public Task<List<string>> GetModules()
		{
			var request = new RestRequest("modules");


			var response = await _client.ExecuteAsync<List<string>>(request);
			if (response.IsSuccessful)
			{
				return response.Data;
			}

			throw MagentoException.Parse(response.Content);
		}
	}
}